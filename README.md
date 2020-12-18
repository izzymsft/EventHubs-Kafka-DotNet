# EventHubs-Kafka-DotNet
Sample .NET client interacting with Apache Kafka (Event Hubs)


## Get the Code and Navigate to the Dotnet Code Base

Fetch the code from the repository, edit the connection settings and compile it

```shell

# Clone the Git Repo
git clone git@github.com:izzymsft/EventHubs-Kafka-DotNet.git

# Switch to the Directory for the Code
cd EventHubs-Kafka-DotNet/SweetStreams

```


## Edit the Program.cs to set your Connection Strings

```csharp

static void Main(string[] args)
{
        
            var brokerList = "sweetstreams.servicebus.windows.net:9093";
            string connectionString = "Endpoint=sb://sweetstreams.servicebus.windows.net/;SharedAccessKeyName=Izzy;SharedAccessKey=AIE54oZFiGNuz89FIqSjHTOfTcsNhVfMG+kyOnCmKx8=;EntityPath=salmonriver";
            string topic = "salmonriver";
            string cacertlocation = "cacert.pem";
            string consumerGroup = "riverdreams2";
            
 }

```


## Compile the Code
```shell

# Fetch All the Dependencies
dotnet restore

# Compile the Code
dotnet build

```

## Running the Producer Code

Once the code is compiled, you can generate some messages

```shell
dotnet run producer
```

## Running the Consumer Code

Once the messages have been generated you can consume them

```shell
dotnet run consumer
```
