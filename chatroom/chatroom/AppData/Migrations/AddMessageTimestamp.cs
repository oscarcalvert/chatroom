using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddTimestampToMessages : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "Timestamp",
            table: "Messages",
            type: "datetime2",
            nullable: false,
            defaultValueSql: "GETDATE()"
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Timestamp",
            table: "Messages");
    }
}