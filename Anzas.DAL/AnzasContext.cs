using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Anzas.DAL;

public partial class AnzasContext : DbContext
{
    public AnzasContext()
    {
    }

    public AnzasContext(DbContextOptions<AnzasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InfoDrill> InfoDrills { get; set; }

    public virtual DbSet<InfoTrench> InfoTrenches { get; set; }

    public virtual DbSet<Mine> Mines { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=anzas;Username=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InfoDrill>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Info_Drill_pkey");

            entity.ToTable("Info_Drill", tb => tb.HasComment("Информация по скважинам"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Azimuth).HasComment("Азимут ист., °");
            entity.Property(e => e.Depth).HasComment("Глубина скважины,м");
            entity.Property(e => e.Diam).HasComment("Диаметр бурения, мм");
            entity.Property(e => e.Dip).HasComment("Угол наклона от горизонта, °");
            entity.Property(e => e.Easting).HasComment("Долгота");
            entity.Property(e => e.Elevation).HasComment("Абс. отм.");
            entity.Property(e => e.EndDate).HasComment("Окончание бурения");
            entity.Property(e => e.Geolog).HasComment("Геолог");
            entity.Property(e => e.HoleId)
                .HasMaxLength(50)
                .HasComment("№ скважины")
                .HasColumnName("HoleID");
            entity.Property(e => e.Northing).HasComment("Широта");
            entity.Property(e => e.NotesCommentsText)
                .HasMaxLength(2000)
                .HasComment("Примечания")
                .HasColumnName("NOTES (COMMENTS, TEXT)");
            entity.Property(e => e.PlaceSite)
                .HasComment("Название участка")
                .HasColumnName("PLACE (SITE)");
            entity.Property(e => e.Profile).HasComment("Номер ПЛ");
            entity.Property(e => e.StartDate).HasComment("Начало бурения");
            entity.Property(e => e.TypeLcode)
                .HasComment("Тип выработки")
                .HasColumnName("TYPE (LCODE)");
            entity.Property(e => e.UrAbs)
                .HasComment("Абс. отм. уровня, м\n")
                .HasColumnName("UR_ABS");
            entity.Property(e => e.Uroven).HasComment("Уровень ПВ, м");

            entity.HasOne(d => d.GeologNavigation).WithMany(p => p.InfoDrills)
                .HasForeignKey(d => d.Geolog)
                .HasConstraintName("Info_Drill_Geolog_fkey");

            entity.HasOne(d => d.PlaceSiteNavigation).WithMany(p => p.InfoDrills)
                .HasForeignKey(d => d.PlaceSite)
                .HasConstraintName("Info_Drill_PLACE (SITE)_fkey");

            entity.HasOne(d => d.TypeLcodeNavigation).WithMany(p => p.InfoDrills)
                .HasForeignKey(d => d.TypeLcode)
                .HasConstraintName("Info_Drill_TYPE (LCODE)_fkey");
        });

        modelBuilder.Entity<InfoTrench>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Info_Trench_pkey");

            entity.ToTable("Info_Trench", tb => tb.HasComment("Информация по канавам/траншеям"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Azimuth).HasComment("Азимут ист., °");
            entity.Property(e => e.Depth).HasComment("Глубина канавы,м");
            entity.Property(e => e.Easting1).HasComment("Долгота (начало)");
            entity.Property(e => e.Easting2).HasComment("Долгота (конец)");
            entity.Property(e => e.Elevation1).HasComment("Абс. отм. (начало)");
            entity.Property(e => e.Elevation2).HasComment("Абс. отм. (конец)");
            entity.Property(e => e.EndDate).HasComment("Окончание проходки");
            entity.Property(e => e.Geolog).HasComment("Геолог");
            entity.Property(e => e.HoleId)
                .HasComment("№ выработки")
                .HasColumnType("character varying")
                .HasColumnName("HoleID");
            entity.Property(e => e.Length)
                .HasComment("Длина канавы,м")
                .HasColumnName("LENGTH");
            entity.Property(e => e.Northing1).HasComment("Широта (начало)");
            entity.Property(e => e.Northing2).HasComment("Широта (конец)");
            entity.Property(e => e.NotesCommentsText)
                .HasMaxLength(2000)
                .HasComment("Примечания")
                .HasColumnName("NOTES (COMMENTS, TEXT)");
            entity.Property(e => e.PlaceSite)
                .HasComment("Название участка")
                .HasColumnName("PLACE (SITE)");
            entity.Property(e => e.Profile).HasComment("Номер ПЛ");
            entity.Property(e => e.StartDate).HasComment("Начало проходки");
            entity.Property(e => e.TypeLcode).HasColumnName("TYPE (LCODE)");
            entity.Property(e => e.Width)
                .HasComment("Ширина канавы,м")
                .HasColumnName("WIDTH");

            entity.HasOne(d => d.GeologNavigation).WithMany(p => p.InfoTrenches)
                .HasForeignKey(d => d.Geolog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Info_Trench_Geolog_fkey");

            entity.HasOne(d => d.PlaceSiteNavigation).WithMany(p => p.InfoTrenches)
                .HasForeignKey(d => d.PlaceSite)
                .HasConstraintName("Info_Trench_PLACE (SITE)_fkey");

            entity.HasOne(d => d.TypeLcodeNavigation).WithMany(p => p.InfoTrenches)
                .HasForeignKey(d => d.TypeLcode)
                .HasConstraintName("Info_Trench_TYPE (LCODE)_fkey");
        });

        modelBuilder.Entity<Mine>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("MINES_pkey");

            entity.ToTable("MINES", tb => tb.HasComment("Справочник"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.TypeLcode)
                .HasMaxLength(300)
                .HasComment("Тип выработки")
                .HasColumnName("TYPE (LCODE)");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Person_pkey");

            entity.ToTable("Person", tb => tb.HasComment("Справочник персонала"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Name)
                .HasComment("Имя")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasComment("Отчество")
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasComment("Фамилия")
                .HasColumnType("character varying")
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("directory.PLACE_pkey");

            entity.ToTable("PLACE", tb => tb.HasComment("Справочник Участков"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.NamePlaceSite)
                .HasMaxLength(300)
                .HasComment("Название участка")
                .HasColumnName("Name_Place(site)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
