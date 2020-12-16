using CourseWork.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Models
{
    public class RestarauntDbContext : DbContext
    {
        public RestarauntDbContext(DbContextOptions<RestarauntDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DishIngredient> DishIngredients { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
    }
}