using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DogApp.Іnfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatingDb : Migration
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
                    { new Guid("1a20c33e-a3b8-4f8c-ace5-c1751f7de304"), "black", "Alice", 6.0, 15.0 },
                    { new Guid("2c042acc-38bd-461d-a489-07ee786518da"), "brown & white", "Bob", 15.0, 30.300000000000001 },
                    { new Guid("303bc3ba-fb9a-4404-8343-9612c89662a2"), "black", "David", 35.0, 40.0 },
                    { new Guid("5193adb0-af3e-4610-9eeb-5c5133393012"), "black & brown", "Richard", 20.0, 40.700000000000003 },
                    { new Guid("768677ef-648e-4a9b-b2dd-958792c3acac"), "grey", "Norman", 17.0, 55.0 },
                    { new Guid("b2ad8106-01e4-4b27-9d7a-aeed1df3693c"), "brown", "Neo", 20.0, 44.700000000000003 },
                    { new Guid("e2d1340f-3401-46e7-96ef-d1f2fe8c09a9"), "brown & white", "Jessy", 23.300000000000001, 37.600000000000001 },
                    { new Guid("e4783b6a-9c04-4d57-a010-3351c877dd6c"), "white", "Jeck", 10.0, 20.5 }
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
