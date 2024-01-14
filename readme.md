# DocuWare Event Registration application

## Git

.gitignore is created using `dotnet new gitignore` command

## Database Control

First create a copy of `docker/.env.example` and save it in `docker/.env` address after modifying environment values.

### To bring database container UP

```bash
cd docker
docker compose --profile db --env-file .env up -d
```

### To bring database container DOWN

```bash
cd docker
docker compose --profile db --env-file .env down
```

Note: This command will not remove the data

### To remove database container and its data

```bash
cd docker
docker compose --profile db --env-file .env down
docker volumn rm docuware-pg
```
Note: This is useful in case you want to change database user and password