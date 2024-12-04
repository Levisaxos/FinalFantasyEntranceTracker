
using finalfantasyentrancetracker.api.Model;
using finalfantasyentrancetracker.api.UseCase.Items;
using finalfantasyentrancetracker.api.UseCase.SQL;

namespace finalfantasyentrancetracker.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ConfigConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.AddTransient<DropDatabaseUseCase>();
            builder.Services.AddTransient<CreateDatabaseUseCase>();
            builder.Services.AddTransient<FillDatabaseUseCase>();
            builder.Services.AddTransient<GetItemsUseCase>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
