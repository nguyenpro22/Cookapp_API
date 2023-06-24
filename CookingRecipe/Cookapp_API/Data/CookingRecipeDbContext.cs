using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cookapp_API.Data;

public partial class CookingRecipeDbContext : DbContext
{
    public CookingRecipeDbContext()
    {
    }

    public CookingRecipeDbContext(DbContextOptions<CookingRecipeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Blacklist> Blacklists { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<IngrePost> IngrePosts { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<NutriPost> NutriPosts { get; set; }

    public virtual DbSet<Nutrition> Nutritions { get; set; }

    public virtual DbSet<Recipepost> Recipeposts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagPost> TagPosts { get; set; }

    public virtual DbSet<TypePost> TypePosts { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=CookappDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_account");

            entity.ToTable("accounts");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Roleid)
                .HasMaxLength(50)
                .HasColumnName("roleid");
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("username");

            
        });

        modelBuilder.Entity<Blacklist>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("blacklist");

            entity.Property(e => e.IsBan).HasColumnName("isBan");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .HasColumnName("reason");
            entity.Property(e => e.RefUser)
                .HasMaxLength(50)
                .HasColumnName("ref_user");

            entity.HasOne(d => d.RefUserNavigation).WithMany()
                .HasForeignKey(d => d.RefUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_blacklist_accounts");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(50)
                .HasColumnName("content");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_comment");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(50)
                .HasColumnName("content");
            entity.Property(e => e.CreateTime).HasColumnName("create_time");
            entity.Property(e => e.RefPost)
                .HasMaxLength(50)
                .HasColumnName("ref_post");
            entity.Property(e => e.RefUser)
                .HasMaxLength(50)
                .HasColumnName("ref_user");

            entity.HasOne(d => d.RefPostNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RefPost)
                .HasConstraintName("FK_comments_recipe_posts");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("images");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Image1).HasColumnName("image");
        });

        modelBuilder.Entity<IngrePost>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ingre_post");

            entity.Property(e => e.RefIngredient)
                .HasMaxLength(50)
                .HasColumnName("ref_ingredient");
            entity.Property(e => e.RefPost)
                .HasMaxLength(50)
                .HasColumnName("ref_post");

            entity.HasOne(d => d.RefIngredientNavigation).WithMany()
                .HasForeignKey(d => d.RefIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ingre_post_ingredients");

            entity.HasOne(d => d.RefPostNavigation).WithMany()
                .HasForeignKey(d => d.RefPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ingre_post_recipe_post");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.ToTable("ingredients");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<NutriPost>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("nutri_post");

            entity.Property(e => e.RefNutri)
                .HasMaxLength(50)
                .HasColumnName("ref_nutri");
            entity.Property(e => e.RefPost)
                .HasMaxLength(50)
                .HasColumnName("ref_post");

            entity.HasOne(d => d.RefNutriNavigation).WithMany()
                .HasForeignKey(d => d.RefNutri)
                .HasConstraintName("FK_nutri_post_nutrition");

            entity.HasOne(d => d.RefPostNavigation).WithMany()
                .HasForeignKey(d => d.RefPost)
                .HasConstraintName("FK_nutri_post_recipe_posts");
        });

        modelBuilder.Entity<Nutrition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_nutri");

            entity.ToTable("nutrition");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Recipepost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_recipe_post");

            entity.ToTable("recipeposts");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(50)
                .HasColumnName("content");
            entity.Property(e => e.CreateTime).HasColumnName("create_time");
            entity.Property(e => e.RefAccount)
                .HasMaxLength(50)
                .HasColumnName("ref_account");
            entity.Property(e => e.RefCategory)
                .HasMaxLength(50)
                .HasColumnName("ref_category");
            entity.Property(e => e.RefImage)
                .HasMaxLength(50)
                .HasColumnName("ref_image");
            entity.Property(e => e.RefTag)
                .HasMaxLength(50)
                .HasColumnName("ref_tag");
            entity.Property(e => e.RefVideo)
                .HasMaxLength(50)
                .HasColumnName("ref_video");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.UpdateTime).HasColumnName("update_time");

           

            entity.HasOne(d => d.RefImageNavigation).WithMany(p => p.Recipeposts)
                .HasForeignKey(d => d.RefImage)
                .HasConstraintName("FK_recipe_posts_images");

            entity.HasOne(d => d.RefVideoNavigation).WithMany(p => p.Recipeposts)
                .HasForeignKey(d => d.RefVideo)
                .HasConstraintName("FK_recipe_posts_videos");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_role");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId)
                .HasMaxLength(50)
                .HasColumnName("roleId");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tag");

            entity.ToTable("tags");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TagPost>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tag_post");

            entity.Property(e => e.RefPost)
                .HasMaxLength(50)
                .HasColumnName("ref_post");
            entity.Property(e => e.RefTag)
                .HasMaxLength(50)
                .HasColumnName("ref_tag");

            entity.HasOne(d => d.RefPostNavigation).WithMany()
                .HasForeignKey(d => d.RefPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tag_post_recipe_posts");

            entity.HasOne(d => d.RefTagNavigation).WithMany()
                .HasForeignKey(d => d.RefTag)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tag_post_tags");
        });

        modelBuilder.Entity<TypePost>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("type_post");

            entity.Property(e => e.RefPost)
                .HasMaxLength(50)
                .HasColumnName("ref_post");
            entity.Property(e => e.RefType)
                .HasMaxLength(50)
                .HasColumnName("ref_type");

            entity.HasOne(d => d.RefPostNavigation).WithMany()
                .HasForeignKey(d => d.RefPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_type_post_recipe_posts");

            entity.HasOne(d => d.RefTypeNavigation).WithMany()
                .HasForeignKey(d => d.RefType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_type_post_type");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.ToTable("videos");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Video1)
                .HasMaxLength(50)
                .HasColumnName("video");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
