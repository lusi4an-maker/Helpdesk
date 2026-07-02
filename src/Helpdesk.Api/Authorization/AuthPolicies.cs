namespace Helpdesk.Api.Authorization;

public static class AuthorizationPolicies
{
    public static IServiceCollection AddHelpdeskPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("SoloAdmins", policy =>
                policy.RequireRole("Administrador", "Gerente"));

            options.AddPolicy("SoloPersonal", policy =>
                policy.RequireRole("Agente", "Analista", "Administrador", "Gerente"));

            options.AddPolicy("SoloCliente", policy =>
                policy.RequireRole("Cliente"));
        });

        return services;
    }
}