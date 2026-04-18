---
name: server-deploy
description: 'Deploy NoteShared to production server. Use when: deploying code, rebuilding containers, updating production, pushing changes to server, redeploy, shipping to prod, release.'
---

# Server Deploy

## Server Info

| Property | Value |
|----------|-------|
| Host | `173.212.212.36` |
| Domain | `note.taskcal.online` |
| User | `root` |
| OS | Ubuntu 24.04 LTS |
| Project dir | `/opt/note-shared` |

## SSH Connection

Password auth only. Use sshpass with `-e` flag (set `SSHPASS` env var) and disable pubkey:

```bash
SSHPASS='<password>' sshpass -e ssh \
  -o StrictHostKeyChecking=no \
  -o PubkeyAuthentication=no \
  root@173.212.212.36 'COMMAND'
```

For multi-line commands use heredoc:

```bash
SSHPASS='<password>' sshpass -e ssh \
  -o StrictHostKeyChecking=no \
  -o PubkeyAuthentication=no \
  root@173.212.212.36 'bash -s' << 'EOF'
# commands here
EOF
```

**Ask the user for the password before connecting.**

## Architecture on Server

```
Nginx (host, ports 80/443)
  ├── / → 127.0.0.1:8088 (Angular web client container)
  ├── /api/ → 127.0.0.1:15000 (.NET API server container)
  ├── /connect/ → 127.0.0.1:15000 (IdentityServer endpoints)
  └── /hub/ → 127.0.0.1:15000 (SignalR WebSockets)

Docker Compose (/opt/note-shared/docker-compose.yml):
  ├── postgres (16-alpine, port 55433 external)
  ├── api (.NET 5, 5000→15000)
  └── web (Angular + nginx, 80→8088)
```

## Tech Stack

- **Client**: Angular 11 + Angular Material + Bootstrap + SignalR
- **Server**: .NET 5 (ASP.NET Core) + Entity Framework Core + IdentityServer4
- **Database**: PostgreSQL 16
- **Real-time**: SignalR WebSockets at `/hub/note` and `/hub/notifications`

## TLS

- Certbot with Nginx plugin, auto-renewal enabled
- Cert: `/etc/letsencrypt/live/note.taskcal.online/fullchain.pem`
- Key: `/etc/letsencrypt/live/note.taskcal.online/privkey.pem`
- Nginx config: `/etc/nginx/sites-available/note.taskcal.online`
- Nginx config source: `deploy/nginx/note.taskcal.online.conf`

### Deploy nginx config

```bash
ssh ... 'cp /opt/note-shared/deploy/nginx/note.taskcal.online.conf /etc/nginx/sites-available/note.taskcal.online'
ssh ... 'ln -sf /etc/nginx/sites-available/note.taskcal.online /etc/nginx/sites-enabled/note.taskcal.online'
ssh ... 'nginx -t && systemctl reload nginx'
```

## Deploy Procedure

### Full deploy (both server and client)

1. Sync files:
```bash
SSHPASS='<password>' sshpass -e rsync -az \
  -e 'ssh -o StrictHostKeyChecking=no -o PubkeyAuthentication=no' \
  --exclude='.git' --exclude='.env' --exclude='node_modules' \
  --exclude='client/dist' --exclude='client/.angular' \
  --exclude='server/bin' --exclude='server/obj' \
  ./ root@173.212.212.36:/opt/note-shared/
```

2. Build and restart:
```bash
# Full rebuild
docker compose build --no-cache && docker compose up -d

# Or rebuild only changed service:
docker compose build api && docker compose up -d api
docker compose build web && docker compose up -d web
```

### Client-only deploy (Angular changes)

```bash
# Sync → rebuild client → restart
rsync ... && ssh ... 'cd /opt/note-shared && docker compose build web && docker compose up -d web'
```

### Server-only deploy (.NET changes)

```bash
# Sync → rebuild server → restart
rsync ... && ssh ... 'cd /opt/note-shared && docker compose build api && docker compose up -d api'
```

### Config-only change (.env update)

No rebuild needed, just restart:
```bash
ssh ... 'cd /opt/note-shared && docker compose restart api'
```

## Verification

```bash
# API check (should return 200 with empty body)
curl -s -o /dev/null -w "%{http_code}" https://note.taskcal.online/api/auth/check-unique-user-name?userName=test

# Frontend check
curl -s https://note.taskcal.online/ | head -5

# Container status
docker compose -f /opt/note-shared/docker-compose.yml ps

# Logs
docker logs noteshared-api --tail 20
docker logs noteshared-web --tail 20
docker logs noteshared-postgres --tail 20
```

## Production .env

Located at `/opt/note-shared/.env` (chmod 600). Contains:
- `POSTGRES_USER` / `POSTGRES_PASSWORD`
- `ASPNETCORE_ENVIRONMENT` (Development for EnsureCreated, Production for Migrate)
- `CLIENT_URL` / `SERVER_URL` — https://note.taskcal.online
- `API_HTTP_PORT=15000`, `WEB_PORT=8088`

## Database Notes

- EF Core with IdentityServer4 (ASP.NET Identity tables: AspNetUsers, AspNetRoles, etc.)
- App tables: Notes, NoteDesigns, NoteHistories, NoteText, Notifications
- **No EF migrations exist** — schema created via `EnsureCreated()` (Development env)
- If DB is empty, set `ASPNETCORE_ENVIRONMENT=Development` for first run, then switch back

## Troubleshooting

| Problem | Solution |
|---------|----------|
| `relation "AspNetUsers" does not exist` | Drop `__EFMigrationsHistory` if it exists, set env to Development, restart api |
| API 500 error | `docker logs noteshared-api` — check connection string |
| Nginx 502 | Check containers: `docker compose ps` |
| TLS cert expired | `certbot renew` (should auto-renew) |
| Disk full | `docker system prune -a` to clean old images |
| SignalR not connecting | Check `/hub/` proxy config with WebSocket upgrade headers |
