using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ILoveLampMusic.Models;

namespace ILoveLampMusic.Migrations
{
    [DbContext(typeof(MusicHistoryContext))]
    [Migration("20160610181453_uno")]
    partial class uno
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ILoveLampMusic.Models.Album", b =>
                {
                    b.Property<int>("AlbumId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlbumTitle");

                    b.Property<string>("Artist");

                    b.Property<string>("YearReleased");

                    b.HasKey("AlbumId");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("ILoveLampMusic.Models.MusicListener", b =>
                {
                    b.Property<int>("MusicListenerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("email");

                    b.Property<string>("name");

                    b.HasKey("MusicListenerId");

                    b.ToTable("MusicListener");
                });

            modelBuilder.Entity("ILoveLampMusic.Models.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlbumId");

                    b.Property<string>("Author");

                    b.Property<string>("Genre");

                    b.Property<int>("Length");

                    b.Property<int?>("MusicListenerId");

                    b.Property<string>("Title");

                    b.HasKey("TrackId");

                    b.HasIndex("AlbumId");

                    b.HasIndex("MusicListenerId");

                    b.ToTable("Track");
                });

            modelBuilder.Entity("ILoveLampMusic.Models.Track", b =>
                {
                    b.HasOne("ILoveLampMusic.Models.Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ILoveLampMusic.Models.MusicListener")
                        .WithMany()
                        .HasForeignKey("MusicListenerId");
                });
        }
    }
}
