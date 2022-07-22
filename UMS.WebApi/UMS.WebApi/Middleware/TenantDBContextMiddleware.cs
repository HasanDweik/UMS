namespace UMS.WebApi.Middleware;

public class TenantDBContextMiddleware
{
    private readonly RequestDelegate _next;

    public TenantDBContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public void Invoke(HttpContext context)
    {
        switch (context.Request.Headers["tenant"])
        {
            case "low":
                
                break;
            case "regular":
                break;
            case "premium":
                break;
            default: 
                break;
        }
    }
}