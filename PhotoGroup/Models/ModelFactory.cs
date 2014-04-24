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
		public PhotoModel Create(Photo photo)
		{
			return new PhotoModel
			{
				Id = photo.Id,
				Title = photo.Title,
				UploaderId = photo.UploaderId
			};
		}
	}
}