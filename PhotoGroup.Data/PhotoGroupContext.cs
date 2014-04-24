using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using PhotoGroup.Data.Entities;

namespace PhotoGroup.Data
{
	public class PhotoGroupContext : DbContext
	{
		public PhotoGroupContext()
			: base("DefaultConnection")
		{
			this.Configuration.LazyLoadingEnabled = false;
			this.Configuration.ProxyCreationEnabled = false;
		}

		static PhotoGroupContext()
		{
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoGroupContext, PhotoGroupMigrationConfiguration>());
		}

		//protected override void OnModelCreating(DbModelBuilder modelBuilder)
		//{
		//	base.OnModelCreating(modelBuilder);

		//	CountingKsMapping.Configure(modelBuilder);
		//}

		public DbSet<Photo> Photos { get; set; }

		public DbSet<Album> Albums { get; set; }
	}
}
