using Microsoft.EntityFrameworkCore;
using School.ConsoleApp.Models;

namespace School.ConsoleApp.DAL;

public class SchoolContext: DbContext
{
	public DbSet<Person> People { get; set; }
	public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
	{
	}
}
