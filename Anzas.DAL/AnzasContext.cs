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

    public virtual DbSet<InfoRoute> InfoRoutes { get; set; }

    public virtual DbSet<InfoTrench> InfoTrenches { get; set; }

    public virtual DbSet<Mine> Mines { get; set; }

    public virtual DbSet<Otbor> Otbors { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Rock> Rocks { get; set; }

    public virtual DbSet<RockCode> RockCodes { get; set; }

    public virtual DbSet<RocksRoute> RocksRoutes { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=DBSERVER;Port=5432;Database=anzas;Username=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InfoDrill>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Info_Drill_pkey");

            entity.ToTable("Info_Drill", tb => tb.HasComment("Информация по скважинам"));

            entity.HasIndex(e => e.Geolog, "IX_Info_Drill_Geolog");

            entity.HasIndex(e => e.PlaceSite, "IX_Info_Drill_PLACE (SITE)");

            entity.HasIndex(e => e.Project, "IX_Info_Drill_Project");

            entity.HasIndex(e => e.TypeLcode, "IX_Info_Drill_TYPE (LCODE)");

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

            entity.HasOne(d => d.ProjectNavigation).WithMany(p => p.InfoDrills)
                .HasForeignKey(d => d.Project)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Info_Drill_Project_fkey");

            entity.HasOne(d => d.TypeLcodeNavigation).WithMany(p => p.InfoDrills)
                .HasForeignKey(d => d.TypeLcode)
                .HasConstraintName("Info_Drill_TYPE (LCODE)_fkey");
        });

        modelBuilder.Entity<InfoRoute>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Info_Route_pkey");

            entity.ToTable("Info_Route", tb => tb.HasComment("Информация по маршрутам"));

            entity.HasIndex(e => e.Geolog, "IX_Info_Route_Geolog");

            entity.HasIndex(e => e.PlaceSite, "IX_Info_Route_PLACE (SITE)");

            entity.HasIndex(e => e.TypeLcode, "IX_Info_Route_TYPE (LCODE)");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Date).HasComment("Дата");
            entity.Property(e => e.Easting1).HasComment("Долгота (начало)");
            entity.Property(e => e.Easting2).HasComment("Долгота (конец)");
            entity.Property(e => e.Elevation1).HasComment("Абс. отм. (начало)");
            entity.Property(e => e.Elevation2).HasComment("Абс. отм. (конец)");
            entity.Property(e => e.HoleId)
                .HasComment("№ маршрута")
                .HasColumnType("character varying")
                .HasColumnName("HoleID");
            entity.Property(e => e.Length)
                .HasComment("Длина маршрута")
                .HasColumnName("LENGTH");
            entity.Property(e => e.Northing1).HasComment("Долгота (начало)");
            entity.Property(e => e.Northing2).HasComment("Широта (конец)");
            entity.Property(e => e.NotesCommentsText)
                .HasMaxLength(2000)
                .HasColumnName("NOTES (COMMENTS, TEXT)");
            entity.Property(e => e.PlaceSite)
                .HasComment("Название участка")
                .HasColumnName("PLACE (SITE)");
            entity.Property(e => e.TypeLcode)
                .HasComment("Тип выработки")
                .HasColumnName("TYPE (LCODE)");

            entity.HasOne(d => d.GeologNavigation).WithMany(p => p.InfoRoutes)
                .HasForeignKey(d => d.Geolog)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Info_Route_Geolog_fkey");

            entity.HasOne(d => d.PlaceSiteNavigation).WithMany(p => p.InfoRoutes)
                .HasForeignKey(d => d.PlaceSite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Info_Route_PLACE (SITE)_fkey");

            entity.HasOne(d => d.TypeLcodeNavigation).WithMany(p => p.InfoRoutes)
                .HasForeignKey(d => d.TypeLcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Info_Route_TYPE (LCODE)_fkey");
        });

        modelBuilder.Entity<InfoTrench>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Info_Trench_pkey");

            entity.ToTable("Info_Trench", tb => tb.HasComment("Информация по канавам/траншеям"));

            entity.HasIndex(e => e.Geolog, "IX_Info_Trench_Geolog");

            entity.HasIndex(e => e.PlaceSite, "IX_Info_Trench_PLACE (SITE)");

            entity.HasIndex(e => e.TypeLcode, "IX_Info_Trench_TYPE (LCODE)");

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

        modelBuilder.Entity<Otbor>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Otbor_pkey");

            entity.ToTable("Otbor", tb => tb.HasComment("Справочник характеристика места отбора обр., проб"));

            entity.Property(e => e.Uid).ValueGeneratedNever();
            entity.Property(e => e.Name).HasColumnType("character varying");
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

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PROJECT_pkey");

            entity.ToTable("PROJECT", tb => tb.HasComment("Проекты"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Name).HasMaxLength(2000);
            entity.Property(e => e.NameFull).HasMaxLength(4000);
            entity.Property(e => e.OrgExecutor).HasColumnName("Org_executor");
        });

        modelBuilder.Entity<Rock>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Rocks_pkey");

            entity.ToTable(tb => tb.HasComment("Литология"));

            entity.HasIndex(e => e.Geolog, "IX_Rocks_Geolog");

            entity.HasIndex(e => e.HoleId, "IX_Rocks_HoleID");

            entity.HasIndex(e => e.RockCode, "IX_Rocks_RockCode");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("Описание керна");
            entity.Property(e => e.From).HasComment("От");
            entity.Property(e => e.Geolog).HasComment("Геолог");
            entity.Property(e => e.HoleId)
                .HasComment("№ выработки")
                .HasColumnName("HoleID");
            entity.Property(e => e.Kernm)
                .HasComment("Выход керна, м")
                .HasColumnName("KERNM");
            entity.Property(e => e.Kernpc)
                .HasComment("Выход керна, %")
                .HasColumnName("KERNPC");
            entity.Property(e => e.Length).HasComment("Длина интервала");
            entity.Property(e => e.Mineral)
                .HasMaxLength(2000)
                .HasComment("Описание аншлифов")
                .HasColumnName("MINERAL");
            entity.Property(e => e.NotesCommentsText)
                .HasMaxLength(2000)
                .HasComment("Примечания")
                .HasColumnName("NOTES (COMMENTS, TEXT)");
            entity.Property(e => e.Petro)
                .HasMaxLength(2000)
                .HasComment("Описание шлифов")
                .HasColumnName("PETRO");
            entity.Property(e => e.Profile).HasComment("Номер ПЛ");
            entity.Property(e => e.RockCode).HasComment("Код породы");
            entity.Property(e => e.To).HasComment("До");

            entity.HasOne(d => d.GeologNavigation).WithMany(p => p.Rocks)
                .HasForeignKey(d => d.Geolog)
                .HasConstraintName("Rocks_Geolog_fkey");

            entity.HasOne(d => d.Hole).WithMany(p => p.Rocks)
                .HasForeignKey(d => d.HoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Rocks_HoleID_fkey");

            entity.HasOne(d => d.RockCodeNavigation).WithMany(p => p.Rocks)
                .HasForeignKey(d => d.RockCode)
                .HasConstraintName("Rocks_RockCode_fkey");
        });

        modelBuilder.Entity<RockCode>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Rock_codes_pkey");

            entity.ToTable("Rock_codes", tb => tb.HasComment("Справочник породы"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Rock)
                .HasMaxLength(2000)
                .HasComment("Порода")
                .HasColumnName("ROCK");
            entity.Property(e => e.RockCode1)
                .HasMaxLength(20)
                .HasComment("Код породы")
                .HasColumnName("ROCK_CODE");
        });

        modelBuilder.Entity<RocksRoute>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Rocks_Route_pkey");

            entity.ToTable("Rocks_Route", tb => tb.HasComment("Литология маршрутов"));

            entity.HasIndex(e => e.Geolog, "IX_Rocks_Route_Geolog");

            entity.HasIndex(e => e.RockCode, "IX_Rocks_Route_RockCode");

            entity.HasIndex(e => e.TnOtbor, "IX_Rocks_Route_TN_Otbor");

            entity.HasIndex(e => e.TnType, "IX_Rocks_Route_TN_Type");

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.Description)
                .HasComment("Описание керна")
                .HasColumnType("character varying");
            entity.Property(e => e.Easting).HasComment("Долгота");
            entity.Property(e => e.Elevation).HasComment("Абс. отм.");
            entity.Property(e => e.Geolog).HasComment("Геолог");
            entity.Property(e => e.HoleId)
                .HasComment("№ маршрута")
                .HasColumnName("HoleID");
            entity.Property(e => e.Mineral)
                .HasComment("Описание аншлифов")
                .HasColumnType("character varying")
                .HasColumnName("MINERAL");
            entity.Property(e => e.Northing).HasComment("Широта");
            entity.Property(e => e.NotesCommentsText)
                .HasComment("Примечания")
                .HasColumnType("character varying")
                .HasColumnName("NOTES (COMMENTS, TEXT)");
            entity.Property(e => e.Petro)
                .HasComment("Описание шлифов")
                .HasColumnType("character varying")
                .HasColumnName("PETRO");
            entity.Property(e => e.RockCode).HasComment("Код породы");
            entity.Property(e => e.Rsample)
                .HasColumnType("character varying")
                .HasColumnName("RSample");
            entity.Property(e => e.Rtn)
                .HasComment("Номер точки наблюдения (т.н.)")
                .HasColumnName("RTN");
            entity.Property(e => e.TnOtbor)
                .HasComment("Характеристика места отбора обр., проб")
                .HasColumnName("TN_Otbor");
            entity.Property(e => e.TnType)
                .HasComment("Тип опробуемых отложений")
                .HasColumnName("TN_Type");

            entity.HasOne(d => d.GeologNavigation).WithMany(p => p.RocksRoutes)
                .HasForeignKey(d => d.Geolog)
                .HasConstraintName("Rocks_Route_Geolog_fkey");

            entity.HasOne(d => d.RockCodeNavigation).WithMany(p => p.RocksRoutes)
                .HasForeignKey(d => d.RockCode)
                .HasConstraintName("Rocks_Route_RockCode_fkey");

            entity.HasOne(d => d.TnOtborNavigation).WithMany(p => p.RocksRoutes)
                .HasForeignKey(d => d.TnOtbor)
                .HasConstraintName("Rocks_Route_TN_Otbor_fkey");

            entity.HasOne(d => d.TnTypeNavigation).WithMany(p => p.RocksRoutes)
                .HasForeignKey(d => d.TnType)
                .HasConstraintName("Rocks_Route_TN_Type_fkey");
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Survey_pkey");

            entity.ToTable("Survey", tb => tb.HasComment("Инклинометрия скважин"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("UID");
            entity.Property(e => e.AzMagn)
                .HasComment("Аз_магн_исходный")
                .HasColumnName("Az_magn");
            entity.Property(e => e.AzimIst)
                .HasComment("Аз_ист_принятый")
                .HasColumnName("Azim_ist");
            entity.Property(e => e.AzimPr)
                .HasComment("Аз_мг_принятый")
                .HasColumnName("Azim_pr");
            entity.Property(e => e.Depth).HasComment("Глубина");
            entity.Property(e => e.HoleId).HasColumnName("HoleID");
            entity.Property(e => e.Inclin).HasComment("Угол наклона от вертикали");

            entity.HasOne(d => d.Hole).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.HoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Survey_HoleID_fkey");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("Type_pkey");

            entity.ToTable("Type", tb => tb.HasComment("Справочник тип опробуемых отложений"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("uid");
            entity.Property(e => e.Name)
                .HasComment("Наименование")
                .HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
