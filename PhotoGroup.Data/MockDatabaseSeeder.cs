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
			//ExecuteQueries(
			//	"DELETE FROM Albums",
			//	"DELETE FROM Photos"
			//  );

			if (_ctx.Photos.Count() > 0)
			{
				return;
			}

			SeedAlbums();
			SeedPhotos();
		}

		private void SeedAlbums()
		{
			var album1 = new Album { Id = 1, CreatorId = 1, CreatedDateTime = new DateTime(2014, 4, 23, 12, 30, 0) };

			_ctx.Albums.Add(album1);

			_ctx.SaveChanges();
		}

		private void SeedPhotos()
		{
			var photos = new List<Photo>
			{
				new Photo { Id = 1, UploaderId = 1, Title = "ski photo 1, user 1"},
				new Photo { Id = 2, UploaderId = 2, Title = "ski photo 2, user 2" },
				new Photo { Id = 3, UploaderId = 1, Title = "ski photo 3, user 1" },
			};

			foreach (var photo in photos)
			{
				_ctx.Photos.Add(photo);
			}
			_ctx.SaveChanges();
		}
	}
}
