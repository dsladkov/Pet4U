using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Pet4U.Application.FileProdiver;
using Pet4U.Application.UseCases.AddPetPhoto;
using Pet4U.Application.UseCases.Providers;
using Pet4U.Domain.Shared;

namespace Pet4U.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
  {
    _minioClient = minioClient;
    _logger = logger;
  }
  public async Task<Result<string>> UploadFileAsync(
    FileData fileData,
    CancellationToken cancellationToken = default)
  {

    // var buckets = await _minioClient.ListBucketsAsync();

    // var bucketStrings = string.Join(", ", buckets.Buckets.Select(x => x.Name));

    try
    {
      var bucketExistArgs = new BucketExistsArgs().WithBucket("photos");

      var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

      if(bucketExist == false)
      {
        var makeBucketArg = new MakeBucketArgs().WithBucket("photos");
        await _minioClient.MakeBucketAsync(makeBucketArg, cancellationToken);
      }

      //await using var stream = file.OpenReadStream();

      var putObjectsArgs = new PutObjectArgs().WithBucket("photos")
                                .WithStreamData(fileData.Stream)
                                .WithObjectSize(fileData.Stream.Length)
                                .WithObject(fileData.ObjectName.ToString());

      var result = await _minioClient.PutObjectAsync(putObjectsArgs, cancellationToken);
      return result.ObjectName;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "fail to upload file in minio");
      return Error.Failure("file.upload", "fail to upload file in minio");
    }
  }
}