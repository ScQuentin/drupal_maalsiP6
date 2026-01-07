FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie tout
COPY . .

RUN echo "=== Contenu de /src ===" && ls -la /src
RUN echo "=== Recherche de tous les .csproj ===" && find /src -name "*.csproj" -type f
RUN echo "=== Contenu de /src/drupal (si existe) ===" && ls -la /src/drupal || echo "Le dossier /src/drupal n'existe pas"


# Publish depuis le projet Drupal (avec majuscule)
WORKDIR "/src/drupal"
RUN dotnet publish "Drupal.csproj" -c Release -o /app/publish

# Image finale
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Drupal.dll"]