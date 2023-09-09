FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["src/ZboxOrleans/ZboxOrleans.Client", "ZboxOrleans.Client/"]
COPY ["src/ZboxOrleans/ZboxOrleans.Silo", "ZboxOrleans.Silo/"]
COPY ["src/ZboxOrleans/ZboxOrleans.GrainInterfaces", "ZboxOrleans.GrainInterfaces/"]
COPY ["src/ZboxOrleans/ZboxOrleans.Grains", "ZboxOrleans.Grains/"]

COPY ["./Directory.Packages.props", "./"]

RUN dotnet restore "ZboxOrleans.Client/ZboxOrleans.Client.csproj"

RUN dotnet build "ZboxOrleans.Client/ZboxOrleans.Client.csproj" -c Release -o /app/build --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

WORKDIR /app
COPY --from=build /app/build .

ENTRYPOINT ["dotnet", "ZboxOrleans.Client.dll", "--environment=Development"]
