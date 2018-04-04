# Scheduling Application
An open source, web-based scheduling application created by students from 
Russia, Estonia and The Netherlands.

## Usage
```bash
# Clone the repository
git clone
"ssh://nathanbakhuijzen@vs-ssh.visualstudio.com:22/_ssh/scheduling-application" scheduling-application
cd scheduling-application
```

Run the project locally:
```bash
cd backend

# Restore all dependencies
dotnet restore

# Run database migrations
dotnet ef database update

# Run the project
dotnet run
```

Or build and run the application in a Docker stack:
```bash
cd docker

# Build the Docker images for this application
./build

# Start the stack
./start
# or ./startd to start as a daemon

# Stop the stack
./stop
```

## Other commands
General commands:
```bash
# Create a new migration
dotnet ef migrations add <migration_name>

# Run database migrations (and create a database)
dotnet ef database update

# Scaffold a controller with its views
dotnet aspnet-codegenerator --project . controller -name <ClassNameController> -m <ClassName> -dc DbEntity
```

## Config
It is possible to create a couple of settings for local use. Duplicate the `appsettings.json` and rename it to `appsettings.local.json`.  
Some of the config variables are:
* `Environment` (`Development`, `Staging` or `Production`)  
Environment variable is `Production` by default.
* `Database`
    * `Host`
    * `Database`
    * `User`
    * `Password`

## Requirements
* dotnet core 2.1 (preview) or higher

## Other
Project
* https://nathanbakhuijzen.visualstudio.com/scheduling-application

Google Drive
* https://goo.gl/ryUWC4

Dropbox
* https://goo.gl/DxuPWG
