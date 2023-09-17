using Librar.DAL;
using Microsoft.EntityFrameworkCore;

namespace Library.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<LibraryContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDb")));

            builder.Services.AddControllers();

            var app = builder.Build();


            app.MapControllers();

            app.Run();
        }
    }
}