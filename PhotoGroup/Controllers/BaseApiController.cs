using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoGroup.Data;
using PhotoGroup.Models;

namespace PhotoGroup.Controllers
{
    public abstract class BaseApiController : ApiController
    {
		private IPhotoGroupRepository _repo;
	    private ModelFactory _modelFactory;

	    public BaseApiController(IPhotoGroupRepository repo)
	    {
		    _repo = repo;
	    }

	    protected IPhotoGroupRepository TheRepository
	    {
		    get { return _repo; }
	    }

	    protected ModelFactory TheModelFactory
	    {
		    get
		    {
			    if (_modelFactory == null)
			    {
				    _modelFactory = new ModelFactory(this.Request, TheRepository);
			    }
			    return _modelFactory;
		    }
	    }
    }
}
