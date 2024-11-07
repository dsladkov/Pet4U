namespace Pet4U.Application.FileProdiver;
public record RemoveFileData
(
    string BucketName, 
    Guid Id,
    int? TimeExpire 
);