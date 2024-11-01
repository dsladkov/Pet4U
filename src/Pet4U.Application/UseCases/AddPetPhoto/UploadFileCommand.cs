namespace Pet4U.Application.UseCases.AddPetPhoto;

public record UploadFileCommand
(

Stream Stream,
//string BucketName,
string ObjectName
)
{
public static UploadFileCommand ToCommand(Stream stream, string objectName) => new(stream, objectName);
}