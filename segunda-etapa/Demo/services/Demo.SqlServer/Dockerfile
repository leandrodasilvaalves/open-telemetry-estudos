FROM mcr.microsoft.com/mssql/server:2017-CU20-ubuntu-16.04

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY . /usr/src/app

RUN chmod +x /usr/src/app/run-initialization.sh

ENV SA_PASSWORD 123@Mudar
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express
ENV SLEEP_TIME 90

EXPOSE 1433

CMD /bin/bash ./entrypoint.sh