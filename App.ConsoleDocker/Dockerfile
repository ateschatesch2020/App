# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /myworkdir
COPY bin/Release/net10.0/publish/ /myworkdir/
ENTRYPOINT ["dotnet", "App.ConsoleDocker.dll"]
