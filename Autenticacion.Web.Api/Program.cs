using Autenticacion.Web.Api.Modules.Authentication;
using Autenticacion.Web.Api.Modules.Feature;
using Autenticacion.Web.Api.Modules.HealthChecks;
using Autenticacion.Web.Api.Modules.Injection;
using Autenticacion.Web.Api.Modules.Mapper;
using Autenticacion.Web.Api.Modules.Swagger;
using Autenticacion.Web.Api.Modules.Validator;
using Autenticacion.Web.Api.Modules.Versioning;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Autenticacion.Web.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddControllers().AddNewtonsoftJson(options =>
        //{
        //    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(); //de esta forma indicamos que en el proceso
        //                                                                                                               //de serializar y deserializar objetos json va a tomar la resolucion predeterminada, pero se puede usar otro tipo de resolucion, segun la necesidad,
        //                                                                                                               //por ejemplo camelcase.  options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        //});

        builder.Services.AddVersioning();
        builder.Services.AddAuthentication(builder.Configuration);
       // builder.Services.AddMapper();
        builder.Services.AddFeature(builder.Configuration); //las cors
        builder.Services.AddValidator();
       // builder.Services.AddHealthCheck(builder.Configuration);
        //builder.Services.AddControllers();
        builder.Services.AddInjection(builder.Configuration);
        builder.Services.AddSwaggerDocumentation();

        //builder.Services.AddCors(option =>
        //{
        //    option.AddPolicy("policyApi", builder =>
        //    {
        //        builder.AllowAnyOrigin()
        //        .AllowAnyMethod()
        //        .AllowAnyHeader();
        //    });
        //});


        var app = builder.Build();

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {

            app.UseDeveloperExceptionPage();
            app.UseSwagger(); //habilitamos el middleware para servir al swagger generated como un endpoint json
            app.UseSwaggerUI( // habilitamos el dashboard de swagger 
                c =>
                {
                    //SwaggerEndpoint ese metodo recibe dos parametros, el primero es la url, el segundo es el nombre del endpoint
                    //  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi Api Empresarial v1");

                    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
     //   app.UseCors("policyApi");
        app.UseAuthorization();
        app.MapControllers();
      //  app.MapHealthChecksUI();

        //con este endpoint el cliente puede consumir en tiempo real el estado de salud del microservicio ya que se actualiza cada 5 segundo en tiempo real
        //app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        //{
        //    //como primer parametro colocamos el path del endpoint y como segundo parametro especificamos la estructura de respuesta
        //    Predicate = _ => true,
        //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

        //});
        app.Run();

    }
}
