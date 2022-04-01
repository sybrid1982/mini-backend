using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBackend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColorFamily",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorFamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorFamily", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "MiniMeta",
                columns: table => new
                {
                    MetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Scale = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniMeta", x => x.MetaId);
                });

            migrationBuilder.CreateTable(
                name: "PaintBrand",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaintBrand", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearPublished = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoxArtUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiniMetaMetaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_MiniMeta_MiniMetaMetaId",
                        column: x => x.MiniMetaMetaId,
                        principalTable: "MiniMeta",
                        principalColumn: "MetaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paints",
                columns: table => new
                {
                    PaintId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorFamilyColorId = table.Column<int>(type: "int", nullable: false),
                    PaintBrandBrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paints", x => x.PaintId);
                    table.ForeignKey(
                        name: "FK_Paints_ColorFamily_ColorFamilyColorId",
                        column: x => x.ColorFamilyColorId,
                        principalTable: "ColorFamily",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paints_PaintBrand_PaintBrandBrandId",
                        column: x => x.PaintBrandBrandId,
                        principalTable: "PaintBrand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Minis",
                columns: table => new
                {
                    MiniId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MiniName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sculptor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minis", x => x.MiniId);
                    table.ForeignKey(
                        name: "FK_Minis_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MiniPaint",
                columns: table => new
                {
                    MinisMiniId = table.Column<int>(type: "int", nullable: false),
                    PaintsPaintId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiniPaint", x => new { x.MinisMiniId, x.PaintsPaintId });
                    table.ForeignKey(
                        name: "FK_MiniPaint_Minis_MinisMiniId",
                        column: x => x.MinisMiniId,
                        principalTable: "Minis",
                        principalColumn: "MiniId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MiniPaint_Paints_PaintsPaintId",
                        column: x => x.PaintsPaintId,
                        principalTable: "Paints",
                        principalColumn: "PaintId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_Photos_Minis_MiniId",
                        column: x => x.MiniId,
                        principalTable: "Minis",
                        principalColumn: "MiniId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_MiniMetaMetaId",
                table: "Games",
                column: "MiniMetaMetaId");

            migrationBuilder.CreateIndex(
                name: "IX_MiniPaint_PaintsPaintId",
                table: "MiniPaint",
                column: "PaintsPaintId");

            migrationBuilder.CreateIndex(
                name: "IX_Minis_GameId",
                table: "Minis",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Paints_ColorFamilyColorId",
                table: "Paints",
                column: "ColorFamilyColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Paints_PaintBrandBrandId",
                table: "Paints",
                column: "PaintBrandBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MiniId",
                table: "Photos",
                column: "MiniId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MiniPaint");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Paints");

            migrationBuilder.DropTable(
                name: "Minis");

            migrationBuilder.DropTable(
                name: "ColorFamily");

            migrationBuilder.DropTable(
                name: "PaintBrand");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "MiniMeta");
        }
    }
}
