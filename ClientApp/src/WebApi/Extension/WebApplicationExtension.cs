using Carter;

namespace WebApi.Extension
{
    public static class WebApplicationExtension
    {
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("VisitorManagement");
                c.OAuthClientSecret("fc44e851-7303-4066-af84-6a68719944ed");
            });

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapCarter();

            return app;
        }
    }
}
