﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace PhotoGroup
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "Photos",
				routeTemplate: "api/photos/{id}",
				defaults: new { controller = "photos", id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "Users",
				routeTemplate: "api/users/{id}",
				defaults: new { controller = "users", id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "Albums",
				routeTemplate: "api/albums/{id}",
				defaults: new { controller = "albums", id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "AlbumPhotos",
				routeTemplate: "api/albums/{albumid}/photos",
				defaults: new { controller = "albums" }
			);

			// Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
			// To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
			// For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
			//config.EnableQuerySupport();

			// To disable tracing in your application, please comment out or remove the following line of code
			// For more information, refer to: http://www.asp.net/web-api
			config.EnableSystemDiagnosticsTracing();

			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}
