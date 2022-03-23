#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CalendarService.API/CalendarService.API.csproj", "CalendarService.API/"]
RUN dotnet restore "CalendarService.API/CalendarService.API.csproj"
COPY ["CalendarService.Infrastructure/CalendarService.Infrastructure.csproj", "CalendarService.Infrastructure/"]
RUN dotnet restore "CalendarService.Infrastructure/CalendarService.Infrastructure.csproj"
COPY ["CalendarService.Core/CalendarService.Core.csproj", "CalendarService.Core/"]
RUN dotnet restore "CalendarService.Core/CalendarService.Core.csproj"
COPY ["CalendarService.Test/CalendarService.Test.csproj", "CalendarService.Test/"]
RUN dotnet restore "CalendarService.Test/CalendarService.Test.csproj"
COPY . .
WORKDIR "/src/CalendarService.API"
RUN dotnet build "CalendarService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalendarService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "CalendarService.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CalendarService.API.dll