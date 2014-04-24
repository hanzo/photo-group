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
	    private IPhotoGroupRepository _repo;
	    private ModelFactory _modelFactory;

	    public PhotosController(IPhotoGroupRepository repo)
	    {
		    _repo = repo;
		    _modelFactory = new ModelFactory();
	    }		

	    public IEnumerable<PhotoModel> Get()
	    {
			return _repo.GetAllPhotos()
				.ToList()
				.Select(f => _modelFactory.Create(f));
	    }

		public IEnumerable<PhotoModel> Get(int id)
		{
			return _repo.GetAllPhotosForUser(id)
				.ToList()
				.Select(f => _modelFactory.Create(f));
		}
    }
}
