﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGroup.Data.Entities
{
	public class Album
	{
		public int Id { get; set; }

		public int CreatorId { get; set; }

		public string Title { get; set; }

		public DateTime CreatedDateTime { get; set; }

		public ICollection<Photo> Photos { get; set; }

		public ICollection<User> Attendees { get; set; }
	}
}
