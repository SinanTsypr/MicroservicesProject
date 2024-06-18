FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 8080
COPY Shared/FreeCourse.Shared/*.csproj Shared/FreeCourse.Shared/
COPY IdentityServer/FreeCourse.IdentityServer/*.csproj IdentityServer/FreeCourse.IdentityServer/
RUN dotnet restore IdentityServer/FreeCourse.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/FreeCourse.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "FreeCourse.IdentityServer.dll" ]