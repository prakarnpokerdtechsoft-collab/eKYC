# =========================
# 1. Build Stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy csproj และ restore ก่อน (ช่วย cache layer)
COPY eKYC/Onboarding/Onboarding.csproj eKYC/Onboarding/
RUN dotnet restore eKYC/Onboarding/Onboarding.csproj

# copy source ทั้งหมด
COPY . .

# build
WORKDIR /src/eKYC/Onboarding
RUN dotnet publish Onboarding.csproj -c Release -o /app/publish

# =========================
# 2. Runtime Stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# copy จาก build stage
COPY --from=build /app/publish .

# expose port (ปรับตามโปรเจกต์)
EXPOSE 80
EXPOSE 443

# run app
ENTRYPOINT ["dotnet", "Onboarding.dll"]
