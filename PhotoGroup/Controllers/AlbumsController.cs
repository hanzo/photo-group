using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

		public object GetAlbumPhotos(int albumId, int page = 0)
		{
			var userId = _identityService.CurrentUser;
			var album = TheRepository.GetAlbum(albumId);

			if (album == null)
				return null;

			if (album.CreatorId != userId)
			{
				//TODO: log something about access rights
				return null;
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

			return new
			{
				TotalCount = totalCount,
				TotalPages = totalPages,
				PrevPageUrl = prevPageUrl,
				NextPageUrl = nextPageUrl,
				Results = results,
			};
		}

		public IEnumerable<AlbumModel> Get()
		{
			var userId = _identityService.CurrentUser;
			var results = TheRepository.GetAlbumsForUser(userId)
				.ToList()
				.Select(a => TheModelFactory.Create(a));
			return results;
		}

		//TODO: should we disallow POST to ~/albums/{id}? it currently returns 201 
		//public PhotoGroupActionResult Post([FromBody] AlbumModel model)
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
				    //return new PhotoGroupActionResult(Request);
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

		public HttpResponseMessage Delete(int id)
		{
			try
			{
				//TODO: need permission check
				if (TheRepository.GetAlbum(id) == null)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound);
				}

				if (TheRepository.DeleteAlbum(id) && TheRepository.SaveAll())
				{
					return Request.CreateResponse(HttpStatusCode.OK);
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest);
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Exception was thrown");
			}
		}

		[HttpPut]
		[HttpPatch]
		public HttpResponseMessage Patch(int id, [FromBody] AlbumModel model)
		{
			try
			{
				//TODO: need permission check
				var entity = TheRepository.GetAlbum(id);

				if (entity == null)
					return Request.CreateResponse(HttpStatusCode.NotFound);

				var parsedValue = TheModelFactory.Parse(model);

				if (parsedValue == null)
					return Request.CreateResponse(HttpStatusCode.BadRequest);

				if (entity.Title != parsedValue.Title)
				{
					entity.Title = parsedValue.Title;
					if (TheRepository.SaveAll())
						return Request.CreateResponse(HttpStatusCode.OK);
				}
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Exception was thrown");
			}
		}
    }
}
