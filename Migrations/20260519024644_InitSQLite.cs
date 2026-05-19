using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HocTiengTrung.Migrations
{
    /// <inheritdoc />
    public partial class InitSQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaiHocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenBaiHoc = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiHocs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CauHoiGhepTus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CotBenTrai = table.Column<string>(type: "TEXT", nullable: true),
                    CotBenPhai = table.Column<string>(type: "TEXT", nullable: true),
                    BaiHocId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHoiGhepTus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CauHoiGhepTus_BaiHocs_BaiHocId",
                        column: x => x.BaiHocId,
                        principalTable: "BaiHocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CauHoiGhepTus_BaiHocId",
                table: "CauHoiGhepTus",
                column: "BaiHocId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHoiGhepTus");

            migrationBuilder.DropTable(
                name: "BaiHocs");
        }
    }
}
