namespace Labo.API.Extensions
{
    public static class ResponseExtensions
    {
        public static void AddTotalHeader(this HttpResponse response, int count)
        {
            response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
            response.Headers.Add("X-Total-Count", count.ToString());
        }
    }
}
