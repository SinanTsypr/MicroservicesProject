FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 8080
COPY Shared/FreeCourse.Shared/*.csproj Shared/FreeCourse.Shared/
COPY Services/Discount/FreeCourse.Services.Discount.API/*.csproj Services/Discount/FreeCourse.Services.Discount.API/
RUN dotnet restore Services/Discount/FreeCourse.Services.Discount.API/*.csproj
COPY . .
RUN dotnet publish Services/Discount/FreeCourse.Services.Discount.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "FreeCourse.Services.Discount.API.dll" ]