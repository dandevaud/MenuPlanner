using System.Net.Http;

namespace MenuPlanner.Client.Shared
{
    /// <summary>Public Http Client used for all Request done by a non-authenticated user</summary>
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
