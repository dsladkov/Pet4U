using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Pet4U.Application.FileProdiver;
using Pet4U.Application.UseCases.AddPetPhoto;
using Pet4U.Application.UseCases.Providers;
using Pet4U.Domain.Shared;
using System.Threading;

namespace Pet4U.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private const int TIME_SECONDS_EXPIRE = 3600; 
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;
    private const int MAX_CONCURRENT_REQUESTS = 10;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
  {
    _minioClient = minioClient;
    _logger = logger;
  }
  public async Task<Result<string>> UploadFileWithSemaphoreAsync(
    FileData fileData,
    SemaphoreSlim semaphore,
    CancellationToken cancellationToken = default)
  {
    await semaphore.WaitAsync(cancellationToken);

    var putObjectsArgs = new PutObjectArgs()
                            .WithBucket(fileData.BucketName)
                            .WithStreamData(fileData.Stream)
                            .WithObjectSize(fileData.Stream.Length)
                            .WithObject(fileData.ObjectName.ToString());
    try
    {
      var bucketExistArgs = new BucketExistsArgs().WithBucket(fileData.BucketName);

      var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

      if(bucketExist == false)
      {
        var makeBucketArg = new MakeBucketArgs().WithBucket(fileData.BucketName);
        await _minioClient.MakeBucketAsync(makeBucketArg, cancellationToken);
      }

      

      var result = await _minioClient.PutObjectAsync(putObjectsArgs, cancellationToken);
      return result.ObjectName;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "fail to upload file in minio with path {path} in bucket {bucket}", fileData.ObjectName, fileData.BucketName);
      return Error.Failure("file.upload", "Fail to upload file in minio");
    }
    finally
    {
        semaphore.Release();
    }
  }


    public async Task<Result<IReadOnlyCollection<string>>> UploadFiles
        (IEnumerable<FileData> files,
        CancellationToken cancellationToken = default)
    {
        var semaforeSlim = new SemaphoreSlim(MAX_CONCURRENT_REQUESTS);

        var fileList = files.ToList();
        try
        {
            await IfBucketNotExistCreateBucket(fileList, cancellationToken);

            var tasks = fileList.Select(async file =>
                await PutObject(file, semaforeSlim, cancellationToken));

            var pathResult = await Task.WhenAll(tasks);

            if(pathResult.Any(p => p.IsFailure))
                return pathResult.First().Error;

            var result = pathResult.Select(p => p.Value).ToList().AsReadOnly();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload files in minio, files amount {amount}", fileList.Count);
            return Error.Failure("files.upload", "Fail to upload files in minio");
        }
    }

    public async Task<Result<string>> PutObject(
    FileData fileData,
    SemaphoreSlim semaphore,
    CancellationToken cancellationToken = default)
    {
        await semaphore.WaitAsync(cancellationToken);
        var putObjectsArgs = new PutObjectArgs()
                                .WithBucket(fileData.BucketName)
                                .WithStreamData(fileData.Stream)
                                .WithObjectSize(fileData.Stream.Length)
                                .WithObject(fileData.ObjectName.ToString());
        try
        {
            

            var result = await _minioClient.PutObjectAsync(putObjectsArgs, cancellationToken);
            return result.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio with path {path} in bucket {bucket}", fileData.ObjectName, fileData.BucketName);
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }

        finally
        {
            semaphore.Release();
        }
    }

    public async Task<Result<string>> UploadFileAsync(
    FileData fileData,
    CancellationToken cancellationToken = default)
    {
        var putObjectsArgs = new PutObjectArgs()
                                .WithBucket(fileData.BucketName)
                                .WithStreamData(fileData.Stream)
                                .WithObjectSize(fileData.Stream.Length)
                                .WithObject(fileData.ObjectName.ToString());
        try
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(fileData.BucketName);

            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArg = new MakeBucketArgs().WithBucket(fileData.BucketName);
                await _minioClient.MakeBucketAsync(makeBucketArg, cancellationToken);
            }

            var result = await _minioClient.PutObjectAsync(putObjectsArgs, cancellationToken);
            return result.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "fail to upload file in minio with path {path} in bucket {bucket}", fileData.ObjectName, fileData.BucketName);
            return Error.Failure("file.upload", "Fail to upload file in minio");
        }
    }

    public async Task<Result<string>> GetUrlFileAsync(UrlFileData file, CancellationToken cancellationToken = default)
  {
    try
    {
      var args = new PresignedGetObjectArgs()
                     .WithBucket(file.BucketName)
                     .WithObject(file.ObjectName)
                     .WithExpiry(file.TimeExpire ?? TIME_SECONDS_EXPIRE);
      var url = await _minioClient.PresignedGetObjectAsync(args);
      return url;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "fail url get from minio");
      return Error.Failure("file.url.get", "fail url get from minio");
    }
  }

  public async Task<Result> RemoveFileAsync(RemoveFileData file, CancellationToken cancellationToken = default)
  {
    try
    {
      var args = new RemoveObjectArgs()
                    .WithBucket(file.BucketName)
                    .WithObject(file.Id.ToString());

      await _minioClient.RemoveObjectAsync(args, cancellationToken);

      return Result.Success();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "fail to remove file from minio");
      return Error.Failure("file.to.remove", "fail to remove file from minio");
    }
  }

    private async Task IfBucketNotExistCreateBucket(
        IEnumerable<FileData> fileData,
        CancellationToken cancellationToken = default)
    {
        HashSet<string> bucketNames = [.. fileData.Select(r => r.BucketName)];
        foreach (var bucketName in bucketNames)
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(bucketName);

            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (bucketExist == false)
            {
                var makeBucketArg = new MakeBucketArgs().WithBucket(bucketName);
                await _minioClient.MakeBucketAsync(makeBucketArg, cancellationToken);
            }
        }
    }

}