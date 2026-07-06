namespace Helpdesk.Web.Services;
using System.Net.Http.Headers;



public class AuthHeaderHandler (ILocalStorageService storage) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await storage.GetItemAsync("token");

        //asigno el token si no es nulo, si es nulo lanzo excepcion
        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        var result = await base.SendAsync(request, cancellationToken);
        return result;
    }

}
