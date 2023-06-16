using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data;

//easily add entity framework by using ctrl period
public class CategoryDbContext:DbContext
{
	public CategoryDbContext(DbContextOptions<CategoryDbContext> options):base(options)
	{

	}

	public DbSet<CategoryModel> Categories { get; set; }

}
