using System.Reflection;
using Core.Models;
using Core.Models.Clients;
using Core.Models.Questions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DatabaseContext : IdentityDbContext<UserModel, RoleModel, int, IdentityUserClaim<int>, UserRoleModel, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<InterestModel> Interests { get; set; }
        public DbSet<SubInterestModel> SubInterests { get; set; }
        public DbSet<QuestionCategoryModel> QuestionCategories { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<QuestionAnswerModel> QuestionsAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Check database used -- only for Dev while Using Sqlite!
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType
                    == typeof(decimal));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
        }
    }
}