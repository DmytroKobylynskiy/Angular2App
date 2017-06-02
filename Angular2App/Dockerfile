FROM microsoft/dotnet:1.0.0-core
WORKDIR /app
ENV ASPNETCORE_URLS http://*:8090
EXPOSE 8090
ENTRYPOINT ["dotnet", "Angular2App.dll"]
COPY . /app
