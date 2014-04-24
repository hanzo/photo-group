using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoGroup.Data;

namespace PhotoGroup.Controllers
{
    public class AlbumsController : BaseApiController
    {
	    public AlbumsController(IPhotoGroupRepository repo) : base(repo)
	    {
		    
	    }

		//public object Get()
		//{
		//	//var results = TheRepository.Get
		//}
    }
}
