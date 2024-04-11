using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config.QuestionsConfigurations
{
    public class AnswerFormConfig : IEntityTypeConfiguration<AnsweredFormModel>
    {
        public void Configure(EntityTypeBuilder<AnsweredFormModel> builder)
        {
            builder.HasMany(x => x.Answers)
                .WithOne(a => a.AnsweredFormModel)
                .HasForeignKey(a => a.FormAnsweredId);
        }
    }
}