using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhotoGroup.ActionResults
{
	public class VersionedActionResult<T> : IHttpActionResult where T : class
	{
		private readonly HttpRequestMessage _request;
		private string _version;
		private T _body;

		public VersionedActionResult(HttpRequestMessage request, string version, T body)
		{
			_request = request;
			_version = version;
			_body = body;
		}

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			var response = _request.CreateResponse(_body);
			response.Headers.Location = new Uri("http://www.foo.com");
			response.Headers.Add("XXX-OurVersion", _version);
			return Task.FromResult(response);
		}
	}
}