# Build stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copy solution and restore as distinct layers
COPY NoteShared.sln ./
COPY NoteShared.Api/NoteShared.Api.csproj NoteShared.Api/
COPY NoteShared.Domain.Core/NoteShared.Domain.Core.csproj NoteShared.Domain.Core/
COPY NoteShared.DTO/NoteShared.DTO.csproj NoteShared.DTO/
COPY NoteShared.Services/NoteShared.Services.csproj NoteShared.Services/
COPY NoteShared.Infrastructure.Data/NoteShared.Infrastructure.Data.csproj NoteShared.Infrastructure.Data/
RUN dotnet restore ./NoteShared.sln

# Copy everything else and publish
COPY . ./
WORKDIR /src/NoteShared.Api
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "NoteShared.Api.dll"]
