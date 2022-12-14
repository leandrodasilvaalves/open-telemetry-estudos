#!/bin/bash

docker-compose stop payment
docker-compose stop catalog
docker-compose stop stock

docker-compose rm -f payment
docker-compose rm -f catalog
docker-compose rm -f stock

docker-compose build
docker-compose up -d 
docker-compose logs -f --tail=all payment catalog stock