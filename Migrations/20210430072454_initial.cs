﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PerfectTunes.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(maxLength: 200, nullable: false),
                    ProductLine = table.Column<string>(maxLength: 200, nullable: false),
                    LogoImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Instruments",
                columns: table => new
                {
                    InstrumentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    DepartmentId = table.Column<string>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    LogoImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instruments", x => x.InstrumentId);
                    table.ForeignKey(
                        name: "FK_Instruments_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instruments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "BrandName", "LogoImage", "ProductLine" },
                values: new object[,]
                {
                    { 1, "Fender Stratocaster", "Fender.jpg", "Guitars" },
                    { 2, "Yamaha", "Yamaha.jpg", "Orchestra" },
                    { 3, "Ludwig", "Ludwig.jpg", "Drum Sets" },
                    { 4, "Latin Percussion", "LatinPercussion.jpg", "Hand Percussion" },
                    { 5, "Korg", "Korg.jpg", "Synthesizers" },
                    { 6, "Jean Paul USA", "JeanPaul.jpg", "Band" },
                    { 7, "Gemeinhardt", "Gemeinhardt.jpg", "Orchestra" },
                    { 8, "Kaizer", "Kaizer.jpg", "Band" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Name" },
                values: new object[,]
                {
                    { "strings", "Strings" },
                    { "percussion", "Percussion" },
                    { "keys", "Keys" },
                    { "brass", "Brass" },
                    { "woodwinds", "Woodwinds" },
                    { "gear", "Gear" }
                });

            migrationBuilder.InsertData(
                table: "Instruments",
                columns: new[] { "InstrumentId", "BrandId", "DepartmentId", "LogoImage", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "strings", "Squire.jpg", "Squier by StratoCaster", 219.99000000000001 },
                    { 2, 2, "strings", "V3.jpg", "V3 Student Violin", 799.99000000000001 },
                    { 3, 3, "percussion", "QuestLove.jpg", "Questlove Pocket Kit", 269.99000000000001 },
                    { 4, 4, "percussion", "CitySeries.jpg", "City Series Conga", 349.99000000000001 },
                    { 5, 2, "keys", "P-45.jpg", "P-45 88-Key Digital Piano", 499.99000000000001 },
                    { 6, 5, "keys", "MicroKorg.jpg", "MicroKorg Synthesizer", 399.99000000000001 },
                    { 7, 6, "brass", "AS-400.jpg", "AS-400 Alto Saxophone", 499.99000000000001 },
                    { 9, 8, "brass", "C-Series-3000.jpg", "C-Series (3000) Trumpet", 299.99000000000001 },
                    { 8, 7, "woodwinds", "2SP.jpg", "2SP Student Flute", 479.99000000000001 },
                    { 10, 6, "woodwinds", "CL-300.jpg", "CL-300 Student Clarinet", 199.99000000000001 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_BrandId",
                table: "Instruments",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Instruments_DepartmentId",
                table: "Instruments",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instruments");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
