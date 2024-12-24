namespace TransactionService.Contract.Models
{
    public class Result
    {
        public bool IsSuccess { get; }

        public string? ErrorMessage { get; set; }

        public Result(bool isSuccess, string errorMessage = default)
        {
            this.IsSuccess = isSuccess;
            this.ErrorMessage = errorMessage;
        }

        public static Result CreateSuccess() => new Result(true);

        public static Result<T> CreateSuccess<T>(T value) => new Result<T>(value, true);

        public static Result CreateFailure() => new Result(false);

        public static Result CreateFailure(string errorMessage) => new Result(false, errorMessage);

        public static Result<T> CreateFailure<T>(T value) => new Result<T>(value, false);

        public static Result<T> CreateFailure<T>(T value, string errorMessage) => new Result<T>(value, false, errorMessage);
    }

    public class Result<TValue> : Result
    {
        public TValue Value { get; }

        public Result(TValue value, bool isSuccess) : base(isSuccess) => this.Value = value;

        public Result(TValue value, bool isSuccess, string errorMessage) : base(isSuccess, errorMessage) => this.Value = value;
    }
}