namespace Pet4U.Application.UseCases.RemovePetPhotoFile;

public record RemoveFileCommand
(
string BucketName,
Guid Id,
string VersionId
)
{
public static RemoveFileCommand ToCommand(string bucketName, Guid id, string versionId) 
=> new(bucketName, id, versionId);
}
