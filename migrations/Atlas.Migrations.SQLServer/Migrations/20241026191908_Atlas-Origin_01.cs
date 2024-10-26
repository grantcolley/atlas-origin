using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas.Migrations.SQLServer.Migrations
{
    /// <inheritdoc />
    public partial class AtlasOrigin_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClrType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentColours",
                columns: table => new
                {
                    DocumentColourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Colour = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rgb = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentColours", x => x.DocumentColourId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentConfigs",
                columns: table => new
                {
                    DocumentConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageMarginLeft = table.Column<int>(type: "int", nullable: false),
                    PageMarginTop = table.Column<int>(type: "int", nullable: false),
                    PageMarginRight = table.Column<int>(type: "int", nullable: false),
                    PageMarginBottom = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignContent = table.Column<int>(type: "int", nullable: false),
                    IgnoreParapgraphSpacing = table.Column<bool>(type: "bit", nullable: true),
                    FontSize = table.Column<int>(type: "int", nullable: true),
                    Font = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Colour = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    SubstituteStart = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SubstituteEnd = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ParagraphSpacingBetweenLinesAfter = table.Column<int>(type: "int", nullable: false),
                    ParagraphSpacingBetweenLinesBefore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentConfigs", x => x.DocumentConfigId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFonts",
                columns: table => new
                {
                    DocumentFontId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Font = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFonts", x => x.DocumentFontId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentParagraphs",
                columns: table => new
                {
                    DocumentParagraphId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DocumentParagraphType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignContent = table.Column<int>(type: "int", nullable: false),
                    IgnoreParapgraphSpacing = table.Column<bool>(type: "bit", nullable: true),
                    FontSize = table.Column<int>(type: "int", nullable: true),
                    Font = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Colour = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    SubstituteStart = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SubstituteEnd = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    ParagraphSpacingBetweenLinesAfter = table.Column<int>(type: "int", nullable: false),
                    ParagraphSpacingBetweenLinesBefore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentParagraphs", x => x.DocumentParagraphId);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Context = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    User = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleId);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSubstitutes",
                columns: table => new
                {
                    DocumentSubstituteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Group = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DocumentConfigId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSubstitutes", x => x.DocumentSubstituteId);
                    table.ForeignKey(
                        name: "FK_DocumentSubstitutes_DocumentConfigs_DocumentConfigId",
                        column: x => x.DocumentConfigId,
                        principalTable: "DocumentConfigs",
                        principalColumn: "DocumentConfigId");
                });

            migrationBuilder.CreateTable(
                name: "DocumentConfigParagraphs",
                columns: table => new
                {
                    DocumentConfigParagraphId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    DocumentConfigId = table.Column<int>(type: "int", nullable: false),
                    DocumentParagraphId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentConfigParagraphs", x => x.DocumentConfigParagraphId);
                    table.ForeignKey(
                        name: "FK_DocumentConfigParagraphs_DocumentConfigs_DocumentConfigId",
                        column: x => x.DocumentConfigId,
                        principalTable: "DocumentConfigs",
                        principalColumn: "DocumentConfigId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentConfigParagraphs_DocumentParagraphs_DocumentParagraphId",
                        column: x => x.DocumentParagraphId,
                        principalTable: "DocumentParagraphs",
                        principalColumn: "DocumentParagraphId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentContents",
                columns: table => new
                {
                    DocumentContentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bold = table.Column<bool>(type: "bit", nullable: true),
                    Italic = table.Column<bool>(type: "bit", nullable: true),
                    Underscore = table.Column<bool>(type: "bit", nullable: true),
                    ImageHeight = table.Column<int>(type: "int", nullable: true),
                    ImageWidth = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RenderCellCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContentType = table.Column<int>(type: "int", nullable: false),
                    DocumentParagraphId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignContent = table.Column<int>(type: "int", nullable: false),
                    IgnoreParapgraphSpacing = table.Column<bool>(type: "bit", nullable: true),
                    FontSize = table.Column<int>(type: "int", nullable: true),
                    Font = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Colour = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    SubstituteStart = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SubstituteEnd = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentContents", x => x.DocumentContentId);
                    table.ForeignKey(
                        name: "FK_DocumentContents_DocumentParagraphs_DocumentParagraphId",
                        column: x => x.DocumentParagraphId,
                        principalTable: "DocumentParagraphs",
                        principalColumn: "DocumentParagraphId");
                });

            migrationBuilder.CreateTable(
                name: "DocumentTableCells",
                columns: table => new
                {
                    DocumentTableCellId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    ColumnNumber = table.Column<int>(type: "int", nullable: false),
                    BorderLeft = table.Column<int>(type: "int", nullable: true),
                    BorderTop = table.Column<int>(type: "int", nullable: true),
                    BorderRight = table.Column<int>(type: "int", nullable: true),
                    BorderBottom = table.Column<int>(type: "int", nullable: true),
                    CellCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BorderLeftColour = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    BorderTopColour = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    BorderRightColour = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    BorderBottomColour = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    CellColour = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    DocumentParagraphId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlignContent = table.Column<int>(type: "int", nullable: false),
                    IgnoreParapgraphSpacing = table.Column<bool>(type: "bit", nullable: true),
                    FontSize = table.Column<int>(type: "int", nullable: true),
                    Font = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Colour = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    SubstituteStart = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SubstituteEnd = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTableCells", x => x.DocumentTableCellId);
                    table.ForeignKey(
                        name: "FK_DocumentTableCells_DocumentParagraphs_DocumentParagraphId",
                        column: x => x.DocumentParagraphId,
                        principalTable: "DocumentParagraphs",
                        principalColumn: "DocumentParagraphId");
                });

            migrationBuilder.CreateTable(
                name: "DocumentTableColumns",
                columns: table => new
                {
                    DocumentTableColumnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: true),
                    DocumentParagraphId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTableColumns", x => x.DocumentTableColumnId);
                    table.ForeignKey(
                        name: "FK_DocumentTableColumns_DocumentParagraphs_DocumentParagraphId",
                        column: x => x.DocumentParagraphId,
                        principalTable: "DocumentParagraphs",
                        principalColumn: "DocumentParagraphId");
                });

            migrationBuilder.CreateTable(
                name: "DocumentTableRows",
                columns: table => new
                {
                    DocumentTableRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: true),
                    DocumentParagraphId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTableRows", x => x.DocumentTableRowId);
                    table.ForeignKey(
                        name: "FK_DocumentTableRows_DocumentParagraphs_DocumentParagraphId",
                        column: x => x.DocumentParagraphId,
                        principalTable: "DocumentParagraphs",
                        principalColumn: "DocumentParagraphId");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRole",
                columns: table => new
                {
                    PermissionsPermissionId = table.Column<int>(type: "int", nullable: false),
                    RolesRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRole", x => new { x.PermissionsPermissionId, x.RolesRoleId });
                    table.ForeignKey(
                        name: "FK_PermissionRole_Permissions_PermissionsPermissionId",
                        column: x => x.PermissionsPermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRole_Roles_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesRoleId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesRoleId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Route = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                    table.ForeignKey(
                        name: "FK_Pages_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModuleId",
                table: "Categories",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentColours_Colour",
                table: "DocumentColours",
                column: "Colour",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentColours_Rgb",
                table: "DocumentColours",
                column: "Rgb",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConfigParagraphs_DocumentConfigId",
                table: "DocumentConfigParagraphs",
                column: "DocumentConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConfigParagraphs_DocumentParagraphId",
                table: "DocumentConfigParagraphs",
                column: "DocumentParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentConfigs_Name",
                table: "DocumentConfigs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentContents_DocumentParagraphId",
                table: "DocumentContents",
                column: "DocumentParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFonts_Font",
                table: "DocumentFonts",
                column: "Font",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentParagraphs_Name",
                table: "DocumentParagraphs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSubstitutes_DocumentConfigId",
                table: "DocumentSubstitutes",
                column: "DocumentConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTableCells_DocumentParagraphId",
                table: "DocumentTableCells",
                column: "DocumentParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTableColumns_DocumentParagraphId",
                table: "DocumentTableColumns",
                column: "DocumentParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTableRows_DocumentParagraphId",
                table: "DocumentTableRows",
                column: "DocumentParagraphId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Context",
                table: "Logs",
                column: "Context");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Message",
                table: "Logs",
                column: "Message");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_TimeStamp",
                table: "Logs",
                column: "TimeStamp");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_User",
                table: "Logs",
                column: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Name",
                table: "Modules",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_CategoryId",
                table: "Pages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_Name",
                table: "Pages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RolesRoleId",
                table: "PermissionRole",
                column: "RolesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Code",
                table: "Permissions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersUserId",
                table: "RoleUser",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "DocumentColours");

            migrationBuilder.DropTable(
                name: "DocumentConfigParagraphs");

            migrationBuilder.DropTable(
                name: "DocumentContents");

            migrationBuilder.DropTable(
                name: "DocumentFonts");

            migrationBuilder.DropTable(
                name: "DocumentSubstitutes");

            migrationBuilder.DropTable(
                name: "DocumentTableCells");

            migrationBuilder.DropTable(
                name: "DocumentTableColumns");

            migrationBuilder.DropTable(
                name: "DocumentTableRows");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "PermissionRole");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "DocumentConfigs");

            migrationBuilder.DropTable(
                name: "DocumentParagraphs");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Modules");
        }
    }
}
