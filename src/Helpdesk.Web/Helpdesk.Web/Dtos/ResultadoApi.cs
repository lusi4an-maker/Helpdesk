namespace Helpdesk.Web.Dtos;

public record ResultadoApi(bool Exito,string? Error);
public record ResultadoApi<T>(bool Exito, T? Data, string? Error);