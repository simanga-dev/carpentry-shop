# Carpentry Shop development commands

# List available commands
default:
    @just --list

# Run the app in development mode
run:
    dotnet run

# Run with hot reload
watch:
    dotnet watch run

# Build the project
build:
    dotnet build

# Restore NuGet packages
restore:
    dotnet restore

# Create a new EF migration
migrate name:
    dotnet ef migrations add {{name}}

# Apply pending migrations to the database
db-update:
    dotnet ef database update

# Rollback to a specific migration
db-rollback migration:
    dotnet ef database update {{migration}}

# List applied migrations
db-status:
    dotnet ef migrations list

# Remove the last migration (if not applied)
migrate-remove:
    dotnet ef migrations remove

# Build the Docker image
docker-build:
    docker compose build

# Run with Docker Compose
docker-up:
    docker compose up

# Stop Docker Compose
docker-down:
    docker compose down

# Clean build artifacts
clean:
    dotnet clean
    rm -rf bin/ obj/
