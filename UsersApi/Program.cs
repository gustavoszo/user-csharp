
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsersApi.Data;
using UsersApi.Models;

namespace UsersApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("dbConnection");

            builder.Services.AddDbContext<UserDbContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            //  Esta linha configura o Identity do ASP.NET Core, que � um sistema completo para gerenciamento de autentica��o e autoriza��o. Ele lida com usu�rios, roles (fun��es), autentica��o, gerenciamento de senhas, entre outros.
            builder.Services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>() //  Essa linha diz ao Identity para usar o UserDbContext para armazenar os dados do Identity no banco de dados. Ou seja, ele vai usar o UserDbContext para criar tabelas como AspNetUsers (para usu�rios), AspNetRoles (para roles), AspNetUserRoles (associa��o de usu�rios e roles), etc.
                .AddDefaultTokenProviders();
                
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();            builder.Services.AddSwaggerGen();

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
