namespace ShoesEShop.Handler.Shared
{
    public class BaseResult
    {
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
    }

    public class BaseResult<T> : BaseResult
    {
        public T Data { get; set; }
    }
}
