namespace WpfApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Persons", "Name", c => c.String(maxLength: 2147483647,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UniqueAttribute",
                        new AnnotationValues(oldValue: "SQLite.CodeFirst.UniqueAttribute", newValue: null)
                    },
                }));
            AlterColumn("dbo.Persons", "Surname", c => c.String(nullable: false, maxLength: 2147483647,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UniqueAttribute",
                        new AnnotationValues(oldValue: null, newValue: "SQLite.CodeFirst.UniqueAttribute")
                    },
                }));
            AlterColumn("dbo.Persons", "Patronymic", c => c.String(maxLength: 2147483647,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UniqueAttribute",
                        new AnnotationValues(oldValue: "SQLite.CodeFirst.UniqueAttribute", newValue: null)
                    },
                }));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Persons", "Patronymic", c => c.String(maxLength: 2147483647,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UniqueAttribute",
                        new AnnotationValues(oldValue: null, newValue: "SQLite.CodeFirst.UniqueAttribute")
                    },
                }));
            AlterColumn("dbo.Persons", "Surname", c => c.String(maxLength: 2147483647,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UniqueAttribute",
                        new AnnotationValues(oldValue: "SQLite.CodeFirst.UniqueAttribute", newValue: null)
                    },
                }));
            AlterColumn("dbo.Persons", "Name", c => c.String(nullable: false, maxLength: 2147483647,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "UniqueAttribute",
                        new AnnotationValues(oldValue: null, newValue: "SQLite.CodeFirst.UniqueAttribute")
                    },
                }));
        }
    }
}
