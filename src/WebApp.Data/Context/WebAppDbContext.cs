using Microsoft.EntityFrameworkCore;

namespace WebApp.Data.Context;

public class WebAppDbContext(DbContextOptions<WebAppDbContext> options) : DbContext(options)
{
}

//public DbSet<User> Users { get; set; }