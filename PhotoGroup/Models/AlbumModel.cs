using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGroup.Models
{
	public class AlbumModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int CreatorId { get; set; }

		public DateTime CreatedDateTime { get; set; }

		public string Url { get; set; }
	}
}