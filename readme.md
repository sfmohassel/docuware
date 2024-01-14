# DocuWare Event Registration application

## Git

.gitignore is created using `dotnet new gitignore` command

## Development Setup

1. Create a copy of `docker/.env.example` and save it in `docker/.env` address after modifying environment values. **DO NOT PUSH `.env` file into source repository.**

2. Create a copy of `src/Host/appsettings.Development.json` and after modifying values, save it in `src/Host/appsettings.Local.json`. **DO NOT PUSH this file into source repository.**

## Database Control


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

## How to do manual testing?

Run app locally, then open 'http://localhost:5051/swagger'. In that page locate the Open API json document link and copy it.
Import API as a collection in Postman using this link

## Ways to improve

- Use AutoMapper for mapping entities
- Add e2e tests for REST API
- Add a UI 😀
- Test with real database using TestContainers

