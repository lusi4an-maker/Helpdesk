namespace Helpdesk.Web.Services;

using System.Collections.Specialized;
using System.Security.Claims;
using System.Text;
using System;

using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;
using System.ComponentModel;

public class CustomAuthenticationStateProvider (ILocalStorageService storage) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal usuario;
        ClaimsPrincipal annonymusUser;
        ClaimsPrincipal main;

        var token = await storage.GetItemAsync("token");
        if (token is null)
        {
            //Si nadie esta autenticado, devuelvo un anonimo/vacio
            annonymusUser = new ClaimsPrincipal(new ClaimsIdentity());
            main = annonymusUser;
        } else
        {
            try
            {
                //Trato de parsear el token, lo guardo en lista para armar la autenticacion.
                var claims = ParseClaimsJwt(token);
                usuario = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt", ClaimTypes.NameIdentifier, ClaimTypes.Role));
                main = usuario;
            }
            catch (Exception ex)
            {
                annonymusUser = new ClaimsPrincipal(new ClaimsIdentity());
                main = annonymusUser;
            }
        }
        
        return new AuthenticationState(main);

    }

    private static List<Claim> ParseClaimsJwt(string token) 
    {
        var segments = token.Split('.');
        var payload = segments[1];
        payload = payload.Replace('-', '+');
        payload = payload.Replace('_', '/');

        if ((payload.Length % 4) != 0)
        {
            payload += new string('=', 4 - (payload.Length % 4));   
        }
        byte[] binaryData = Convert.FromBase64String(payload);
        var bytesAsJson = JsonDocument.Parse(binaryData);

        bytesAsJson.RootElement.EnumerateObject();

        List<Claim> claims = [];
        foreach (JsonProperty property in bytesAsJson.RootElement.EnumerateObject())
        {
            string name = property.Name;
            string value = property.Value.ToString();
            claims.Add(new Claim(name, value));
        }

        return claims;
    }

    public void NotifyAuthChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

}
