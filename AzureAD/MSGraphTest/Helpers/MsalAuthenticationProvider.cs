using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;

namespace MSGraphTest
{
    // This class encapsulates the details of getting a token from MSAL and exposes it via the
    // IAuthenticationProvider interface so that GraphServiceClient or AuthHandler can use it.
    // A significantly enhanced version of this class will in the future be available from
    // the GraphSDK team.  It will supports all the types of Client Application as defined by MSAL.
    public class MsalAuthenticationProvider : IAuthenticationProvider
    {
        private IConfidentialClientApplication _clientApplication;
        private string[] _scopes;

        public MsalAuthenticationProvider(IConfidentialClientApplication clientApplication, string[] scopes)
        {
            _clientApplication = clientApplication;
            _scopes = scopes;
        }

        /// <summary>
        /// Update HttpRequestMessage with credentials
        /// </summary>
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var result = await _clientApplication.AcquireTokenForClient(_scopes).ExecuteAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);
        }
    }
}