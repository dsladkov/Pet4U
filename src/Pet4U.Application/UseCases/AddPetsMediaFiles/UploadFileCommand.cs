using Pet4U.Application.UseCases.AddPetPhoto;

public record UploadFilesCommand(IEnumerable<UploadFileCommand> uploadFilesCommand, Guid volunteerId, Guid petId)
{
public static UploadFilesCommand ToCommand(IEnumerable<UploadFileCommand> uploadFilesCommand, Guid volunteerId, Guid petId) => new(uploadFilesCommand, volunteerId, petId);
}
