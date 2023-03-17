# dotnet-otlp-elk
Dotnet with OpenTelemetry + ELK Stack - 1 Web + 2 API

## Requirenments
Docker with ELK + APM 
See https://github.com/deviantony/docker-elk

Example of Fleet-enrolled Elastic Agent pre-configured with an agent policy
for running the APM Server integration (see kibana.yml).
Run with
```
docker-compose  -f docker-compose.yml -f extensions/fleet/fleet-compose.yml -f extensions/fleet/agent-apmserver-compose.yml up
```

![Running Docker with ELK + APM .](/assets/images/DockerELK-APM.png)

### Start Notes
The first try the Kibana didn't start with the user - kibana_system
Had to change the password "changeme" of "kibana_system"
```
docker-compose exec elasticsearch bin/elasticsearch-reset-password --batch --user kibana_system
```	
Then copy the new pass to .env

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
