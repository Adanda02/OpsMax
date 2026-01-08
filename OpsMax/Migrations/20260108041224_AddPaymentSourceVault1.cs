using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpsMax.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentSourceVault1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSourceDocuments_PaymentSources_PaymentSourceId",
                table: "PaymentSourceDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentSources",
                table: "PaymentSources");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "PaymentSources",
                newName: "AccountID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PaymentSources",
                newName: "AccountName");

            migrationBuilder.AlterColumn<int>(
                name: "AccountName",
                table: "PaymentSources",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "idPaymentSource",
                table: "PaymentSources",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Account",
                table: "PaymentSources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentSourceID",
                table: "PaymentSourceDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentSources",
                table: "PaymentSources",
                column: "idPaymentSource");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSourceDocuments_PaymentSources_PaymentSourceId",
                table: "PaymentSourceDocuments",
                column: "PaymentSourceId",
                principalTable: "PaymentSources",
                principalColumn: "idPaymentSource",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSourceDocuments_PaymentSources_PaymentSourceId",
                table: "PaymentSourceDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentSources",
                table: "PaymentSources");

            migrationBuilder.DropColumn(
                name: "idPaymentSource",
                table: "PaymentSources");

            migrationBuilder.DropColumn(
                name: "Account",
                table: "PaymentSources");

            migrationBuilder.DropColumn(
                name: "PaymentSourceID",
                table: "PaymentSourceDocuments");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "PaymentSources",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "PaymentSources",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PaymentSources",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentSources",
                table: "PaymentSources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSourceDocuments_PaymentSources_PaymentSourceId",
                table: "PaymentSourceDocuments",
                column: "PaymentSourceId",
                principalTable: "PaymentSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
