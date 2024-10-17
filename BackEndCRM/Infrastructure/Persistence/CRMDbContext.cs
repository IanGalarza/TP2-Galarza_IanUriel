using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = Domain.Models.TaskStatus;

namespace Infrastructure.Persistence
{
    public class CRMDbContext : DbContext
    {
        public DbSet<CampaignTypes> CampaignTypes { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Interactions> Interactions { get; set; }
        public DbSet<InteractionTypes> InteractionTypes { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskStatus> TaskStatus { get; set; }
        public DbSet<Users> Users { get; set; }

        public CRMDbContext(DbContextOptions<CRMDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuro las relaciones de las tablas y precargo los datos estaticos correspondientes

            modelBuilder.Entity<CampaignTypes>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsRequired();

                entity.HasData(
                    new CampaignTypes { Id = 1, Name = "SEO" },
                    new CampaignTypes { Id = 2, Name = "PPC" },
                    new CampaignTypes { Id = 3, Name = "Social Media" },
                    new CampaignTypes { Id = 4, Name = "Email Marketing" });
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(c => c.ClientID);
                entity.Property(c => c.ClientID)
                .ValueGeneratedOnAdd();

                entity.Property(c => c.Name)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(c => c.Email)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(c => c.Phone)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(c => c.Company)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

                entity.Property(c => c.Address)
                .HasColumnType("varchar(max)")
                .IsRequired();

                entity.Property(c => c.CreateDate)
                .IsRequired();

                //Precarga de nuevos Clientes

                entity.HasData(
                    new Clients { ClientID = 1, Name = "Pedro Fernandez", Email = "pfernandez@google.com", Phone = "555-123-4567", Company = "Google", Address = "1600 Amphitheatre Parkway, Mountain View, CA", CreateDate = DateTime.Now },
                    new Clients { ClientID = 2, Name = "Martin Martinez", Email = "mmartinez@amazon.com", Phone = "555-987-6543", Company = "Amazon", Address = "410 Terry Ave N, Seattle, WA", CreateDate = DateTime.Now },
                    new Clients { ClientID = 3, Name = "Ana Perez", Email = "aperez@microsoft.com", Phone = "555-456-7890", Company = "Microsoft", Address = "One Microsoft Way, Redmond, WA", CreateDate = DateTime.Now },
                    new Clients { ClientID = 4, Name = "Lucas Rodriguez", Email = "lrodriguez@nvidia.com", Phone = "555-321-7654", Company = "Nvidia", Address = "2788 San Tomas Expressway, Santa Clara, CA", CreateDate = DateTime.Now },
                    new Clients { ClientID = 5, Name = "Matias Bogado", Email = "mbogado@valve.com", Phone = "555-654-3210", Company = "Valve", Address = "10400 NE 4th St, Bellevue, WA", CreateDate = DateTime.Now }
                    );
            });

            modelBuilder.Entity<Interactions>(entity =>
            {
                entity.HasKey(i => i.InteractionID);
                entity.Property(i => i.InteractionID)
                .ValueGeneratedOnAdd();

                entity.Property(i => i.Notes)
                .HasColumnType("varchar(max)")
                .IsRequired();

                entity.Property(i => i.Date)
                .IsRequired();

                entity.HasOne(i => i.InteractionTypes)
                .WithMany(i => i.Interactions)
                .HasForeignKey(i => i.InteractionType);

                entity.HasOne(i => i.Project)
                .WithMany(p => p.Interactions)
                .HasForeignKey(i => i.ProjectID);
            });

            modelBuilder.Entity<InteractionTypes>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(25)
                .IsRequired();

                entity.HasData(
                    new InteractionTypes { Id = 1, Name = "Initial Meeting"},
                    new InteractionTypes { Id = 2, Name = "Phone call" },
                    new InteractionTypes { Id = 3, Name = "Email" },
                    new InteractionTypes { Id = 4, Name = "Presentation of Results" });
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(p => p.ProjectID);
                entity.Property(p => p.ProjectID)
                .ValueGeneratedOnAdd();

                entity.Property(p => p.ProjectName)
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(p => p.StartDate)
                .IsRequired();

                entity.Property(p => p.EndDate)
                .IsRequired();

                entity.Property(p => p.CreateDate)
                .IsRequired();

                entity.HasOne(p => p.CampaignTypes)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CampaignType);

                entity.HasOne(p => p.Clients)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ClientID);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasKey(t => t.TaskID);
                entity.Property(t => t.TaskID)
                .ValueGeneratedOnAdd();

                entity.Property(t => t.Name)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

                entity.Property(t => t.DueDate)
                .IsRequired();

                entity.Property(t => t.CreateDate)
                .IsRequired();

                entity.HasOne(t => t.Projects)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectID);

                entity.HasOne(t => t.Users)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedTo);

                entity.HasOne(t => t.TaskStatus)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.Status);
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name)
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsRequired();

                entity.HasData(
                    new TaskStatus { Id = 1, Name = "Pending"},
                    new TaskStatus { Id = 2, Name = "In Progress" },
                    new TaskStatus { Id = 3, Name = "Blocked" },
                    new TaskStatus { Id = 4, Name = "Done" },
                    new TaskStatus { Id = 5, Name = "Cancel" });
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(u => u.UserID);

                entity.Property(u => u.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

                entity.Property(u => u.Email)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

                entity.HasData(
                    new Users { UserID = 1, Name = "Joe Done", Email = "jdone@marketing.com" },
                    new Users { UserID = 2, Name = "Nill Amstrong", Email = "namstrong@marketing.com" },
                    new Users { UserID = 3, Name = "Marlyn Morales", Email = "mmorales@marketing.com" },
                    new Users { UserID = 4, Name = "Antony Orué", Email = "aorue@marketing.com" },
                    new Users { UserID = 5, Name = "Jazmin Fernandez", Email = "jfernandez@marketing.com" }
                    );
            });
        }
    }
}
