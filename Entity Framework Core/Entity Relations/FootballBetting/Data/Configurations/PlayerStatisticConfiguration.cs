namespace P03_FootballBetting.Data.Configurations
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder
                .HasKey(ps => new
                {
                    ps.PlayerId,
                    ps.GameId
                });


            builder
                .HasOne(b => b.Game)
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(g => g.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(b => b.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(p => p.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
