# 1. Aşama: SDK ile derleme (.NET 9.0 Alpine)
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

COPY ["AuthService.csproj", "./"]
RUN dotnet restore "AuthService.csproj"

COPY . .
RUN dotnet publish "AuthService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Aşama: Çalıştırma ortamı (.NET 9.0 ASP.NET Alpine)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

# Güvenlik için yetkisiz (non-root) kullanıcı
RUN addgroup -g 10001 -S appgroup && \
    adduser -u 10001 -S appuser -G appgroup
USER appuser:appgroup

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "AuthService.dll"]