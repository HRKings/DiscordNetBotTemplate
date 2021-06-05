FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /source

LABEL author="HRKings"

# Copy main project
COPY *.csproj .
RUN dotnet restore

# Copy and publish app and libraries
COPY . .

# Run the project
ENTRYPOINT [ "dotnet", "run" ]