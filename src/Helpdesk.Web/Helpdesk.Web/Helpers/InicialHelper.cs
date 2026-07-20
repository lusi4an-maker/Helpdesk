namespace Helpdesk.Web.Helpers;

public static class InicialHelper
{
    public static string InicialesHelper(string? nombreAgente)
    {
        if (string.IsNullOrWhiteSpace(nombreAgente))
        {
            return "?";
        }
        else
        {
            var partes = nombreAgente.Split(' ');
            char primera = partes[0][0];
            if (partes.Length > 1)
            {
                char segunda = partes[1][0];
                return $"{primera}{segunda}".ToUpper();
            }
            else 
            { 
                return $"{primera}".ToUpper();
            }
            
        }
    }
}
