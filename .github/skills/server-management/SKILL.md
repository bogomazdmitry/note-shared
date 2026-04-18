---
name: server-management
description: 'Manage NoteShared production server. Use when: checking server status, viewing logs, debugging production issues, restarting services, managing database, server maintenance, nginx config, SSL certificates, backups, monitoring.'
---

# Server Management

## Server Info

| Property | Value |
|----------|-------|
| Host | `173.212.212.36` |
| Domain | `note.taskcal.online` |
| User | `root` |
| OS | Ubuntu 24.04 LTS |
| Project dir | `/opt/note-shared` |

## SSH Connection

**Ask the user for the password before connecting.**

```bash
SSHPASS='<password>' sshpass -e ssh \
  -o StrictHostKeyChecking=no \
  -o PubkeyAuthentication=no \
  root@173.212.212.36 'COMMAND'
```

## Container Management

```bash
# Status
docker compose -f /opt/note-shared/docker-compose.yml ps

# Logs
docker logs noteshared-api --tail 50
docker logs noteshared-web --tail 50
docker logs noteshared-postgres --tail 50

# Restart
cd /opt/note-shared && docker compose restart api
cd /opt/note-shared && docker compose restart web
cd /opt/note-shared && docker compose restart postgres

# Stop all
cd /opt/note-shared && docker compose down

# Start all
cd /opt/note-shared && docker compose up -d
```

## Database Access

```bash
# Connect to PostgreSQL
docker exec -it noteshared-postgres psql -U notes -d note_shared

# List tables
docker exec noteshared-postgres psql -U notes -d note_shared -c '\dt'

# Run SQL query
docker exec noteshared-postgres psql -U notes -d note_shared -c 'SELECT COUNT(*) FROM "AspNetUsers";'
```

## Nginx

```bash
# Config location
/etc/nginx/sites-available/note.taskcal.online

# Test config
nginx -t

# Reload
systemctl reload nginx

# View config
cat /etc/nginx/sites-available/note.taskcal.online
```

## SSL / TLS

```bash
# Certificate info
certbot certificates

# Manual renewal
certbot renew

# Auto-renewal timer
systemctl list-timers | grep certbot
```

## Ports

| Service | Container Port | Host Port |
|---------|---------------|-----------|
| Angular web | 80 | 127.0.0.1:8088 |
| .NET API | 5000 | 127.0.0.1:15000 |
| PostgreSQL | 5432 | 0.0.0.0:55433 |

## Environment

`.env` at `/opt/note-shared/.env` (chmod 600)
