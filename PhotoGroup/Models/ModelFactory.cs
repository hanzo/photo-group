using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using PhotoGroup.Data;
using PhotoGroup.Data.Entities;

namespace PhotoGroup.Models
{
	public class ModelFactory
	{
		private UrlHelper _urlHelper;
		private IPhotoGroupRepository _repo;

		public ModelFactory(HttpRequestMessage message, IPhotoGroupRepository repo)
		{
			_urlHelper = new UrlHelper(message);
			_repo = repo;
		}

		public PhotoModel Create(Photo photo)
		{
			if (photo == null)
				return null;

			return new PhotoModel
			{
				Id = photo.Id,
				Title = photo.Title,
				UploaderId = photo.CreatorId,
				AlbumId = photo.AlbumId,
				Url = _urlHelper.Link("Photos", new {id = photo.Id})
			};
		}

		public UserModel Create(User user)
		{
			if (user == null)
				return null;

			return new UserModel
			{
				Id = user.Id,
				Name = String.Format("{0} {1}", user.FirstName, user.LastName)
			};
		}

		public AlbumModel Create(Album album)
		{
			if (album == null)
				return null;

			return new AlbumModel
			{
				Id = album.Id,
				CreatorId = album.CreatorId,
				Title = album.Title,
				CreatedDateTime = album.CreatedDateTime,
				Url = _urlHelper.Link("Albums", new { id = album.Id }),
				//Photos = album.Photos == null ? null : album.Photos.Select(p => Create(p)),
				Photos = _repo.GetPhotosForAlbum(album.Id).ToList().Select(p => Create(p)),
			};
		}

		public Album Parse(AlbumModel model)
		{
			try
			{
				var album = new Album();

				if (model.Title != default(string))
				{
					album.Title = model.Title;
				}

				// should this happen in the albums controller?
				album.CreatedDateTime = DateTime.UtcNow;

				return album;
			}
			catch
			{
				return null;
			}
		}
	}
}