using Core.Models.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config.QuestionsConfigurations
{
    public class QuestionCategoryConfiguration : IEntityTypeConfiguration<QuestionCategoryModel>
    {
        public void Configure(EntityTypeBuilder<QuestionCategoryModel> builder)
        {
            builder.HasMany(q => q.Questions)
                .WithOne(qc => qc.QuestionCategory)
                .HasForeignKey(qc => qc.QuestionCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}