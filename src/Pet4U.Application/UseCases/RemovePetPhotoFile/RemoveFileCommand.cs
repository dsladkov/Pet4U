namespace Pet4U.Application.UseCases.RemovePetPhotoFile;

public record RemoveFileCommand
(
string BucketName,
string ObjectName,
string VersionId
)
{
public static RemoveFileCommand ToCommand(string bucketName, string objectName, string versionId) 
=> new(bucketName, objectName, versionId);
}
