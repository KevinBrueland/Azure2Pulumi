
namespace Azure2Pulumi.Importer.Common
{
    public readonly struct Result<T> : IEquatable<Result<T>>
    {
        public Result(T content, bool isSuccess, string message)
        {
            Content = content;
            IsSuccess = isSuccess;
            Message = message;
        }

        public Result(T content, string message)
        {
            Content = content;
            IsSuccess = message == null;
            Message = message;
        }
        public Result(bool isSuccess, string message)
        {
            Content = default;
            IsSuccess = isSuccess;
            Message = message;
        }

        public T Content { get; }

        public bool IsSuccess { get; }

        public string Message { get; }

        public static implicit operator Result<T>(T content)
        {
            return new Result<T>(content, true, null);
        }

        public static implicit operator Result<T>(string message)
        {
            return new Result<T>(default, false, message);
        }

        public static bool operator ==(Result<T> left, Result<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Result<T> left, Result<T> right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Result<T> result && Equals(result);
        }

        public bool Equals(Result<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Content, other.Content);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Content);
        }
    }
}
