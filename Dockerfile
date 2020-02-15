#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["StudiTrain/StudiTrain.csproj", "StudiTrain/"]
RUN dotnet restore "StudiTrain/StudiTrain.csproj"
COPY . .
WORKDIR "/src/StudiTrain"
RUN dotnet build "StudiTrain.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudiTrain.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get update && \
    apt-get install -y openssh-server bash curl python
RUN mkdir /app/.profile.d && echo "[ -z \"$SSH_CLIENT\" ] && source <(curl --fail --retry 3 -sSL \"$HEROKU_EXEC_URL\")" > /app/.profile.d/heroku-exec.sh
RUN rm /bin/sh && ln -s /bin/bash /bin/sh
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet", "StudiTrain.dll"]
