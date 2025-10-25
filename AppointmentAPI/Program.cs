
using Application.Extensions.AppExts;
using AppointmentAPI.Converters;
using Carter;
using Infrastructure;

namespace AppointmentAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureHttpJsonOptions(opts =>
            {
                opts.SerializerOptions.Converters.Add(new TimeOnlyConverter());
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });


            builder.Services.AddCarter();
            builder.Services.AddInfastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            app.UseCors("AllowFrontend");
            app.MapCarter();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/openapi/v1.json", "demo API");
                });
                app.MapOpenApi();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.Run();
        }
    }
}
