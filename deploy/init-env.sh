#!/usr/bin/env bash
# Create the .env file required by docker-compose.yml.
# Safe to run repeatedly: an existing .env is validated, never overwritten.
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
env_file="$repo_root/.env"
required_keys=(APP_DOMAIN POSTGRES_PASSWORD ADMIN_EMAIL ADMIN_PASSWORD)

read_value() {
    local key="$1"
    # Last assignment wins, matching Compose. Strip surrounding quotes.
    sed -n "s/^${key}=//p" "$env_file" | tail -n 1 |
        sed -e 's/^"\(.*\)"$/\1/' -e "s/^'\(.*\)'$/\1/"
}

if [[ -f "$env_file" ]]; then
    missing=()
    for key in "${required_keys[@]}"; do
        if [[ -z "$(read_value "$key")" ]]; then
            missing+=("$key")
        fi
    done
    if ((${#missing[@]} > 0)); then
        echo "error: $env_file is missing a value for: ${missing[*]}" >&2
        echo "Fill those in, or delete the file and rerun this script to generate secrets." >&2
        exit 1
    fi
    chmod 600 "$env_file"
    echo "Using existing $env_file"
    exit 0
fi

domain="${APP_DOMAIN:-}"
if [[ -z "$domain" && -t 0 ]]; then
    read -r -p "Public domain [localhost]: " domain
fi
domain="${domain:-localhost}"

admin_email="${ADMIN_EMAIL:-admin@${domain}}"

# ASP.NET Core Identity requires upper, lower, digit, and symbol characters.
# The literal prefix satisfies those rules; the random suffix provides entropy.
admin_password="Admin-$(openssl rand -hex 16)"

umask 077
cat >"$env_file" <<EOF
APP_DOMAIN=$domain
APP_TAG=latest
POSTGRES_PASSWORD=$(openssl rand -hex 32)
ADMIN_EMAIL=$admin_email
ADMIN_PASSWORD=$admin_password
EOF
chmod 600 "$env_file"

echo "Created $env_file"
echo "  APP_DOMAIN=$domain"
echo "  ADMIN_EMAIL=$admin_email"
echo "  ADMIN_PASSWORD=$admin_password"
echo
echo "For a public deployment, set APP_DOMAIN and ADMIN_EMAIL to real values"
echo "and point DNS at the host running your proxy."
