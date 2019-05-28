#  FROM mcr.microsoft.com/dotnet/core/sdk:2.1
# WORKDIR /app
# EXPOSE 29460
# # Copy csproj and restore as distinct layers
# COPY *.csproj ./
# RUN dotnet restore

# # Copy everything else and build
# COPY . ./
# RUN dotnet publish -c Release -o out

# # Build runtime image
# FROM microsoft/dotnet:aspnetcore-runtime
# WORKDIR /app
# COPY . ./ 
# ENTRYPOINT ["dotnet", "AuthenticationService.API.dll","--server.urls", "http://0.0.0.0:5000"]
FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 29460

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["KeepNoteAPIs/NoteService/NoteService.API/NoteService.API.csproj", "NoteService/NoteService.API/"]
RUN dotnet restore "NoteService/NoteService.API/NoteService.API.csproj"
COPY . .
WORKDIR "/src/NoteService.API/NoteService.API.csproj"
RUN dotnet build "NoteService.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NoteService.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NoteService.API.dll","--server.urls", "http://0.0.0.0:5000"]