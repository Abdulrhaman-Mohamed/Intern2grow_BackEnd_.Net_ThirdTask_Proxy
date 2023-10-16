
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
            Console.WriteLine("Please Write First Endpoint in your URL Like ('/Books/Get') write Books Note: \n should URL end with Books/api/service in your controller \t ");
            string URL = Console.ReadLine();
            Console.WriteLine("Please Write Port Number");
            string Port = Console.ReadLine();

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
                    Port = $"{Port}"
                }
                ));
            

            app.Run();
        }

       
    }
}