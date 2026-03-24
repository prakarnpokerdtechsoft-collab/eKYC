# ==========================================
# Stage 1: Base image สำหรับรันแอปพลิเคชัน
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
# EXPOSE 8081 # เปิดคอมเมนต์หากใช้ HTTPS

# ==========================================
# Stage 2: Build image สำหรับคอมไพล์โค้ด
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# คัดลอกแค่ไฟล์ .csproj เข้ามาก่อนเพื่อทำ Restore (ใช้ Cache ของ Docker ให้เป็นประโยชน์)
COPY ["Onboarding/Onboarding.csproj", "Onboarding/"]
RUN dotnet restore "Onboarding/Onboarding.csproj"

# คัดลอกไฟล์ทั้งหมดที่เหลือเข้ามา
COPY . .

# ย้ายเข้าไปที่โฟลเดอร์ของโปรเจกต์และสั่ง Build
WORKDIR "/src/Onboarding"
RUN dotnet build "Onboarding.csproj" -c Release -o /app/build

# ==========================================
# Stage 3: Publish
# ==========================================
FROM build AS publish
RUN dotnet publish "Onboarding.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ==========================================
# Stage 4: Final image (นำไฟล์ที่ Publish แล้วมารัน)
# ==========================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Onboarding.dll"]