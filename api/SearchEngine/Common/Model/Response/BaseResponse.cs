namespace SearchEngine.Common.Model.Response
{
    public abstract class BaseResponse<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }
    }
}
