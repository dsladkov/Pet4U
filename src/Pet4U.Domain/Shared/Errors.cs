namespace Pet4U.Domain.Shared;

public static class Errors
{
  public static class General
  {
    public static Error ValueIsInvalid( string? name = null )
    {
      return Error.Validation("value.is.invalid", $"{name ?? "value"} is invalid");
    }
    public static Error NotFound( Guid? id = null )
    {
      var forId = id == null ? "": $"for Id '{id}'";
      return Error.NotFound("record.not.found", $"record not found{forId}");
    }
    public static Error LengthIsInvalid( string? name = null )
    {
      var label = name + " " ?? "";
      return Error.Validation("length.is.invalid", $"invalid {label}length ");
    }
  }
}