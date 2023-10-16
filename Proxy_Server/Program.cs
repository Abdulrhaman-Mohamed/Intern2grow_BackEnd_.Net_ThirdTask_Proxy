
namespace Proxy_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string URL = Console.ReadLine();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.MapWhen(o => o.Request.Path.StartsWithSegments($"/{URL}/api"), b => b.RunProxy(
                new ProxyOptions
                {
                    Scheme = "https",
                    Host = "localhost",
                    Port = "44300"
                }
                ));
            

            app.Run();
        }

       
    }
}