using System;
using System.Threading.Tasks;
using OrientDb.Common;
using RestSharp;

namespace OrientDb.OrientApiCommands
{
    public abstract class OrientApiCommand<TIn,TOut>
    {
        protected TIn Input { get; }
        protected readonly IRestClient Client;

        public OrientApiCommand(TIn input,IRestClient client)
        {
            Input = input;
            Client = client;
        }

        public abstract Task<Result<TOut, Exception>> Execute();
        
        
    }
}