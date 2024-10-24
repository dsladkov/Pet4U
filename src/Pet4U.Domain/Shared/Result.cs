namespace Pet4U.Domain.Shared;

public class Result
{
  protected Result(bool isSuccess, Error error)
  {
    if((isSuccess && error != Type.None) || (!isSuccess && error == Type.None))
      throw new InvalidOperationException();
    
    IsSuccess = isSuccess;
    Error = error;
  }

  public Error Error { get; }
  public bool IsSuccess { get; }
  public bool IsFailure => !IsSuccess;

  public static Result Success() => new(true, Error.None());
  public static Result Failure(Error error) => new(false, error);

  public static implicit operator Result(Error error) => new (false, error);
}

public class Result<TValue> : Result
{
  private readonly TValue _value;

  public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("The value of a failure cannot be accessed");

  protected Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
  {
    _value = value;
  }

  public static Result<TValue> Success( TValue value ) => new(value, true, Error.None());
  public static new Result<TValue> Failure ( Error error ) => new(default!, false, error);

  public static implicit operator Result<TValue>(TValue value) => new(value, true, Error.None());
  public static implicit operator Result<TValue>(Error error) => new(default!, false, error);
}

// public class Result<TValue, TError> : Result<TValue>
// {

//   private readonly TError _error;
//   public Result(TValue value, TError error, bool isSuccess, string? errorString) : base(value, isSuccess, errorString)
//   {
//     _error = error;
//   }

//   public new TError Error => IsFailure ? _error : default!;


//   public static new Result<TValue, TError> Success( TValue value ) => new(value, default! ,true, null);
//   public static Result<TValue, TError> Failure ( TError error, string errorString ) => new(default!, error, false, errorString);

//   public static implicit operator Result<TValue, TError>(TValue value) => new(value, default!, true, null);
//   public static implicit operator Result<TValue, TError>(TError error) => new(default!, error, false, "error");

// }