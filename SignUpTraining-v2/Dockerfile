FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SignUpTraining-v2/SignUpTraining-v2.csproj", "SignUpTraining-v2/"]
RUN dotnet restore "SignUpTraining-v2/SignUpTraining-v2.csproj"
COPY . .
WORKDIR "/src/SignUpTraining-v2"
RUN dotnet build "SignUpTraining-v2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignUpTraining-v2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignUpTraining-v2.dll"]
