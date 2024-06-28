
namespace Azure2Pulumi.Importer.Common
{
    public static class ResultBuilder
    {
        public static Result<T> Success<T>(T content) => new Result<T>(content, true, string.Empty);

        public static Result<T> Success<T>(T content, string message) => new Result<T>(content, true, message);

        public static Result<T> Failure<T>(string message) => new Result<T>(default(T), false, message);
        public static Result<T> Failure<T>(T content, string message) => new Result<T>(content, false, message);

    }
}
