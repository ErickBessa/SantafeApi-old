using Microsoft.EntityFrameworkCore.Migrations;

namespace SantafeApi.Migrations
{
    public partial class intial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "hasAccess",
                table: "AspNetUsers",
                newName: "HasAccess");

            migrationBuilder.AlterDatabase(
                collation: "Latin1_General_CI_AS");

            migrationBuilder.AddColumn<int>(
                name: "CodCliente",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            // migrationBuilder.CreateTable(
            //     name: "tbCliente",
            //     columns: table => new
            //     {
            //         CodCliente = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         NomeCliente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         CnpjCliente = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
            //         TecResponsavel = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         EnderecoCliente = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
            //         TipoDoLocal = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //         Email = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
            //         Telefone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //         CodUsuario = table.Column<int>(type: "int", nullable: false),
            //         DataCad = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_tbCliente", x => x.CodCliente);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "tbItemsVistoria",
            //     columns: table => new
            //     {
            //         CodItemVis = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         CodLocal = table.Column<int>(type: "int", nullable: false),
            //         NomeLocal = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         CodItem = table.Column<int>(type: "int", nullable: false),
            //         CodCliente = table.Column<int>(type: "int", nullable: false),
            //         NomeItemVis = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         ParamItem = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //     });

            // migrationBuilder.CreateTable(
            //     name: "tbItens",
            //     columns: table => new
            //     {
            //         CodItem = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         NomeItem = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         Norma = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
            //         CodUsuario = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_tbItens", x => x.CodItem);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "tbLocal",
            //     columns: table => new
            //     {
            //         CodLocal = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         CodCliente = table.Column<int>(type: "int", nullable: false),
            //         NomeLocal = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_tbLocal", x => x.CodLocal);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "tbLocalItem",
            //     columns: table => new
            //     {
            //         CodLocalItem = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         CodCliente = table.Column<int>(type: "int", nullable: false),
            //         CodItem = table.Column<int>(type: "int", nullable: false),
            //         NomeLocalItem = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_tbLocalItem", x => x.CodLocalItem);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "tbControleOs",
            //     columns: table => new
            //     {
            //         Cod = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         CodCliente = table.Column<int>(type: "int", nullable: false),
            //         CodUsuario = table.Column<int>(type: "int", nullable: false),
            //         Data = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_tbControleOs", x => x.Cod);
            //         table.ForeignKey(
            //             name: "FK_tbControleOs_tbCliente",
            //             column: x => x.CodCliente,
            //             principalTable: "tbCliente",
            //             principalColumn: "CodCliente",
            //             onDelete: ReferentialAction.Restrict);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "tbStatus",
            //     columns: table => new
            //     {
            //         CodStatus = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         CodItem = table.Column<int>(type: "int", nullable: false),
            //         NomeStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         Gravidade = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.ForeignKey(
            //             name: "FK_tbStatus_tbItens",
            //             column: x => x.CodItem,
            //             principalTable: "tbItens",
            //             principalColumn: "CodItem",
            //             onDelete: ReferentialAction.Restrict);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "tbVistoria",
            //     columns: table => new
            //     {
            //         Cod = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         CodControle = table.Column<int>(type: "int", nullable: false),
            //         CodCliente = table.Column<int>(type: "int", nullable: false),
            //         NomeCliente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         TipoLocal = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //         CodLocal = table.Column<int>(type: "int", nullable: false),
            //         NomeLocal = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         CodItem = table.Column<int>(type: "int", nullable: false),
            //         NomeItem = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         Param = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         CodStatus = table.Column<int>(type: "int", nullable: false),
            //         NomeStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //         Conformidade = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
            //         Gravidade = table.Column<int>(type: "int", nullable: false),
            //         Medidas = table.Column<string>(type: "varchar(400)", unicode: false, maxLength: 400, nullable: false),
            //         NomeImg = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.ForeignKey(
            //             name: "FK_tbVistoria_tbControleOs1",
            //             column: x => x.CodControle,
            //             principalTable: "tbControleOs",
            //             principalColumn: "Cod",
            //             onDelete: ReferentialAction.Restrict);
            //         table.ForeignKey(
            //             name: "FK_tbVistoria_tbItens",
            //             column: x => x.CodItem,
            //             principalTable: "tbItens",
            //             principalColumn: "CodItem",
            //             onDelete: ReferentialAction.Restrict);
            //         table.ForeignKey(
            //             name: "FK_tbVistoria_tbLocal",
            //             column: x => x.CodLocal,
            //             principalTable: "tbLocal",
            //             principalColumn: "CodLocal",
            //             onDelete: ReferentialAction.Restrict);
            //     });

            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUsers_CodCliente",
            //     table: "AspNetUsers",
            //     column: "CodCliente",
            //     unique: true,
            //     filter: "[CodCliente] IS NOT NULL");

            // migrationBuilder.CreateIndex(
            //     name: "IX_tbControleOs_CodCliente",
            //     table: "tbControleOs",
            //     column: "CodCliente");

            // migrationBuilder.CreateIndex(
            //     name: "IX_tbStatus_CodItem",
            //     table: "tbStatus",
            //     column: "CodItem");

            // migrationBuilder.CreateIndex(
            //     name: "IX_tbVistoria_CodControle",
            //     table: "tbVistoria",
            //     column: "CodControle");

            // migrationBuilder.CreateIndex(
            //     name: "IX_tbVistoria_CodItem",
            //     table: "tbVistoria",
            //     column: "CodItem");

            // migrationBuilder.CreateIndex(
            //     name: "IX_tbVistoria_CodLocal",
            //     table: "tbVistoria",
            //     column: "CodLocal");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tbCliente_CodCliente",
                table: "AspNetUsers",
                column: "CodCliente",
                principalTable: "tbCliente",
                principalColumn: "CodCliente",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tbCliente_CodCliente",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tbItemsVistoria");

            migrationBuilder.DropTable(
                name: "tbLocalItem");

            migrationBuilder.DropTable(
                name: "tbStatus");

            migrationBuilder.DropTable(
                name: "tbVistoria");

            migrationBuilder.DropTable(
                name: "tbControleOs");

            migrationBuilder.DropTable(
                name: "tbItens");

            migrationBuilder.DropTable(
                name: "tbLocal");

            migrationBuilder.DropTable(
                name: "tbCliente");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CodCliente",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CodCliente",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "HasAccess",
                table: "AspNetUsers",
                newName: "hasAccess");

            migrationBuilder.AlterDatabase(
                oldCollation: "Latin1_General_CI_AS");
        }
    }
}
