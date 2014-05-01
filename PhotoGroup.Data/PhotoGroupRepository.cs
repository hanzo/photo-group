using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoGroup.Data.Entities;

namespace PhotoGroup.Data
{
	public class PhotoGroupRepository : IPhotoGroupRepository
	{
		private PhotoGroupContext _ctx;
		public PhotoGroupRepository(PhotoGroupContext ctx)
		{
			_ctx = ctx;
						
			//TODO: move this to repository constructor
			var dbSeeder = new MockDatabaseSeeder(ctx);
			dbSeeder.Seed();
		}

		public bool SaveAll()
		{
			return _ctx.SaveChanges() > 0;
		}
		
		public Photo GetPhoto(int id)
		{
			return _ctx.Photos.FirstOrDefault(p => p.Id == id);
		}

		public IQueryable<Photo> GetAllPhotos()
		{
			return _ctx.Photos;
		}

		public IQueryable<Photo> GetPhotosForAlbum(int albumId)
		{
			return _ctx.Photos.Where(p => p.AlbumId == albumId);
		}

		public IQueryable<Photo> GetAllPhotosForUser(int userId)
		{
			return _ctx.Photos.Where(p => p.CreatorId == userId);
		}

		public User GetUserInfo(int id)
		{
			return _ctx.Users.FirstOrDefault(u => u.Id == id);
		}

		public IQueryable<User> GetAllUsers()
		{
			return _ctx.Users;
		}

		public Album GetAlbum(int id)
		{
			//TODO: add permission checks
			return _ctx.Albums.FirstOrDefault(a => a.Id == id);
		}

		public IQueryable<Album> GetAlbumsForUser(int userId)
		{
			return _ctx.Albums.Where(a => a.CreatorId == userId);
		}

		public bool Insert(Album album)
		{
			try
			{
				_ctx.Albums.Add(album);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool DeleteAlbum(int id)
		{
			try
			{
				var entity = _ctx.Albums.FirstOrDefault(a => a.Id == id);
				if (entity != null)
				{
					_ctx.Albums.Remove(entity);
					return true;
				}
			}
			catch
			{
				// TODO Logging
			}

			return false;
		}
	}
}
