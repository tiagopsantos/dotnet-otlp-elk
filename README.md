# dotnet-otlp-elk
Dotnet with OpenTelemetry + ELK Stack - 1 Web + 2 API

## Requirenments
Docker with ELK + APM + ElastisSearch
See https://github.com/deviantony/docker-elk

Example of Fleet-enrolled Elastic Agent pre-configured with an agent policy
for running the APM Server integration (see kibana.yml).
Run with
```
docker-compose  -f docker-compose.yml -f extensions/fleet/fleet-compose.yml -f extensions/fleet/agent-apmserver-compose.yml docker-compose  -f docker-compose.yml -f extensions/fleet/fleet-compose.yml -f extensions/fleet/agent-apmserver-compose.yml -f extensions/enterprise-search/enterprise-search-compose.yml up
```

![Running Docker with ELK + APM .](/assets/images/DockerELK-APM.png)

### Start Notes
The first try the Kibana didn't start with the user - kibana_system
Had to change the password "changeme" of "kibana_system"
```
docker-compose exec elasticsearch bin/elasticsearch-reset-password --batch --user kibana_system
```	
Then copy the new pass to .env

### Testing with ElasticSearch - AppSearch
To enable the management experience for Enterprise Search, modify the Kibana configuration file in
[`kibana/config/kibana.yml`][config-kbn] and add the following setting:
```yaml
enterpriseSearch.host: http://enterprise-search:3002
```	


## Project dotnet
Set multiple Start Visual Studio - Web, Api1, Api2
Will start the https profile configs 

### Configurations
- ApiApplication1 (calls api2)
```
  "Api2Url": "https://localhost:7132",
  "elk-apm-server": "http://localhost:8200"
``` 

- ApiApplication2 (standalone)
```
  "elk-apm-server": "http://localhost:8200"
```


- WebApplication1 (calls api1 & api2)
```
  "elk-apm-server": "http://localhost:8200",

  "Api1Url": "https://localhost:7199",
  "Api2Url": "https://localhost:7132"
```


## DEMO
### ServiceMap 
![ServiceMap .](/assets/images/DEMO-servicemap.png)

### Trace Timeline
![trace-timeline](/assets/images/DEMO-trace-timeline.png)

### Discover
![discover.](/assets/images/DEMO-discover.png)

## DEMO Log Exception
### Trace Timeline
![trace-timeline](/assets/images/DEMO-trace-timeline-exception.png)

### Observability To Discover
![Observability To Discover.](/assets/images/DEMO-observabilityToDiscover.png)



## ElasticSearch - API
### Create Index (API) 
![Elasticsearch-Index-API](/assets/images/Elasticsearch-Index-API.png)

### Add Documents via API (curl, postman...)
![AddDocuments](/assets/images/Elasticsearch-Index-AddDocuments.png)

### Browse Documents
![BrowseDocuments](/assets/images/Elasticsearch-Index-BrowseDocuments.png)

### .NET  ElasticSearchClient - Search by index
![DotNetSearch](/assets/images/Elasticsearch-Index-DotNetSearch.png)