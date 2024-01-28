using RestSharp;

namespace BankSystemApi.Utils
{
    public static class Helper
    {
        public static RestResponse SendRequest(string endpoint, Method method, object requestBody = null)
        {
            var client = new RestClient("https://www.localhost:8080/api/");
            var request = new RestRequest(endpoint, method);

            if (requestBody != null)
            {
                request.AddJsonBody(requestBody);
            }

            return client.Execute(request);
        }
    }
}
