namespace Pet4U.Application.UseCases.GetUrlPetPhoto;

public record GetUrlFileCommand
(
string BucketName,
string ObjectName
)
{
public static GetUrlFileCommand ToCommand(string bucketName, string objectName) => new(bucketName, objectName);
}
