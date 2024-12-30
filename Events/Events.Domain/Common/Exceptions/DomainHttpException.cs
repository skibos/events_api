using System.Net;

namespace Events.Domain.Common.Exceptions
{
	public class DomainHttpException : Exception
    {
		public HttpStatusCode StatusCode { get; set; }

        public DomainHttpException(string message) : base(message) { }
    }
}

