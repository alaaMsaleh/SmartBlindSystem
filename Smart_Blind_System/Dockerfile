FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . .

RUN dotnet restore "Smart_Blind_System/Smart_Blind_System.API.csproj"

RUN dotnet publish "Smart_Blind_System/Smart_Blind_System.API.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}

# EXPOSE 8080  # احذفيه أو سيبيه، مش فارق

ENTRYPOINT ["dotnet", "Smart_Blind_System.API.dll"]