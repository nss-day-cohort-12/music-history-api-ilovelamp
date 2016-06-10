using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ILoveLampMusic.Migrations
{
    public partial class MySecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    AlbumId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlbumTitle = table.Column<string>(nullable: true),
                    Artist = table.Column<string>(nullable: true),
                    YearReleased = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.AlbumId);
                });

            migrationBuilder.CreateTable(
                name: "MusicListener",
                columns: table => new
                {
                    MusicListenerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicListener", x => x.MusicListenerId);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    TrackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlbumId = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Length = table.Column<int>(nullable: false),
                    MusicListenerId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.TrackId);
                    table.ForeignKey(
                        name: "FK_Track_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "AlbumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Track_MusicListener_MusicListenerId",
                        column: x => x.MusicListenerId,
                        principalTable: "MusicListener",
                        principalColumn: "MusicListenerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Track_AlbumId",
                table: "Track",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Track_MusicListenerId",
                table: "Track",
                column: "MusicListenerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Track");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "MusicListener");
        }
    }
}
