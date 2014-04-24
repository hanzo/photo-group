using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGroup.Services
{
	public class PhotoGroupIdentityService : IPhotoGroupIdentityService
	{
		public int CurrentUser
		{
			get { return 1; }
		}
	}
}