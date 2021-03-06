using System;
using System.Collections.Generic;
using System.Net;
using ServiceStack.ServiceHost;

namespace ServiceStack.Common.Web
{
	public class HttpError : Exception, IHttpError
	{
		public HttpError() : this(null) {}

		public HttpError(string message)
			: this(HttpStatusCode.InternalServerError, message) {}

		public HttpError(HttpStatusCode statusCode, string errorCode)
			: this(statusCode, errorCode, null) {}

		public HttpError(HttpStatusCode statusCode, string errorCode, string errorMessage)
			: base(errorMessage ?? errorCode)
		{
			this.ErrorCode = errorCode;
			this.StatusCode = statusCode;
			this.Headers = new Dictionary<string, string>();
		}

		public HttpError(HttpStatusCode statusCode, Exception innerException)
			: this(innerException.Message, innerException)
		{
			this.StatusCode = statusCode;
		}

		public HttpError(string message, Exception innerException) : base(message, innerException)
		{
			if (innerException != null)
			{
				this.ErrorCode = innerException.GetType().Name;
			}
			this.Headers = new Dictionary<string, string>();			
		}

		public string ErrorCode { get; set; }

		public string ContentType { get; set; }

		public Dictionary<string, string> Headers { get; set; }

		public HttpStatusCode StatusCode { get; set; }

		public object Response { get; set; }

		public IContentTypeWriter ResponseFilter { get; set; }
		
		public IRequestContext RequestContext { get; set; }

		public IDictionary<string, string> Options
		{
			get { return this.Headers; }
		}
	}
}