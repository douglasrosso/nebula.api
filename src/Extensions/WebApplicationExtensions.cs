namespace nebula.api.src.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseGlobalExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(err => err.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message = "Erro interno do servidor." });
            }));

            return app;
        }
    }
}
