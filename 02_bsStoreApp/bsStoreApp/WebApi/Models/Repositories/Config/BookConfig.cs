using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Models.Repositories.Config;

public class BookConfig : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasData(
                new Book{Id = 1, Title = "Mesneviden Dertler", Price = 252},
                new Book{Id = 2, Title = "C ile Programalama", Price = 312},
                new Book{Id = 3, Title = "Sherlock Holmes", Price = 145}
            );
    }
    
    
}