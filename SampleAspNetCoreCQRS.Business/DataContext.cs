using System;
using SampleAspNetCoreCQRS.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace SampleAspNetCoreCQRS.Business
{
    public interface IDataContext
    {
        DbSet<PersonEnt> People { get; set; }

        #region Methods

        void Update<T>(T entity) where T : class;
        void Add<T>(T entity) where T : class;

        bool Commit();
        Task<bool> CommitAsync();

        #endregion        
    }

    public class DataContext : DbContext, IDataContext
    {
        public DbSet<PersonEnt> People { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        #region Methods

        public bool Commit()
        {
            return this.SaveChanges() > 0;
        }

        public async Task<bool> CommitAsync()
        {
            return await this.SaveChangesAsync() > 0;
        }

        public new void Update<T>(T entity) where T : class
        {
            this.Entry((object)entity).State = EntityState.Modified;
        }

        public new void Add<T>(T entity) where T : class
        {
            this.Entry((object)entity).State = EntityState.Added;

            this.Set<T>().Add(entity);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(24, 8);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEnt>().ToTable("People");
        }

        #endregion

    }
}
