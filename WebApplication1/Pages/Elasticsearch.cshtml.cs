﻿using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Buffers.Text;
using System.Runtime.Intrinsics.X86;

namespace WebApplication1.Pages
{
    public class ElasticsearchModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string ElasticsearchServerUrl { get; set; }
        public string AppSearchAPIKey { get; set; }

        public dynamic SearchResult { get; set; }


        public ElasticsearchModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            ElasticsearchServerUrl = config["ElasticsearchServerUrl"];
            AppSearchAPIKey = config["AppSearchAPIKey"];
        }

        public void OnGet()
        {
            var settings = new ElasticsearchClientSettings(new Uri(ElasticsearchServerUrl))
                //.CertificateFingerprint("<FINGERPRINT>")
                //.Authentication(new BasicAuthentication("<USERNAME>", "<PASSWORD>"));
                .Authentication(new ApiKey(AppSearchAPIKey)); 

            var client = new ElasticsearchClient(settings);
            //Elasticsearch index-based - national-parks-demo-index-engine
            //
            var res = client.SearchShards();
            //https://www.elastic.co/guide/en/elasticsearch/client/net-api/8.6/examples.html
            var x = client.Search<NationalParkDemoDoc>(s => s
            .Index("search-national-parks-demo")    
            .Size(10)
            );

            SearchResult = res;
        }
        class NationalParkDemoDoc
        {
            public string id { get; set; }
            public string title { get; set; }
            public string states { get; set; }
            public int visitors { get; set; }

        }
    }
}