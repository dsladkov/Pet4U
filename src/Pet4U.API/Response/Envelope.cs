namespace Pet4U.Response;

//public record ResponseError(Error error, string?  InvalidField);

public record ResponseError(string? errorCode, string? errorMessage,  string?  InvalidField);

public record Envelope
{
  public object? Result { get; }

  //public string? ErrorCode { get; }

  //public string? ErrorMessage { get; }

  public IReadOnlyCollection<ResponseError> Errors { get;}

  public DateTime TimeGenerated { get; }

  private Envelope(object? result, IEnumerable<ResponseError> errors) //Error? error, 
  {
    Result = result;
    //ErrorCode = error?.Code;
    //ErrorMessage = error?.Message;
    Errors = errors.ToList();
    
    TimeGenerated = DateTime.Now;
  }

  public static Envelope Ok(object? result) =>
    new Envelope(result, null);

  public static Envelope Error(List<ResponseError> errors) => //Error error, 
    new Envelope(null, errors);  
}