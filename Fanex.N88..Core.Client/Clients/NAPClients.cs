namespace Fanex.N88.Core.Client
{
    using System;
    using System.Net.Http;

    internal sealed class NAPClients
    {
        internal NAPClients()
        {
        }

        public Func<HttpClient> CreateHttpClient { get; set; }
    }
}