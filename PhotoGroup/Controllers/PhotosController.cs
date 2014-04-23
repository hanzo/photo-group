using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoGroup.Data;
using PhotoGroup.Data.Entities;

namespace PhotoGroup.Controllers
{
    public class PhotosController : ApiController
    {
	    private IPhotosRepository _repo;

	    public PhotosController(IPhotosRepository repo)
	    {
		    _repo = repo;
	    }

		public IEnumerable<Photo> Get()
		{
			return Enumerable.Repeat(_repo.GetPhoto(1), 1);
		}

	    public Photo Get(string id)
	    {
		    return _repo.GetPhoto(Convert.ToInt32(id));
	    }
    }
}
