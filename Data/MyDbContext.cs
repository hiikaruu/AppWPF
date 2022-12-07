using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SQLite.CodeFirst;

namespace WpfApp.Data
{
    public class MyDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<MyDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);

            var model = modelBuilder.Build(Database.Connection);
            ISqlGenerator sqlGenerator = new SqliteSqlGenerator();
            _ = sqlGenerator.Generate(model.StoreModel);
                
        }

        public DbSet<Tables.Person> Persons { get; set; }

    }

}
