# DEMO

docker run -d --name demo-postgres -e POSTGRES_PASSWORD=Password123! -e PGDATA=/var/lib/postgresql/data/pgdata -v demo-postgres:/var/lib/postgresql/data postgres:13.12
