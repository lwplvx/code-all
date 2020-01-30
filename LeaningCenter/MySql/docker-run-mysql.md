# Docker 安装 Mysql
docker pull mysql:5.7

# Docker Mysql 启动

sudo docker run --name pwc-mysql -e MYSQL_ROOT_PASSWORD=Cloud@1234 -p 3307:3306 -d mysql

docker run --name some-mysql -e MYSQL_ROOT_PASSWORD=my-secret-pw -d mysql:tag --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci

docker run --name sit-mysql -e MYSQL_ROOT_PASSWORD=Cloud@1234 -p 3307:3306  -d mysql:5.7.25 --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci --lower_case_table_names=1


docker run --name uat-mysql -e MYSQL_ROOT_PASSWORD=Cloud@1234 -p 3306:3306  -d mysql:5.7.25 --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci --lower_case_table_names=1


sudo docker run -p 3306:3306 --name sit-5.7 -v /usr/local/mysql/my.cnf:/etc/mysql/my.cnf -v /usr/local/mysql/logs:/logs -v /Users/luweiping/mysql/data:/mysql_data -e MYSQL_ROOT_PASSWORD=Cloud@1234 -d mysql:5.7.25


sudo docker run -p 3308:3306 --name mysql-latest -v /Users/luweiping/mysql-latest/my.cnf:/etc/mysql/my.cnf -v /Users/luweiping/mysql-latest/logs:/logs -v /Users/luweiping/mysql-latest/data:/mysql_data -e MYSQL_ROOT_PASSWORD=Cloud@1234 -d mysql:latest
