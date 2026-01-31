FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY BulkyBook.sln ./
COPY BulkyBookWeb/BulkyBookWeb.csproj BulkyBookWeb/
COPY BulkyBook.Domain/BulkyBook.Domain.csproj BulkyBook.Domain/
COPY BulkyBook.Application/BulkyBook.Application.csproj BulkyBook.Application/
COPY BulkyBook.Infrastructure/BulkyBook.Infrastructure.csproj BulkyBook.Infrastructure/
RUN dotnet restore

COPY BulkyBookWeb/ BulkyBookWeb/
COPY BulkyBook.Domain/ BulkyBook.Domain/
COPY BulkyBook.Application/ BulkyBook.Application/
COPY BulkyBook.Infrastructure/ BulkyBook.Infrastructure/
WORKDIR /src/BulkyBookWeb
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ApplyMigrations=true
EXPOSE 8080

COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "BulkyBookWeb.dll"]
