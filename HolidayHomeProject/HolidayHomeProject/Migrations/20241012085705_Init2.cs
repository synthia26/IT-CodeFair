using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HolidayHomeProject.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    HostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.HostId);
                    table.ForeignKey(
                        name: "FK_Hosts_UserAccounts_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    HouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPeopleNo = table.Column<int>(type: "int", nullable: false),
                    RentPricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThumbnailImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseImagesPaths = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyImage1Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyImage2Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPermissionRequired = table.Column<bool>(type: "bit", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.HouseId);
                    table.ForeignKey(
                        name: "FK_Houses_Hosts_HostId",
                        column: x => x.HostId,
                        principalTable: "Hosts",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hosts_UserAccountId",
                table: "Hosts",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_HostId",
                table: "Houses",
                column: "HostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Hosts");
        }
    }
}
