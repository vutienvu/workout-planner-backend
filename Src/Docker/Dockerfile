FROM --platform=linux/amd64 mysql:latest

COPY mysql.cnf /etc/mysql/conf.d/mysql.cnf
RUN chmod 0444 /etc/mysql/conf.d/mysql.cnf

CMD ["mysqld"]