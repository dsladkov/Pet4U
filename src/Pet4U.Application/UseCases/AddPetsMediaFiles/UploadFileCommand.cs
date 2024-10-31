using Pet4U.Application.UseCases.AddPetPhoto;

public record UploadFilesCommand(IEnumerable<UploadFileCommand> uploadFilesCommand)
{
public static UploadFilesCommand ToCommand(IEnumerable<UploadFileCommand> uploadFilesCommand) => new(uploadFilesCommand);
}
