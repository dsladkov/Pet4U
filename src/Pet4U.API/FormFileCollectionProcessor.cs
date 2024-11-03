using Pet4U.Application.UseCases.AddPetPhoto;

namespace Pet4U.API;

public class FormFileCollectionProcessor : IAsyncDisposable, IFormFileCollectionProcessor
{
  private readonly List<UploadFileCommand> commands = []!;

  public async ValueTask DisposeAsync() =>
    await Parallel.ForEachAsync(commands,  async (command, cancellationToken) => await command.Stream.DisposeAsync());

  public UploadFilesCommand Process(IFormFileCollection files)
  {
    Parallel.ForEach(files, file => {
      var stream = file.OpenReadStream();

      commands.Add(new UploadFileCommand(stream, "photos",file.Name));
    });

    return UploadFilesCommand.ToCommand(commands);
  }
}

public interface IFormFileCollectionProcessor
{
  UploadFilesCommand Process(IFormFileCollection files);
}