# MySQL5.7 添加用户、删除用户与授权

参考 
* [https://www.cnblogs.com/xujishou/p/6306765.html](https://www.cnblogs.com/xujishou/p/6306765.html)
* [http://www.php.cn/mysql-tutorials-415708.html](http://www.php.cn/mysql-tutorials-415708.html)

CREATE USER 'username'@'host' IDENTIFIED BY 'password';
DROP USER 'username'@'host';


二.授权:
命令:GRANT privileges ON databasename.tablename TO 'username'@'host'

PS: privileges - 用户的操作权限,如SELECT , INSERT , UPDATE 等(详细列表见该文最后面).如果要授予所的权限则使用ALL.;databasename - 数据库名,tablename-表名,如果要授予该用户对所有数据库和表的相应操作权限则可用*表示, 如*.*.


1、在MySQL中创建新用户

使用具有shell访问权限的root用户登录MySQL服务器并创建名为“rahul”的新用户。下面的命令只允许从localhost系统访问用户rahul的MySQL服务器。


mysql> CREATE USER 'rahul'@'localhost' IDENTIFIED BY 'password';

现在将权限分配给特定数据库。下面的命令将允许用户rahul拥有数据库“mydb”的所有权限。


mysql> GRANT ALL ON mydb.* TO 'rahul'@'localhost';

创建用户并分配适当的权限后，请确保重新加载权限。


mysql> FLUSH PRIVILEGES;

