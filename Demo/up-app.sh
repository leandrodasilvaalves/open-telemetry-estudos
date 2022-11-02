#!/bin/bash


docker-compose up -d \
entrypoint \
servico-core-a \
servico-core-a \
redis \
jaeger \
collector
docker-compose logs -f --tail="all" #collector