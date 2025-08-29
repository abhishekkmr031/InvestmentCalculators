using Investment.FVCalculator.Api.StartupExtensions;

namespace Investment.FVCalculator.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add CORS policy
        builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
    });

        // Add services to the container.
        builder.Services.RegisterDependencies(); // we can pass IConfiguration also
        builder.Services.AddControllers();

        builder.Services.RateLimiter();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        //     
        // }
        app.UseSwagger();
        app.UseSwaggerUI();



        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthorization();


        app.MapControllers();
        app.UseRateLimiter();

        // ✅ Explicitly map OPTIONS requests to avoid 405
        //app.MapMethods("{*path}", new[] { "OPTIONS" }, () => Results.Ok());
        app.Run();
    }
}
