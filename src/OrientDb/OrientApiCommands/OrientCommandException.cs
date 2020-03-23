using System;
using System.Net.Http;
using RestSharp;

namespace OrientDb.OrientApiCommands
{
    public class OrientCommandException<T> : Exception
    {
        public IRestRequest Request { get; }
        public IRestResponse Response { get; }
        public T Input { get; }

        public OrientCommandException(IRestRequest request,IRestResponse response,T input):base(GetMessage(input),GetInnerException(request,response))
        {
            Request = request;
            Response = response;
            Input = input;
        }

        private static string GetMessage(T input) =>
            $"executing : {input} completed with error (see inner exception for details)";
        
        private static Exception GetInnerException(IRestRequest request,IRestResponse response)
        {
            var details = $@"<Url>
{request.Resource}
</Url>
<Error>
{response.ErrorMessage}
</Error>
<StatusCode>
{response.StatusDescription}
<StatusCode>
<Content>
{response.Content}
</Content>
<Exception>
{response.ErrorException}
</Exception>";
            return new HttpRequestException(details);
        }
       
    }
}