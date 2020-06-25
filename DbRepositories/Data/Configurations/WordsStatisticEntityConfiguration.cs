using DbRepositories.Data.Object;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbRepositories.Data.Configurations
{
    public class WordsStatisticEntityConfiguration : IEntityTypeConfiguration<WordsStatistic>
    {
        public void Configure( EntityTypeBuilder<WordsStatistic> builder )
        {
            builder.ToTable( nameof( WordsStatistic ) ).HasKey( item => item.WordsStatisticId );

            builder.Property( item => item.SiteUrl ).HasMaxLength( 500 );
            builder.Property( item => item.UniqueWord ).HasMaxLength( 255 );
            builder.Property( item => item.Count ).HasColumnType( "int" );
            builder.Property( item => item.Timestamp ).HasColumnType( "Date" );
        }
    }
}
