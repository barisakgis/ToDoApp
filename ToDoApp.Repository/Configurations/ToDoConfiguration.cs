

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Models.Entities;

namespace ToDoApp.Repository.Configurations;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("ToDos").HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("ToDoId");
        builder.Property(c => c.CreatedTime).HasColumnName("CreatedTime");
        builder.Property(c => c.UpdatedTime).HasColumnName("UpdatedTime");
        builder.Property(c => c.Title).HasColumnName("Title");
        builder.Property(c => c.Description).HasColumnName("Description");
        builder.Property(c => c.StartDate).HasColumnName("StartDate");
        builder.Property(c => c.EndDate).HasColumnName("EndDate");
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate");
        builder.Property(c => c.Priority).HasColumnName("Priority");
        builder.Property(c => c.CategoryId).HasColumnName("CategoryId");
        builder.Property(c => c.Completed).HasColumnName("Completed");
        //builder.Property(c => c.Category).HasColumnName("Category");
        builder.Property(c => c.UserId).HasColumnName("UserId");
      //  builder.Property(c => c.User).HasColumnName("User");
        
        builder.HasOne(x => x.Category).WithMany(x => x.ToDos).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(X => X.User).WithMany(x => x.ToDos).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
          
        builder.Navigation(x => x.Category).AutoInclude();
        builder.Navigation(x => x.User).AutoInclude(); 
    }
}
