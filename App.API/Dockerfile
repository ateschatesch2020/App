FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY ./App.Application/*.csproj ./App.Application/
COPY ./App.Domain/*.csproj ./App.Domain/
COPY ./App.Caching/*.csproj ./App.Caching/
COPY ./App.Persistence/*.csproj ./App.Persistence/
COPY ./App.API/*.csproj ./App.API/
COPY *.sln .
COPY . .
RUN dotnet restore
RUN dotnet publish ./App.API/App.API.csproj -c Release -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /publish/ .
ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "App.API.dll"]