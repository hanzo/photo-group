using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using PhotoGroup.Data.Entities;

namespace PhotoGroup.Models
{
	public class ModelFactory
	{
		private UrlHelper _urlHelper;

		public ModelFactory(HttpRequestMessage message)
		{
			_urlHelper = new UrlHelper(message);
		}

		public PhotoModel Create(Photo photo)
		{
			if (photo == null)
				return null;

			return new PhotoModel
			{
				Id = photo.Id,
				Title = photo.Title,
				UploaderId = photo.UploaderId,
				Url = _urlHelper.Link("Photo", new { id = photo.Id })
			};
		}
	}
}