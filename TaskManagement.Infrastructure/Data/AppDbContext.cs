using TaskManagement.Core.NotificationAggregate;
using TaskManagement.Core.ProjectAggregate;

namespace TaskManagement.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
        public DbSet<TaskItem> TaskItems => Set<TaskItem>();
        public DbSet<Issue> Issues => Set<Issue>();
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<Notification> Notifications => Set<Notification>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
    }
}
