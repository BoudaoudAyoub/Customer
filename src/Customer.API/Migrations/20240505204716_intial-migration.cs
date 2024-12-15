using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerMan.API.Migrations;

/// <inheritdoc />
public partial class Intialmigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "customerContext");

        migrationBuilder.CreateTable(
            name: "Customers",
            schema: "customerContext",
            columns: table => new
            {
                ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                Deleted = table.Column<bool>(type: "bit", nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                State = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                Street = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                Country = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                RestoredBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RestoredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.ID);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Customers",
            schema: "customerContext");
    }
}
