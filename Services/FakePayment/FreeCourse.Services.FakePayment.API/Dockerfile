FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim as build
WORKDIR /app
EXPOSE 8080
COPY Shared/FreeCourse.Shared/*.csproj Shared/FreeCourse.Shared/
COPY Services/FakePayment/FreeCourse.Services.FakePayment.API/*.csproj Services/FakePayment/FreeCourse.Services.FakePayment.API/
RUN dotnet restore Services/FakePayment/FreeCourse.Services.FakePayment.API/*.csproj
COPY . .
RUN dotnet publish Services/FakePayment/FreeCourse.Services.FakePayment.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "FreeCourse.Services.FakePayment.API.dll" ]