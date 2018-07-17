using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekHunters.Migrations.SqlServerMigrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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

            migrationBuilder.CreateTable(
                name: "CandidateSkills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CandidateId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSkills_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_CandidateId",
                table: "CandidateSkills",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_SkillId",
                table: "CandidateSkills",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateSkills");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
