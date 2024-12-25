using Newtonsoft.Json;

namespace TransactionService.Contract.Models
{
    public class Result
    {
        public bool IsSuccess { get; }

        public Error Error { get; set; }

        [JsonIgnore]
        public int HttpStatusCode { get; set; }

        public Result(bool isSuccess, int httpStatusCode = 200)
        {
            this.IsSuccess = isSuccess;
            this.HttpStatusCode = httpStatusCode;
        }

        public Result(Error error, int httpStatusCode = 400)
        {
            this.IsSuccess = false;
            this.Error = error;
            this.HttpStatusCode = httpStatusCode;
        }

        public static Result CreateSuccess() => new Result(true);

        public static Result<T> CreateSuccess<T>(T value) => new Result<T>(value);

        public static Result CreateFailure() => new Result(false, 400);

        public static Result CreateFailure(Enum type, string message, int httpStatusCode = 400) => new Result(new Error(type, message), httpStatusCode);

        public static Result<TValue> CreateFailure<TValue>(
          Enum type,
          string message,
          int httpStatusCode = 400)
        {
            return new Result<TValue>(new Error(type, message), httpStatusCode);
        }
    }

    public class Result<TValue> : Result
    {
        public TValue Value { get; }

        public Result(TValue value) : base(true) => this.Value = value;

        public Result(Error error, int httpStatusCode = 400)
      : base(error, httpStatusCode)
        {
        }
    }
}