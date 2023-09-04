using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using QomekCore.Repository;
using QomekData;
using QomekData.Entities;

namespace QomekAppBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IQomekRepository<Blog>,QomekRepository<Blog>>();
            builder.Services.AddScoped<IQomekRepository<User>, QomekRepository<User>>();
            builder.Services.AddScoped<IQomekRepository<Comment>, QomekRepository<Comment>>();
            builder.Services.AddScoped<IBlogRepository,BlogRepository>();
            var conn =
                $"Server={Env.GetString("DB_HOST")};Port={Env.GetString("DB_PORT")};Database={Env.GetString("DB_DATABASE")};User ID={Env.GetString("DB_USERNAME")};Password={Env.GetString("DB_PASSWORD")};Include Error Detail=true";
            builder.Services.AddDbContext<QomekDbContext>((provider, options) =>
            {
                options.UseNpgsql(conn);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}