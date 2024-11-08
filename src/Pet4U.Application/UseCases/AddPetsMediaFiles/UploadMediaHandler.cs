using Microsoft.Extensions.Logging;
using Pet4U.Application.Database;
using Pet4U.Application.FileProdiver;
using Pet4U.Application.UseCases.AddPetPhoto;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Application.UseCases.Providers;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.AddPetsMediaFiles;

public class UploadMediaHandler : IUploadMediaHandler
{
  private const string BUCKET_NAME = "photos";
  private readonly IFileProvider _fileProvider;
  private readonly ILogger<UploadMediaHandler> _logger;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IVolunteersRepository _volunteersRepository;

  public UploadMediaHandler(IVolunteersRepository volunteersRepository, IFileProvider fileProvider, IUnitOfWork unitOfWork, ILogger<UploadMediaHandler> logger)
  {
    _volunteersRepository = volunteersRepository;
    _fileProvider = fileProvider;
    _logger = logger;
    _unitOfWork = unitOfWork;
  }


  public async Task<Result<IReadOnlyList<string>>> HandleAsync
  (
    UploadFilesCommand command,
    CancellationToken cancellationToken
  )
  {
    List<Result<string>> results = [];
    var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
    try
    {

      var volunteerResult = await _volunteersRepository.GetByIdAsync(VolunteerId.Create(command.volunteerId), cancellationToken);

      if(volunteerResult.IsFailure)
        return Errors.General.NotFound(command.volunteerId);

      var petResult = await _volunteersRepository.GetPetById(PetId.Create(command.petId), cancellationToken);

      if (petResult.IsFailure)
          return Errors.General.NotFound(command.petId);

      IEnumerable<FileData> filesData = command.uploadFilesCommand
              .Select(file => new FileData(file.Stream, file.BucketName, Guid.NewGuid().ToString() + file.ObjectName.Substring(file.ObjectName.IndexOf('.') - 1)));

      //Add petPhoto 
      foreach (var fileData in filesData)
      {
         petResult.Value.AddPetPhoto(new PetPhoto(){Path = fileData.ObjectName,  IsMain = false });
      }

      await _unitOfWork.SaveChangesAsync(cancellationToken);

      var result = await _fileProvider.UploadFiles(filesData, cancellationToken);

      transaction.Commit();

      return result.Value.Select(r => r).ToList();

     }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                "Failure add photos to pet: {id}", command.petId);

      transaction.Rollback();

      return Error.Failure("files.upload", "fail to upload files in minio");
    }
  }
    //public async Task<Result<IReadOnlyList<string>>> HandleAsync
    //(
    //  UploadFilesCommand command,
    //  CancellationToken cancellationToken
    //)
    //{
    //    List<Result<string>> results = [];
    //    var semaphoreSlim = new SemaphoreSlim(MAX_CONCURRENT_REQUESTS);
    //    try
    //    {
    //        await semaphoreSlim.WaitAsync(cancellationToken);

    //        await Parallel.ForEachAsync(command.uploadFilesCommand, async (file, cancellationToken) => {

    //            var fileData = new FileData(file.Stream, file.BucketName, Guid.NewGuid().ToString() + file.ObjectName.Substring(file.ObjectName.IndexOf('.') - 1));
    //            var result = await _fileProvider.UploadFileWithSemaphoreAsync(fileData, semaphoreSlim, cancellationToken);
    //            results.Add(result);
    //        });

    //        return results.Select(r => r.Value).ToList();
    //        //return Result<IReadOnlyList<string>>.Success(results.Select(r => r.Value).ToList().AsReadOnly());
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, ex.Message);
    //        return Error.Failure("file.upload", "fail to upload file in minio");
    //    }
    //    finally
    //    {
    //        _ = semaphoreSlim.Release(MAX_CONCURRENT_REQUESTS);
    //    }


    //}
}
