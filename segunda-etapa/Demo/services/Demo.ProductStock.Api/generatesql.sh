#!/bin/bash
 dotnet ef migrations script --project ./Demo.ProductStock.Api.csproj  --output ./db/tabelas.sql 