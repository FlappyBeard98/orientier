using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using OrientDb.Common;
using RestSharp;

namespace OrientDb.OrientApiCommands
{
    public class Connect :OrientApiCommand<OrientApiConnectionSettings,OrientConnection>
    {
        public Connect(OrientApiConnectionSettings input, IRestClient client) : base(input, client)
        {
        }
         
        private string ConnectionUrl=> 
            $"http://{Input.Server}:{Input.Port}/connect/{Input.Database}";

        private IRestRequest ConnectionRequest =>
            new RestRequest(ConnectionUrl, Method.GET)
            {
                Credentials = new NetworkCredential(Input.Username, Input.Password)
            };

        public override async Task<Result<OrientConnection, Exception>> Execute()
        {
            
            Debug.WriteLine($"connecting to orient database - {ConnectionUrl}");

            var request = ConnectionRequest;
            
            var response = await Client.ExecuteAsync(request,Method.GET);
            if (!response.IsSuccessful)
                return Result<OrientConnection, Exception>.Err(new OrientCommandException<OrientApiConnectionSettings>(request,response,Input));
            
            Debug.WriteLine($"successful response from orient database - {ConnectionUrl} ; trying get session");
            
            var session = response.Cookies.FirstOrDefault(p => p.Name == "OSESSIONID")?.Value;
            if(string.IsNullOrEmpty(session))
                throw new NullReferenceException("empty session");

            Debug.WriteLine($"successful connection to orient database - {ConnectionUrl} ; session - {session}");
            return Result<OrientConnection, Exception>.Ok(new OrientConnection(Input.Server,Input.Database,Input.Port,session));
        }

    }
}