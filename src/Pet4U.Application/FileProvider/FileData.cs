namespace Pet4U.Application.FileProdiver;

public record FileData
(
    Stream Stream,
    string BucketName, 
    string ObjectName 
);