
namespace EsteroidesToDo.Application.Common
{
    public class OperationResult<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public string Error { get; }

        private OperationResult(T value,bool isSuccess, string error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static OperationResult<T> Success(T value) => new OperationResult<T>(value,true, null);
        public static OperationResult<T> Failure(string error) => new OperationResult<T>(default,false, error);
    }

}
