using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DogApp.Іnfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Creating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dogs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    color = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    tail_length = table.Column<double>(type: "float", nullable: false),
                    weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dogs", x => x.id);
                    table.CheckConstraint("ck_tail_length", "tail_length>=0 and tail_length<=100");
                    table.CheckConstraint("ck_weight", "weight>0 and weight<=150");
                });

            migrationBuilder.InsertData(
                table: "dogs",
                columns: new[] { "id", "color", "name", "tail_length", "weight" },
                values: new object[,]
                {
                    { new Guid("522c2af8-fa09-4a09-8688-ad82ce28e96d"), "black", "Alice", 6.0, 15.0 },
                    { new Guid("533df35a-8a05-4cc2-addd-584ed7bd9de6"), "brown", "Neo", 20.0, 44.700000000000003 },
                    { new Guid("8211c646-1be7-467b-8818-0333024ae862"), "black & brown", "Richard", 20.0, 40.700000000000003 },
                    { new Guid("8f7b4b2e-1cad-4f59-bbc0-0d188146a65e"), "black", "David", 35.0, 40.0 },
                    { new Guid("90069a2c-2363-4873-8c86-61b08663b1c1"), "brown & white", "Jessy", 23.300000000000001, 37.600000000000001 },
                    { new Guid("ab791748-e52f-4dd0-85ed-e981b09b92d2"), "grey", "Norman", 17.0, 55.0 },
                    { new Guid("c61af73e-68fb-4b30-9e45-e3362ed4f378"), "brown & white", "Bob", 15.0, 30.300000000000001 },
                    { new Guid("cbdf6c20-5512-4fa6-a809-aa2d18ef3177"), "white", "Jeck", 10.0, 20.5 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_dogs_name",
                table: "dogs",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dogs");
        }
    }
}
