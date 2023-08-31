using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Autors__3214EC2772B8F1C0", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__F9B8A48A25C39098", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "Librarians",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Libraria__3214EC275880F892", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PublishingCodeTypes",
                columns: table => new
                {
                    CodeType = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publishi__D07465C11279A326", x => x.CodeType);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DocumentNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Readers__3214EC27DEC054CC", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Readers__Documen__39AD8A7F",
                        column: x => x.DocumentType,
                        principalTable: "DocumentTypes",
                        principalColumn: "Type");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Autors = table.Column<int>(type: "int", nullable: false),
                    PublishingCode = table.Column<int>(name: "Publishing Code", type: "int", nullable: false),
                    PublishingCodeType = table.Column<int>(name: "Publishing Code Type", type: "int", nullable: false),
                    Year = table.Column<DateTime>(type: "date", nullable: false),
                    CountryPublish = table.Column<string>(name: "Country Publish", type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SityPublish = table.Column<string>(name: "Sity Publish", type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Books__3214EC27DC7A646B", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Books__Autors__320C68B7",
                        column: x => x.Autors,
                        principalTable: "Autors",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Books__Publishin__33008CF0",
                        column: x => x.PublishingCodeType,
                        principalTable: "PublishingCodeTypes",
                        principalColumn: "CodeType");
                });

            migrationBuilder.CreateTable(
                name: "BooksAutorsRelation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutorID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BooksAut__3214EC2792EF1CF1", x => x.ID);
                    table.ForeignKey(
                        name: "FK__BooksAuto__Autor__35DCF99B",
                        column: x => x.AutorID,
                        principalTable: "Autors",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__BooksAuto__BookI__36D11DD4",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Autors",
                table: "Books",
                column: "Autors");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Publishing Code Type",
                table: "Books",
                column: "Publishing Code Type");

            migrationBuilder.CreateIndex(
                name: "IX_BooksAutorsRelation_AutorID",
                table: "BooksAutorsRelation",
                column: "AutorID");

            migrationBuilder.CreateIndex(
                name: "IX_BooksAutorsRelation_BookID",
                table: "BooksAutorsRelation",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Readers_DocumentType",
                table: "Readers",
                column: "DocumentType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksAutorsRelation");

            migrationBuilder.DropTable(
                name: "Librarians");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Autors");

            migrationBuilder.DropTable(
                name: "PublishingCodeTypes");
        }
    }
}
