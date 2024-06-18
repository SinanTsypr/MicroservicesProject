FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 8080
COPY Shared/FreeCourse.Shared/*.csproj Shared/FreeCourse.Shared/
COPY Services/Catalog/FreeCourse.Services.Catalog.API/*.csproj Services/Catalog/FreeCourse.Services.Catalog.API/
RUN dotnet restore Services/Catalog/FreeCourse.Services.Catalog.API/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/FreeCourse.Services.Catalog.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "FreeCourse.Services.Catalog.API.dll" ]