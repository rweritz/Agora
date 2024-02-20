Start the `http` Launch Configuration of the Aspire Host `Agora.AppHost` which will automatically open the Aspire Dashboard

dotnet workload update
dotnet workload install aspire
dotnet workload list

dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\Agora.PersistenceWorker.pfx -p <password> --trust


```shell
docker build -f Agora.Simulator/Dockerfile -t agora-simulator .       
```

Afterwards you can start Prometheus and Grafana 
```shell
docker-compose up -d
```

Open `localhost:3000` within the browser and login to Grafana

![grafana-screenshot.png](grafana-screenshot.png)