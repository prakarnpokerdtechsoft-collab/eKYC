# 1. Base Stage: สำหรับรันแอปพลิเคชัน (ใช้ image ขนาดเล็ก)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
# เปิด Port มาตรฐานของ .NET 8 ขึ้นไป (ปรับเป็น 80/443 ได้ตามที่ตั้งค่าในแอป)
EXPOSE 8080
EXPOSE 8081

# 2. Build Stage: สำหรับคอมไพล์โค้ด (ใช้ SDK image)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy เฉพาะไฟล์ .csproj มาก่อนเพื่อ Restore dependencies (ใช้ Cache ของ Docker ให้เป็นประโยชน์)
COPY ["Onboarding/Onboarding.csproj", "Onboarding/"]
RUN dotnet restore "./Onboarding/Onboarding.csproj"

# Copy Source Code ทั้งหมดแล้วทำการ Build
COPY . .
WORKDIR "/src/Onboarding"
RUN dotnet build "./Onboarding.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3. Publish Stage: นำโค้ดที่ Build แล้วมา Publish เตรียมนำไปรัน
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Onboarding.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4. Final Stage: นำไฟล์ที่ Publish แล้วไปใส่ใน Base Image เพื่อนำไปใช้งานจริง
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Onboarding.dll"]