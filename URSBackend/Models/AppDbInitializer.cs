using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace URSBackend.Models
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var services = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = services.ServiceProvider.GetService<ApplicationDbContext>();

                context.Users.AddRange(new User() { 
                    Username= "admin",
                    Password ="admin",
                    Name = "Administrator"
                
                },
                new User()
                {
                    Username = "registrar",
                    Password = "registrar",
                    Name = "Registrar"
                });
                context.SaveChanges(); 
            }
        }
    }
}