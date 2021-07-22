using System.Data.Entity;



namespace TestePratico_Odair.Models
{
    public class TestePratico_OdairContext: DbContext
    {

        public TestePratico_OdairContext() : base("DefaultConnection")
        {



        }

        public System.Data.Entity.DbSet<TestePratico_Odair.Models.Users> Users { get; set; }
    }
}