namespace Pet4U.Domain.Shared;

public record Error
{
  public const string SEPARATOR = "|";
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


  public string Serialize()
  {
    // "value.is.invalid"|| "Message is invalid"
    return string.Join(SEPARATOR, Code, Message, Type);
  }

  public static Error Deserialize(string serialized)
  {
    var parts = serialized.Split(SEPARATOR);
    if(parts.Length < 3)
      throw new ArgumentException("Invalid serialized format");

    if(Enum.TryParse<Type>(parts[2], out var type) == false)
      throw new ArgumentException("Invalid serialized format");
    
    return new Error(parts[0], parts[1], type);
  }

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