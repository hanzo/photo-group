using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using PhotoGroup.Data;
using PhotoGroup.Models;
using PhotoGroup.Services;

namespace PhotoGroup.Controllers
{
    public class UsersController : BaseApiController
    {
	    private IPhotoGroupIdentityService _identityService;

	    public UsersController(IPhotoGroupRepository repo, IPhotoGroupIdentityService identityService) 
			: base(repo)
	    {
		    _identityService = identityService;
	    }

	    public IEnumerable<UserModel> Get()
	    {
		    var result = TheRepository.GetAllUsers()
			    .ToList()
			    .Select(u => TheModelFactory.Create(u));

		    return result;
	    }

	    public UserModel Get(int id)
	    {
		    var userId = _identityService.CurrentUser;
		    return TheModelFactory.Create(TheRepository.GetUserInfo(userId));
	    }
    }
}
