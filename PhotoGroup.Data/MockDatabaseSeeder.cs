//#define TEST_SEED
//#define FORCE_RECREATE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoGroup.Data.Entities;

namespace PhotoGroup.Data
{
	class MockDatabaseSeeder
	{
		PhotoGroupContext _ctx ;

		public MockDatabaseSeeder(PhotoGroupContext ctx)
		{
			_ctx = ctx;
		}

		void ExecuteQueries(params string[] sqlStatements)
		{
			foreach (var sql in sqlStatements)
			{
				_ctx.Database.ExecuteSqlCommand(sql);
			}
		}

		public void Seed()
		{
//#if !(TEST_SEED || FORCE_RECREATE)
			if (_ctx.Users.Count() > 0)
			{
				return;
			}
//#endif

//#if TEST_SEED || FORCE_RECREATE
      ExecuteQueries(
        "DELETE FROM Users",
        "DELETE FROM Albums",
        "DELETE FROM Photos"
      );
//#endif

			SeedUsers();
			SeedAlbums();
			SeedPhotos();
		}

		private void SeedUsers()
		{
			var users = new List<User>
			{
				new User { Id = 1, FirstName = "Hans", LastName = "Werner"},
				new User { Id = 2, FirstName = "Joe", LastName = "Shmo"},
				new User { Id = 3, FirstName = "Rolo", LastName = "Tony"},
			};

			foreach (var user in users)
			{
				_ctx.Users.Add(user);
			}
			_ctx.SaveChanges();
		}

		private void SeedAlbums()
		{
			var albums = new List<Album>
			{
				new Album { Id = 1, CreatorId = 1, CreatedDateTime = new DateTime(2014, 4, 23, 12, 30, 0) },
				new Album { Id = 2, CreatorId = 2, CreatedDateTime = new DateTime(2014, 4, 21, 12, 30, 0) },
				new Album { Id = 3, CreatorId = 1, CreatedDateTime = new DateTime(2014, 3, 21, 12, 30, 0) },
			};

			foreach (var album in albums)
			{
				_ctx.Albums.Add(album);
			}
			_ctx.SaveChanges();
		}

		private void SeedPhotos()
		{
			var photos = new List<Photo>
			{
				new Photo { Id = 1, CreatorId = 1, Title = "ski photo 1, user 1"},
				new Photo { Id = 2, CreatorId = 2, Title = "ski photo 2, user 2" },
				new Photo { Id = 3, CreatorId = 1, Title = "ski photo 3, user 1" },
			};

			foreach (var photo in photos)
			{
				_ctx.Photos.Add(photo);
			}
			_ctx.SaveChanges();
		}
	}
}
