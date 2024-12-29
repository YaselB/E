using Microsoft.EntityFrameworkCore;

namespace eclipse.Aplication;
public class DBContext : DbContext{
    public DBContext(DbContextOptions<DBContext> options) : base(options){ }
    public required DbSet<User> Users { get; set; }
    public required DbSet<Comentary> Comentarys { get; set; }
    public required DbSet<Navbar> Navbars { get; set; }
    public required DbSet<Ticket> Tickets { get; set; }
    public required DbSet<Rol> Rols { get; set; }
    public required DbSet<Dates> Dates { get; set; }
    public required DbSet<Prices> Prices { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Rol>().HasOne(r=> r.Username).WithOne(u=> u.Rol).HasForeignKey<Rol>(r=>r.UserID);
        modelBuilder.Entity<Dates>().HasOne(u=> u.ticket).WithMany(r=> r.dates).HasForeignKey(u=> u.IDTickets);
        modelBuilder.Entity<Prices>().HasOne(u=> u.ticket).WithMany(r=> r.prices).HasForeignKey(u=> u.IDTickets);
    }
}