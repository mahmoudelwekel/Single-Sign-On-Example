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
                c.OAuthClientId("DigitalEye");
                c.OAuthClientSecret("d1735c79-d054-430f-a98a-d398719c70a7");
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
