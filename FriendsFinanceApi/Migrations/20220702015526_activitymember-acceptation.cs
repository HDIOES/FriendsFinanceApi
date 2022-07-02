using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FriendsFinanceApi.Migrations
{
    public partial class activitymemberacceptation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ActivityAcceptedByUser",
                table: "ActivityMembers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityAcceptedByUser",
                table: "ActivityMembers");
        }
    }
}
