using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGroup.Models
{
	public class PhotoModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int UploaderId { get; set; }

		//public string Url { get; set; }
	}
}