# Carpentry Shop development commands

# List available commands
default:
    @just --list

# Run the application
# With Aspire CLI installed: orchestrates PostgreSQL + Web App automatically
# Without Aspire CLI: runs main project directly (requires DB_CONNECTION env var or ConnectionStrings:carpentrydb in appsettings)
run:
    @if command -v aspire >/dev/null 2>&1; then \
        echo "✓ Aspire CLI found. Running with orchestration..."; \
        aspire run; \
    else \
        echo "! Aspire CLI not found. Running project directly..."; \
        echo "  To enable Aspire orchestration with PostgreSQL container, install the CLI:"; \
        echo "    curl -sSL https://aspire.dev/install.sh | bash"; \
        echo ""; \
        dotnet run --project CarpentryShop.csproj; \
    fi

# Run with hot reload (runs main project directly)
watch:
    dotnet watch run --project CarpentryShop.csproj

# Run using Aspire AppHost directly (requires Aspire CLI to be installed)
run-aspire:
    @echo "Note: This requires Aspire CLI to be installed."
    @echo "      Install with: curl -sSL https://aspire.dev/install.sh | bash"
    aspire run

# Build the entire solution (including Aspire projects)
build:
    dotnet build

# Build only the main web project
build-web:
    dotnet build CarpentryShop.csproj

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

# Create .env with generated secrets (safe to rerun)
env:
    ./deploy/init-env.sh

# Build the Docker image
docker-build: env
    docker compose build

# Run with Docker Compose
docker-up: env
    docker compose up

# Stop Docker Compose
docker-down: env
    docker compose down

# Clean build artifacts
clean:
    dotnet clean
    rm -rf bin/ obj/ CarpentryShop.AppHost/bin/ CarpentryShop.AppHost/obj/ CarpentryShop.ServiceDefaults/bin/ CarpentryShop.ServiceDefaults/obj/

# Install Aspire CLI
install-aspire:
    curl -sSL https://aspire.dev/install.sh | bash

# Check Aspire CLI status
aspire-status:
    @if command -v aspire >/dev/null 2>&1; then \
        echo "✓ Aspire CLI is installed:"; \
        aspire --version; \
    else \
        echo "! Aspire CLI is not installed."; \
        echo "  Install with: just install-aspire"; \
    fi
