FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

COPY . ./
CMD ["dotnet", "run", "--urls", "http://0.0.0.0:5001"]
