# Gets the official Docker image for .NET from MS
FROM microsoft/dotnet:2.2-sdk

# Sets the working directory
WORKDIR /app

# Copies the current directory to the image
COPY . ./

RUN dotnet restore

EXPOSE 5000
EXPOSE 5001
CMD ["dotnet", "watch", "run"]
