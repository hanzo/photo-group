﻿using System;
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

	    public HttpResponseMessage Post([FromBody] AlbumModel model)
	    {
		    try
		    {
			    var entity = TheModelFactory.Parse(model);

			    if (entity == null)
				    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid album defined in body.");

				// is this the right place to do this? 
				entity.CreatorId = _identityService.CurrentUser;

			    TheRepository.Insert(entity);

			    if (TheRepository.SaveAll())
			    {
					return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(entity));
			    }
			    else
			    {
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the database.");
				}
		    }
		    catch (Exception ex)
		    {
			    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Exception thrown while parsing.");
		    }
	    }
    }
}
