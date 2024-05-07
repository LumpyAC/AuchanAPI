using Microsoft.EntityFrameworkCore;
using AuchanAPI.Models;

namespace AuchanAPI.Data
{
    public class ArticleContext : DbContext
    {
        public ArticleContext(DbContextOptions<ArticleContext> options) 
            : base(options)
        { 
        }

        public DbSet<Article> Articles { get; set; }
    }
}
