docker run --rm \
    --name postgres \
    --publish 5432:5432 \
    --env POSTGRES_PASSWORD=mysecretpassword \
    --platform linux/amd64 \
    postgres:15.1-alpine