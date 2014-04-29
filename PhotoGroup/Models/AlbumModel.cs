using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoGroup.Data.Entities;

namespace PhotoGroup.Models
{
	public class AlbumModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int CreatorId { get; set; }

		public DateTime CreatedDateTime { get; set; }

		public string Url { get; set; }

		public IEnumerable<PhotoModel> Photos { get; set; }

		public IEnumerable<UserModel> Attendees { get; set; }
	}
}