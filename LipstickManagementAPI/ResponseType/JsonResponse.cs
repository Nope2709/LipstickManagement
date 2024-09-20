namespace HotPotToYou.Controllers.ResponseType
{
    public class JsonResponse<T>
    {
        public JsonResponse(T value)
        {
            Value = value;
        }
        public T Value { get; set; }
    }
}
