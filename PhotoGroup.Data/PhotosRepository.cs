using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoGroup.Data.Entities;

namespace PhotoGroup.Data
{
	public class PhotosRepository : IPhotosRepository
	{
		public Photo GetPhoto(int id)
		{
			return new Photo {Id = 1, Title = "ski photo 1"};
		}

		public IQueryable<Photo> GetAllPhotosForUser(int userId)
		{
			return new List<Photo>
			{
				new Photo {Id = 1, Title = "ski photo 1"},
				new Photo {Id = 2, Title = "ski photo 2"}
			}.AsQueryable();
		}
	}
}
