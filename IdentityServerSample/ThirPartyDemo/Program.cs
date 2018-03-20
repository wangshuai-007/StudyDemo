using System;
using System.Net.Http;
using IdentityModel.Client;

namespace ThirPartyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var diso = DiscoveryClient.GetAsync("http://localhost:5000").Result;

            if (diso.IsError)
            {
                Console.WriteLine(diso.Error);
            }

            var tokenClient = new TokenClient(diso.TokenEndpoint, "client", "secret");
            var tokenRespose = tokenClient.RequestClientCredentialsAsync("api").Result;

            if (tokenRespose.IsError)
            {
                Console.WriteLine(tokenRespose.ErrorDescription);
            }
            else
            {
                Console.WriteLine(tokenRespose.Json);
            }

            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenRespose.AccessToken);

            var respose = httpClient.GetAsync("http://localhost:5001/api/values").Result;

            if (respose.IsSuccessStatusCode)
            {
                Console.WriteLine(respose.Content.ReadAsStringAsync().Result);
            }
        


        Console.ReadKey();
        }
    }
}
