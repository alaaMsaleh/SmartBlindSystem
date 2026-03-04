FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# نسخ كل ملفات المشروع (Domain, Service, Infrastructure, API)
COPY . ./

# تنفيذ الـ Restore والـ Publish للمشروع الأساسي مباشرة
RUN dotnet restore "Smart_Blind_System/Smart_Blind_System.API.csproj"
RUN dotnet publish "Smart_Blind_System/Smart_Blind_System.API.csproj" -c Release -o out

# المرحلة الثانية: التشغيل
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Smart_Blind_System.API.dll"]
