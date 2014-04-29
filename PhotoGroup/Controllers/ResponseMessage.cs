using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhotoGroup.Controllers
{
	//TODO: should this live in a different part of the code? 

	public class PhotoGroupActionResult : IHttpActionResult 
	{
		private readonly HttpRequestMessage _request;

		public PhotoGroupActionResult(HttpRequestMessage request)
		{
			_request = request;
		}

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			var response = _request.CreateResponse(HttpStatusCode.Created);
			response.Headers.Location = new Uri("www.foo.com");
			return Task.FromResult(response);
		}
	}
}