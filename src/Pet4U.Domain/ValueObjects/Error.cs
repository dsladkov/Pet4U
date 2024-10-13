namespace Pet4U.Domain.ValueObjects;

public record Error
{
  public string? Code { get; }
  public string? Message { get; }
  public Type Type { get; }

  private Error(string? code, string? message, Type type)
  {
    Code = code;
    Message = message;
    Type = type;
  }

  public static Error Validation(string code, string message) => new(code, message, Type.Validation);

  public static Error NotFound(string code, string message) => new(code, message, Type.NotFound);
  
  public static Error Failure(string code, string message) => new(code, message, Type.Failure);

  public static Error Conflict(string code, string message) => new(code, message, Type.Conflict);

  public static Error None() => new(null, null, Type.None);

  public static implicit operator Type(Error error) => error.Type;

}


public enum Type
{
  None,
  Validation,
  NotFound,
  Failure,
  Conflict
}