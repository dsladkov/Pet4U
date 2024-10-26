namespace Pet4U.Application.FileProdiver;

public record UrlFileData
(
    string BucketName, 
    string ObjectName,
    int? TimeExpire 
);