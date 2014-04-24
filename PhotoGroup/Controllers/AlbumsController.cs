using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoGroup.Data;
using PhotoGroup.Models;
using PhotoGroup.Services;

namespace PhotoGroup.Controllers
{
    public class AlbumsController : BaseApiController
    {
	    private IPhotoGroupIdentityService _identityService;

	    public AlbumsController(IPhotoGroupRepository repo, IPhotoGroupIdentityService identityService) 
			: base(repo)
	    {
		    _identityService = identityService;
	    }

		public AlbumModel Get(int id)
		{
			var userId = _identityService.CurrentUser;
			var album = TheRepository.GetAlbum(id);

			if (album == null)
				return null;

			if (album.CreatorId != userId)
			{
				//TODO: log something about access rights
				return null;
			}

			return TheModelFactory.Create(album);
		}

		public IEnumerable<AlbumModel> Get()
		{
			var userId = _identityService.CurrentUser;
			var results = TheRepository.GetAlbumsForUser(userId)
				.ToList()
				.Select(a => TheModelFactory.Create(a));
			return results;
		}
    }
}
