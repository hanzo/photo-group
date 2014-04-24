using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGroup.Data
{
	public class PhotoGroupMigrationConfiguration : DbMigrationsConfiguration<PhotoGroupContext>
	{
		public PhotoGroupMigrationConfiguration()
		{
			this.AutomaticMigrationsEnabled = true;
			this.AutomaticMigrationDataLossAllowed = true;
		}

#if DEBUG
		protected override void Seed(PhotoGroupContext context)
		{
			// Seed the database if necessary
			new MockDatabaseSeeder(context).Seed();
		}
#endif
	}
}
