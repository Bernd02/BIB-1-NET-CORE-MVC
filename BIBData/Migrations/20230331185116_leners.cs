using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace BIBData.Migrations
{
	/// <inheritdoc />
	public partial class leners : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Leners",
				columns: table => new {
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Familienaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Geboortedatum = table.Column<DateTime>(type: "datetime2", nullable: false),
					TelefoonNr = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Leners", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Leners");
		}
	}
}