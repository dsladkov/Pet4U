namespace Pet4U.Application.FileProdiver;
public record RemoveFileData
(
    string BucketName, 
    string ObjectName,
    int? TimeExpire 
);