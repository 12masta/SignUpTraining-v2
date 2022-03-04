FROM mcr.microsoft.com/playwright:focal AS base
WORKDIR /app

RUN apt-get update && \
      apt-get -y install sudo

RUN useradd -m docker && echo "docker:docker" | chpasswd && adduser docker sudo

RUN curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg

RUN sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg

RUN sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'

RUN sudo apt-get update; \
      sudo apt-get install -y apt-transport-https && \
      sudo apt-get update && \
      sudo apt-get install -y dotnet-sdk-6.0 

FROM mcr.microsoft.com/playwright:focal AS build

WORKDIR /src

RUN apt-get update && \
      apt-get -y install sudo

RUN useradd -m docker && echo "docker:docker" | chpasswd && adduser docker sudo

RUN curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg

RUN sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg

RUN sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'

RUN sudo apt-get update; \
      sudo apt-get install -y apt-transport-https && \
      sudo apt-get update && \
      sudo apt-get install -y dotnet-sdk-6.0 

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
