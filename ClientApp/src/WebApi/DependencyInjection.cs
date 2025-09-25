using Carter;
using Domain.Entities.OrderDomain;
using Duende.AspNetCore.Authentication.OAuth2Introspection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;

namespace WebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddCarter();

            services.AddOpenIddict()
                .AddValidation(options =>
                {
                    options.SetIssuer("https://localhost:7000/");
                    options.AddAudiences("VisitorManagement");

                    options.UseIntrospection()
                                   .SetClientId("VisitorManagement")
                                   .SetClientSecret("fc44e851-7303-4066-af84-6a68719944ed");

 
                    options.UseAspNetCore();
                    options.UseSystemNetHttp();

                }); 


            services.AddAuthentication("Bearer").AddOAuth2Introspection("Bearer", options =>
                {
                    options.Authority = "https://localhost:7000/";
                    options.ClientId = "VisitorManagement";
                    options.ClientSecret = "fc44e851-7303-4066-af84-6a68719944ed"; // only if required
                });


            //services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            services.AddAuthorization();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:7000/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:7000/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "CreateOrders", "VisitorManagement.Orders" },
                                { "ReadOrders", "VisitorManagement.Orders" },
                                { "DeleteOrders", "VisitorManagement.Orders" }
                            }
                        },
                    }
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
