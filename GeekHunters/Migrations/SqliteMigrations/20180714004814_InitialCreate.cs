using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekHunters.Migrations.SqliteMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });
            migrationBuilder.Sql("INSERT INTO Candidates (FirstName,LastName) VALUES ('Jon','Snow');");
            migrationBuilder.Sql("INSERT INTO Candidates (FirstName,LastName) VALUES ('Daenerys','Tangaryen');");
            migrationBuilder.Sql("INSERT INTO Candidates (FirstName,LastName) VALUES ('Cersei','Lannister');");
            migrationBuilder.Sql("INSERT INTO Candidates (FirstName,LastName) VALUES ('Sansa','Stark');");
            migrationBuilder.Sql("INSERT INTO Candidates (FirstName,LastName) VALUES ('Arya','Stark');");
            migrationBuilder.Sql("INSERT INTO Candidates (FirstName,LastName) VALUES ('Tyrion','Lannister');");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });
            migrationBuilder.Sql("INSERT INTO Skills (Name) VALUES ('SQL');");
            migrationBuilder.Sql("INSERT INTO Skills (Name) VALUES ('JavaScript');");
            migrationBuilder.Sql("INSERT INTO Skills (Name) VALUES ('C#');");
            migrationBuilder.Sql("INSERT INTO Skills (Name) VALUES ('Java');");
            migrationBuilder.Sql("INSERT INTO Skills (Name) VALUES ('Python');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
