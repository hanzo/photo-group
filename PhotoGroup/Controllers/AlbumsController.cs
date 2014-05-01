using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Http.Routing;
using PhotoGroup.Data;
using PhotoGroup.Models;
using PhotoGroup.Services;

namespace PhotoGroup.Controllers
{
    public class AlbumsController : BaseApiController
    {
	    private IPhotoGroupIdentityService _identityService;

	    private const int PAGE_SIZE = 1;

	    public AlbumsController(IPhotoGroupRepository repo, IPhotoGroupIdentityService identityService) 
			: base(repo)
	    {
		    _identityService = identityService;
	    }

		public IHttpActionResult Get(int id)
		{
			var userId = _identityService.CurrentUser;
			var album = TheRepository.GetAlbum(id);

			if (album == null)
				return NotFound();

			if (album.CreatorId != userId)
			{
				//TODO: log something about access rights
				return Unauthorized();
			}

			return Versioned(TheModelFactory.Create(album));
		}

		public IHttpActionResult GetAlbumPhotos(int albumId, int page = 0)
		{
			var userId = _identityService.CurrentUser;
			var album = TheRepository.GetAlbum(albumId);

			if (album == null)
				return NotFound();

			if (album.CreatorId != userId)
			{
				//TODO: log something about access rights
				return Unauthorized();
			}

			var baseQuery = TheRepository.GetPhotosForAlbum(albumId)
				.OrderBy(p => p.PhotoDateTime);

			var totalCount = baseQuery.Count();

			// ensure we round up our page count to get the last partial page
			var totalPages = Math.Ceiling((double) totalCount/PAGE_SIZE);

			var results = baseQuery
				.Skip(PAGE_SIZE * page)
				.Take(PAGE_SIZE)
				.ToList()
				.Select(p => TheModelFactory.Create(p));

			var urlHelper = new UrlHelper(Request);
			var prevPageUrl = page > 0 ? urlHelper.Link("AlbumPhotos", new { page = page - 1 }) : "";
			var nextPageUrl = page < totalPages - 1 ? urlHelper.Link("AlbumPhotos", new { page = page + 1 }) : "";

			return Versioned(
				new 
				{
					TotalCount = totalCount,
					TotalPages = totalPages,
					PrevPageUrl = prevPageUrl,
					NextPageUrl = nextPageUrl,
					Results = results,
				});
		}

		public IHttpActionResult Get()
		{
			var userId = _identityService.CurrentUser;
			var results = TheRepository.GetAlbumsForUser(userId)
				.ToList()
				.Select(a => TheModelFactory.Create(a));
			return Versioned(results);
		}

		//TODO: should we disallow POST to ~/albums/{id}? it currently returns 201 
		//public VersionedActionResult Post([FromBody] AlbumModel model)
	    public IHttpActionResult Post([FromBody] AlbumModel model)
	    {
		    try
		    {
			    var entity = TheModelFactory.Parse(model);

			    if (entity == null)
				    return BadRequest("Invalid album defined in body.");

				// is this the right place to do this? 
				entity.CreatorId = _identityService.CurrentUser;

			    TheRepository.Insert(entity);

			    if (TheRepository.SaveAll())
			    {
				    return Versioned(entity);
			    }
			    else
			    {
				    return BadRequest("Could not save to the database.");
			    }
		    }
		    catch (Exception ex)
		    {
			    return BadRequest("Exception thrown while parsing.");
		    }
	    }

		public IHttpActionResult Delete(int id)
		{
			try
			{
				//TODO: need permission check
				if (TheRepository.GetAlbum(id) == null)
				{
					return NotFound();
				}

				if (TheRepository.DeleteAlbum(id) && TheRepository.SaveAll())
				{
					return Ok(); //TODO: convert to Versioned
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception ex)
			{
				return BadRequest("Exception was thrown");
			}
		}

		[HttpPut]
		[HttpPatch]
		public IHttpActionResult Patch(int id, [FromBody] AlbumModel model)
		{
			try
			{
				//TODO: need permission check
				var entity = TheRepository.GetAlbum(id);

				if (entity == null)
					return NotFound();

				var parsedValue = TheModelFactory.Parse(model);

				if (parsedValue == null)
					return BadRequest();

				if (entity.Title != parsedValue.Title)
				{
					entity.Title = parsedValue.Title;
					if (TheRepository.SaveAll())
						return Ok(); //TODO: convert to Versioned
				}
				return BadRequest();
			}
			catch (Exception ex)
			{
				return BadRequest("Exception was thrown");
			}
		}
    }
}
