# Usa la imagen base de .NET 9 SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia el archivo del proyecto y restaura las dependencias
COPY ["Dunware.Blog/Dunware.Blog.csproj", "Dunware.Blog/"]
RUN dotnet restore "Dunware.Blog/Dunware.Blog.csproj"

# Copia el resto de los archivos y compila la aplicación
COPY . .
WORKDIR "/src/Dunware.Blog"
RUN dotnet build "Dunware.Blog.csproj" -c Release -o /app/build

# Publica la aplicación
RUN dotnet publish "Dunware.Blog.csproj" -c Release -o /app/publish

# Usa la imagen base de .NET 9 Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copia los archivos publicados desde la etapa de compilación
COPY --from=build /app/publish .

# Expone el puerto en el que la aplicación escucha
EXPOSE 80

# Define el comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Dunware.Blog.dll"]