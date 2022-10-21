#!/bin/bash

docker-compose stop collector
docker-compose rm -f collector
docker-compose up -d
docker-compose logs -f --tail="all" collector