using Enquizitive.Features.Quiz;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enquizitive.Infrastructure.Configurations;

public class QuizTypeConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> config)
    {
        config.ToTable("quizzes");

        config.HasKey(x => x.Id);

        config.Property(x => x.Name)
            .IsRequired();

        config.Property(x => x.Description)
            .IsRequired(false);

        config.Property(x => x.CreatedAt)
            .HasDefaultValue(DateTimeOffset.UtcNow);

        config.Property(x => x.UpdatedAt)
            .HasDefaultValue(DateTimeOffset.UtcNow);
    }
}