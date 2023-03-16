# dotnet-otlp-elk
Dotnet with OpenTelemetry + ELK Stack - 1 Web + 2 API

#Requirenments
Docker with ELK + APM 
See https://github.com/deviantony/docker-elk

Example of Fleet-enrolled Elastic Agent pre-configured with an agent policy
for running the APM Server integration (see kibana.yml).
Run with
docker-compose  -f docker-compose.yml -f extensions/fleet/fleet-compose.yml -f extensions/fleet/agent-apmserver-compose.yml up


#Start Notes
In the first try the Kibana didn't start with the user - kibana_system
Had to change the password "changeme" of "kibana_system"
	$ docker-compose exec elasticsearch bin/elasticsearch-reset-password --batch --user kibana_system
	
Then copy the new pass to .env

#Start Visual Studio
Set multiple start - Web, Api1, Api2