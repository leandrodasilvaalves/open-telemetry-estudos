#!/bin/bash

docker-compose stop entrypoint
docker-compose rm -f entrypoint
docker-compose stop servico-core-a
docker-compose rm -f servico-core-a
docker-compose build
docker-compose up -d
docker-compose logs -f --tail="all" collector