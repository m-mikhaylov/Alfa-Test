FROM yandex/clickhouse-server

COPY init.sql /docker-entrypoint-initdb.d/init.sql
COPY users.xml /etc/clickhouse-server/users.xml

RUN apt-get install -y clickhouse-client
