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
    public class PhotosController : BaseApiController
    {
	    public PhotosController(IPhotoGroupRepository repo) : base(repo)
	    {
	    }		

	    public IEnumerable<PhotoModel> Get()
	    {
			return TheRepository.GetAllPhotos()
				.ToList()
				.Select(f => TheModelFactory.Create(f));
	    }

	    public PhotoModel Get(int id)
	    {
		    return TheModelFactory.Create(TheRepository.GetPhoto(id));
	    }

		//public IEnumerable<PhotoModel> Get(int id)
		//{
		//	return TheRepository.GetAllPhotosForUser(id)
		//		.ToList()
		//		.Select(f => TheModelFactory.Create(f));
		//}
    }
}
