using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace timefree_training_ticketing.Migrations
{
    /// <inheritdoc />
    public partial class changeDateTypeTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ticketing-system");

            migrationBuilder.CreateTable(
                name: "ticket",
                schema: "ticketing-system",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    event_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    ticket_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    _deleted = table.Column<bool>(type: "bit", nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by_ip = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    date_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    modified_by_ip = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ticket__497F6CB4E07C527F", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "ticketing-system",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    _deleted = table.Column<bool>(type: "bit", nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by_ip = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    date_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    modified_by_ip = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__497F6CB44F81FB5C", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "ticketing-system",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    user_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ticket_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    _deleted = table.Column<bool>(type: "bit", nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getutcdate())"),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_by_ip = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    date_modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    modified_by_ip = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order__497F6CB4C4F04505", x => x.guid);
                    table.ForeignKey(
                        name: "FK_order_ticket",
                        column: x => x.ticket_guid,
                        principalSchema: "ticketing-system",
                        principalTable: "ticket",
                        principalColumn: "guid");
                    table.ForeignKey(
                        name: "FK_order_user",
                        column: x => x.user_guid,
                        principalSchema: "ticketing-system",
                        principalTable: "user",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_ticket_guid",
                schema: "ticketing-system",
                table: "order",
                column: "ticket_guid");

            migrationBuilder.CreateIndex(
                name: "IX_order_user_guid",
                schema: "ticketing-system",
                table: "order",
                column: "user_guid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order",
                schema: "ticketing-system");

            migrationBuilder.DropTable(
                name: "ticket",
                schema: "ticketing-system");

            migrationBuilder.DropTable(
                name: "user",
                schema: "ticketing-system");
        }
    }
}
