using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrientDb.Common;
using RestSharp;

namespace OrientDb.OrientApiCommands
{
    public class Batch<T>:OrientApiCommand<OrientQueryBatch,T> 
    {
        public Batch(OrientQueryBatch input, IRestClient client) : base(input, client)
        {
        }
        
        private string BatchUrl=> 
            $"http://{Input.Server}:{Input.Port}/batch/{Input.Database}";

        private object BatchObject =>
            new
            {
                transaction = true,
                operations = new[]
                {
                    new
                    {
                        type = "script",
                        language = "sql",
                        script = Input.GetSqlStatements()
                    }
                }
            };
        
        private IRestRequest BatchRequest =>
            new RestRequest($"{BatchUrl}", Method.POST)
                .AddHeader("content-type", "application/x-www-form-urlencoded")
                .AddHeader("Cookie", $"OSESSIONID={Input.SessionId}")
                .AddHeader("Accept-Encoding", "gzip,deflate")
                .AddJsonBody(BatchObject);
        
        public override Task<Result<T, Exception>> Execute() => ExecuteInternal();
        
        private async Task<Result<T, Exception>> ExecuteInternal()
        {
            Debug.WriteLine($"start executing batch on orient database - {BatchUrl} , with query \r\n\t{string.Join("\r\n\t",Input.GetSqlStatements())}");

            var request = BatchRequest;
            
            
            var response = await Client.ExecuteAsync<DefaultResult>(request,Method.POST);
            if (!response.IsSuccessful)
                return Result<T, Exception>.Err(new OrientCommandException<OrientQueryBatch>(request,response,Input));
            
            Debug.WriteLine($"successful response from orient database - {BatchUrl}");

            return Result<T, Exception>.Ok(response.Data.Result);
        }

      
        private class DefaultResult
        {
            [JsonProperty("result")]
            public T Result { get; set; }
        }
        
       
    }
}