# Getting Started

Start the `http` Launch Configuration of the Aspire Host `Agora.AppHost` which will automatically open the Aspire Dashboard
### Install dotnet Aspire

Update .Net workload
```shell
dotnet workload update
```

Install Aspire via .Net workload
```shell
dotnet workload install aspire
```

List installed .Net workloads to verify Aspire is installed
```shell
dotnet workload list
```

### Setup HTTPS with a development certificate

Create a development certificate via .Net CLI
```shell
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\Agora.Simulator.pfx -p <password> --trust
```

Set the password of the newly created development certificate as a user secret
```shell
dotnet user-secrets -p Agora.Simulator\Agora.Simulator.csproj set "Kestrel:Certificates:Default:Password" "<password>"
```
### Build the Docker image and Start the Docker compose stack

Build a Docker image of the Agora.Simulator
```shell
docker build -f Agora.Simulator/Dockerfile -t agora-simulator .       
```

Afterwards you can start Docker compose stack 
```shell
docker-compose up -d
```

### Take a look at the Grafana dashboard

Open `localhost:3000` within the browser and login to Grafana

![grafana-screenshot.png](grafana-screenshot.png)