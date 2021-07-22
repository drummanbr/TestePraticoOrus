using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web.Configuration;
using TestePratico_Odair.Models;

namespace TestePratico_Odair.Class
{
    public class UserHelper
    {

        public class UsersHelper : IDisposable
        {
            private static ApplicationDbContext userContext = new ApplicationDbContext();
            private static TestePratico_OdairContext db = new TestePratico_OdairContext();




            //Deletar Usuários

            public static bool DeleteUser(string UserName)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var userASP = userManager.FindByEmail(UserName);
                if (userASP == null)
                {
                    return false;
                }
                var response = userManager.Delete(userASP);
                return response.Succeeded;

            }

            // Atualizar Usuários
            public static bool UpdateUser(string currentUserName, string newUserName)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var userASP = userManager.FindByEmail(currentUserName);
                
                if (userASP == null)
                {

                    return false;
                }

                userASP.UserName = newUserName;
                userASP.Email = newUserName;
              


                var response = userManager.Update(userASP);
               

                return response.Succeeded;

            }

            internal static void CreateUserASP(object userName, string v)
            {
                throw new NotImplementedException();
            }

            public static void CheckRole(string roleName)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

                // Verificar se o Role existe, se não existe cria
                if (!roleManager.RoleExists(roleName))
                {
                    roleManager.Create(new IdentityRole(roleName));
                }
            }

            public static void CheckSuperUser()
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
                var email = WebConfigurationManager.AppSettings["AdminUser"];
                var password = WebConfigurationManager.AppSettings["AdminPassWord"];
                var userASP = userManager.FindByName(email);
                if (userASP == null)
                {
                    CreateUserASP(email, "Admin", password);
                    return;
                }

                userManager.AddToRole(userASP.Id, "Admin");
            }
            public static void CreateUserASP(string email, string roleName)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

                var userASP = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                };

                userManager.Create(userASP, email);
                userManager.AddToRole(userASP.Id, roleName);


               
        }

            public static void CreateUserASP(string email, string roleName, string password)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

                var userASP = new ApplicationUser
                {
                    Email = email,
                    UserName = email,


                };

                userManager.Create(userASP, password);
                userManager.AddToRole(userASP.Id, roleName);
            }

            
            public void Dispose()
            {
                userContext.Dispose();
                db.Dispose();
            }
        }

       
    }
}