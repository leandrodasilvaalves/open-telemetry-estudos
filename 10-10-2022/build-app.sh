#!/bin/bash

docker-compose stop entrypoint
docker-compose rm -f entrypoint
docker-compose stop servico-core-a
docker-compose rm -f servico-core-a
docker-compose stop servico-core-b
docker-compose rm -f servico-core-b
docker-compose stop collector
docker-compose rm -f collector
docker-compose build
docker-compose up -d
docker-compose logs -f --tail="all" collector