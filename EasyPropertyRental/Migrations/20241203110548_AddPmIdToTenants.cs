using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPropertyRental.Migrations
{
    /// <inheritdoc />
    public partial class AddPmIdToTenants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginViewModel",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginViewModel", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "propertyOwners",
                columns: table => new
                {
                    po_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__property__368DA7F071EEF4CF", x => x.po_id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationViewModel",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationViewModel", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "propertyManagers",
                columns: table => new
                {
                    pm_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    po_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__property__26B1033612BE5E19", x => x.pm_id);
                    table.ForeignKey(
                        name: "FK__propertyM__po_id__3C69FB99",
                        column: x => x.po_id,
                        principalTable: "propertyOwners",
                        principalColumn: "po_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "buildings",
                columns: table => new
                {
                    building_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    pm_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__building__9C9FBF7F17E45386", x => x.building_id);
                    table.ForeignKey(
                        name: "FK__buildings__pm_id__3F466844",
                        column: x => x.pm_id,
                        principalTable: "propertyManagers",
                        principalColumn: "pm_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "apartments",
                columns: table => new
                {
                    apartment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    building_id = table.Column<int>(type: "int", nullable: true),
                    unit_number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    floor = table.Column<int>(type: "int", nullable: true),
                    bedrooms = table.Column<int>(type: "int", nullable: true),
                    bathrooms = table.Column<int>(type: "int", nullable: true),
                    rent = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    is_available = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__apartmen__DC51C2EC93EA0B8E", x => x.apartment_id);
                    table.ForeignKey(
                        name: "FK__apartment__build__4316F928",
                        column: x => x.building_id,
                        principalTable: "buildings",
                        principalColumn: "building_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tenants",
                columns: table => new
                {
                    tenant_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    apartment_id = table.Column<int>(type: "int", nullable: true),
                    pm_id = table.Column<int>(type: "int", nullable: true),
                    move_in_date = table.Column<DateOnly>(type: "date", nullable: true),
                    move_out_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tenants__D6F29F3E48711940", x => x.tenant_id);
                    table.ForeignKey(
                        name: "FK__tenants__apartme__46E78A0C",
                        column: x => x.apartment_id,
                        principalTable: "apartments",
                        principalColumn: "apartment_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__tenants__pm_id__47E78A0D",
                        column: x => x.pm_id,
                        principalTable: "propertyManagers",
                        principalColumn: "pm_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    appointment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenant_id = table.Column<int>(type: "int", nullable: true),
                    pm_id = table.Column<int>(type: "int", nullable: true),
                    appointment_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__appointm__A50828FC335BED9C", x => x.appointment_id);
                    table.ForeignKey(
                        name: "FK__appointme__pm_id__4BAC3F29",
                        column: x => x.pm_id,
                        principalTable: "propertyManagers",
                        principalColumn: "pm_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK__appointme__tenan__4AB81AF0",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "tenant_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    sender_id = table.Column<int>(type: "int", nullable: false),
                    receiver_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    receiver_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sent_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__messages__0BBF6EE6F1BBF413", x => x.message_id);
                    table.ForeignKey(
                        name: "FK__messages__receiv__59FA5E80",
                        column: x => x.receiver_id,
                        principalTable: "tenants",
                        principalColumn: "tenant_id");
                    table.ForeignKey(
                        name: "FK__messages__receiv__5AEE82B9",
                        column: x => x.receiver_id,
                        principalTable: "propertyManagers",
                        principalColumn: "pm_id");
                    table.ForeignKey(
                        name: "FK__messages__receiv__5BE2A6F2",
                        column: x => x.receiver_id,
                        principalTable: "propertyOwners",
                        principalColumn: "po_id");
                    table.ForeignKey(
                        name: "FK__messages__sender__571DF1D5",
                        column: x => x.sender_id,
                        principalTable: "tenants",
                        principalColumn: "tenant_id");
                    table.ForeignKey(
                        name: "FK__messages__sender__5812160E",
                        column: x => x.sender_id,
                        principalTable: "propertyManagers",
                        principalColumn: "pm_id");
                    table.ForeignKey(
                        name: "FK__messages__sender__59063A47",
                        column: x => x.sender_id,
                        principalTable: "propertyOwners",
                        principalColumn: "po_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_apartments_building_id",
                table: "apartments",
                column: "building_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_pm_id",
                table: "appointments",
                column: "pm_id");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_tenant_id",
                table: "appointments",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_buildings_pm_id",
                table: "buildings",
                column: "pm_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_receiver_id",
                table: "messages",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_sender_id",
                table: "messages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_propertyManagers_po_id",
                table: "propertyManagers",
                column: "po_id");

            migrationBuilder.CreateIndex(
                name: "UQ__property__AB6E6164FD60D7AF",
                table: "propertyManagers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__property__AB6E61649182340E",
                table: "propertyOwners",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tenants_apartment_id",
                table: "tenants",
                column: "apartment_id");

            migrationBuilder.CreateIndex(
                name: "IX_tenants_pm_id",
                table: "tenants",
                column: "pm_id");

            migrationBuilder.CreateIndex(
                name: "UQ__tenants__AB6E6164130DA31B",
                table: "tenants",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "LoginViewModel");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "RegistrationViewModel");

            migrationBuilder.DropTable(
                name: "tenants");

            migrationBuilder.DropTable(
                name: "apartments");

            migrationBuilder.DropTable(
                name: "buildings");

            migrationBuilder.DropTable(
                name: "propertyManagers");

            migrationBuilder.DropTable(
                name: "propertyOwners");
        }
    }
}
