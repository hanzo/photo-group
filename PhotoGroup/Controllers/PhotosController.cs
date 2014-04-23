using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using PhotoGroup.Data;
using PhotoGroup.Data.Entities;
using PhotoGroup.Models;

namespace PhotoGroup.Controllers
{
    public class PhotosController : ApiController
    {
	    private IPhotosRepository _repo;
	    private ModelFactory _modelFactory;

	    public PhotosController(IPhotosRepository repo)
	    {
		    _repo = repo;
		    _modelFactory = new ModelFactory();
	    }

		public IEnumerable<Photo> Get()
		{
			return Enumerable.Repeat(_repo.GetPhoto(1), 1);
		}

	    public IEnumerable<PhotoModel> Get(string id)
	    {
			return _repo.GetAllPhotosForUser(Convert.ToInt32(id))
				.Select(f => _modelFactory.Create(f));
	    }
    }
}
