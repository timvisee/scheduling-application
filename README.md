# Scheduling Application
An open source, web-based scheduling application created by students from 
Russia, Spain, Estonia and The Netherlands.

## Usage
```bash
# Clone the repository
git clone
"ssh://nathanbakhuijzen@vs-ssh.visualstudio.com:22/_ssh/scheduling-application" scheduling-application
cd scheduling-application
```

Run the project locally:
```bash
cd webapp

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

Vue frontend commands:
```bash
cd frontend

# Install npm dependencies
npm install

# Build the frontend resources
npm run build

# or run a live development server for the frontend
# make sure the dotnet core backend server is running
npm run dev
```

## Requirements
* dotnet core 2.0 or higher

## Other
Project
* https://nathanbakhuijzen.visualstudio.com/scheduling-application

Google Drive
* https://goo.gl/ryUWC4

Dropbox
* https://goo.gl/DxuPWG
