namespace Sandbox.Utility.Result;

public record ValidationError : Error
{
    public ValidationError(IEnumerable<FieldErrors> validationErrors)
        : base("ValidationFailure", "Validation error occurred during processing the request")
    {
        Model = validationErrors;
    }

    public IEnumerable<FieldErrors> Model { get; set; }
}

public record FieldErrors(string Field, string[] Errors);