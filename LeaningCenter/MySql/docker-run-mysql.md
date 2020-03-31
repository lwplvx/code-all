# Docker 安装 运行 Mysql

    docker pull mysql:5.7

# Docker Mysql 启动

    sudo docker run --name pwc-mysql -e MYSQL_ROOT_PASSWORD=123xxx  -p 3307:3306 -d mysql

    docker run --name some-mysql -e MYSQL_ROOT_PASSWORD=my-secret-pw -d mysql:tag --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci

    docker run --name sit-mysql -e MYSQL_ROOT_PASSWORD=123xxx -p 3307:3306  -d mysql:5.7.25 --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci --lower_case_table_names=1


    docker run --name uat-mysql -e MYSQL_ROOT_PASSWORD=123xxx -p 3306:3306  -d mysql:5.7.25 --character-set-server=utf8mb4 --collation-server=utf8mb4_unicode_ci --lower_case_table_names=1


    sudo docker run -p 3306:3306 --name sit-5.7 -v /usr/local/mysql/my.cnf:/etc/mysql/my.cnf -v /usr/local/mysql/logs:/logs -v /Users/youruserpath/mysql/data:/mysql_data -e MYSQL_ROOT_PASSWORD=123xxx -d mysql:5.7.25


    sudo docker run -p 3308:3306 --name mysql-latest -v /Users/youruserpath/mysql-latest/my.cnf:/etc/mysql/my.cnf -v /Users/youruserpath/mysql-latest/logs:/logs -v /Users/youruserpath/mysql-latest/data:/mysql_data -e MYSQL_ROOT_PASSWORD=123xxx -d mysql:latest
