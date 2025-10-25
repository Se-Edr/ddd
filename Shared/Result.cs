

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared
{
    public class Result
    {
        private Result(bool isSucces,ApplicationError error)
        {
            if(isSucces && error !=ApplicationError.NoError || !isSucces && error == ApplicationError.NoError)
            {
                throw new Exception("Mega bug...");
            }
            IsSucces = isSucces;
            Error = error;
        }

        public ApplicationError Error { get; }
        public bool IsSucces{ get; }

        public static Result Success() => new(true, ApplicationError.NoError);
        public static Result Failure(ApplicationError err) => new(false, err);
    }

    public record ApplicationError
    {
        public ApplicationError(string description,ErrorType type)
        {
            Description = description;
            Type = type;
        }

        public string Description { get; }

        public ErrorType Type { get; }

        public static ApplicationError NoError = new("ok", ErrorType.None);
        public static ApplicationError Failure(string description) => new(description, ErrorType.Failure);
        public static ApplicationError NotFound(string description) => new(description, ErrorType.NotFound);
        public static ApplicationError Validation(string description) => new(description, ErrorType.Validation);

    }

    public enum ErrorType
    {
        None=0,
        Failure=1,
        Validation=2,
        NotFound=3

    }
}
