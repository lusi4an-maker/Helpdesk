namespace Helpdesk.Web.Dtos;

public record ValidationProblem(Dictionary<string, string[]>? Errors);