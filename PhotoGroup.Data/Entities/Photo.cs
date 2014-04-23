using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGroup.Data.Entities
{
	public class Photo
	{
		public Photo()
		{

		}

		public int Id { get; set; }

		public string Title { get; set; }

		public int UploaderId { get; set; }
	}
}
