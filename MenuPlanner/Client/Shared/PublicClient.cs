using System.Net.Http;

namespace MenuPlanner.Client.Shared
{
    public class PublicClient
    {
        //https://chrissainty.com/avoiding-accesstokennotavailableexception-when-using-blazor-webassembly-hosted-template-with-individual-user-accounts/
        public HttpClient Client { get; }

        public PublicClient(HttpClient httpClient)
        {
            Client = httpClient;
        }
    }
}
