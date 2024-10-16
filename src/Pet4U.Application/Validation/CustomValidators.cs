using FluentValidation;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.Validation;

public static class CustomValidators
{
  public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(this IRuleBuilder<T, TElement> ruleBuilder, Func<TElement, Result<TValueObject>> factoryMethod) 
  {
	  return ruleBuilder.Custom((value, context) => 
    {
     Result<TValueObject> result = factoryMethod(value);
     if(result.IsSuccess)
      return;
     context.AddFailure(result.Error.Serialize());
    });
  }
}