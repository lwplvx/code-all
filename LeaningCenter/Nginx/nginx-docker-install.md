# Ngnix Docker 安装

参考：[https://www.runoob.com/docker/docker-install-nginx.html](https://www.runoob.com/docker/docker-install-nginx.html)


查找Docker Hub上的nginx镜像

    docker search nginx
    
    NAME                      DESCRIPTION                                     STARS     OFFICIAL   AUTOMATED
    nginx                     Official build of Nginx.                        3260      [OK]       
    jwilder/nginx-proxy       Automated Nginx reverse proxy for docker c...   674                  [OK]
    richarvey/nginx-php-fpm   Container running Nginx + PHP-FPM capable ...   207                  [OK]
    million12/nginx-php       Nginx + PHP-FPM 5.5, 5.6, 7.0 (NG), CentOS...   67                   [OK]
    maxexcloo/nginx-php       Docker framework container with Nginx and ...   57                   [OK]
    webdevops/php-nginx       Nginx with PHP-FPM                              39                   [OK]
    h3nrik/nginx-ldap         NGINX web server with LDAP/AD, SSL and pro...   27                   [OK]
    bitnami/nginx             Bitnami nginx Docker Image                      19                   [OK]
    maxexcloo/nginx           Docker framework container with Nginx inst...   7                    [OK]
    ...
这里我们拉取官方的镜像

    docker pull nginx
等待下载完成后，我们就可以在本地镜像列表里查到REPOSITORY为nginx的镜像。

## 使用nginx镜像
运行容器
    docker run -p 8081:80 --name mynginx -d nginx

## 然后
首先，创建目录 nginx, 用于存放后面的相关东西。
    mkdir -p ~/nginx/www ~/nginx/logs ~/nginx/conf ~/nginx/conf.d
拷贝容器内 Nginx 默认配置文件到本地当前目录下的 conf 目录，容器 ID 可以查看 docker ps 命令输入中的第一列：
    docker cp 2aa65c091c2b:/etc/nginx/nginx.conf ~/nginx/conf ~/nginx/conf.d

* www: 目录将映射为 nginx 容器配置的虚拟目录。
* logs: 目录将映射为 nginx 容器的日志目录。
* conf: 目录里的配置文件将映射为 nginx 容器的配置文件。
* conf.d: 目录里的配置文件将映射为 nginx 容器的配置文件。

# 部署命令

    docker run -d -p 8082:80 --name my-nginx -v ~/nginx/www:/usr/share/nginx/html -v ~/nginx/conf/nginx.conf:/etc/nginx/nginx.conf -v ~/nginx/logs:/var/log/nginx nginx

    docker run -d -p 80:80 --name nginx-80 -v ~/nginx/www:/usr/share/nginx/html -v ~/nginx/conf/nginx.conf:/etc/nginx/nginx.conf -v ~/nginx/conf.d:/etc/nginx/conf.d -v ~/nginx/logs:/var/log/nginx nginx

## 命令说明：

* -p 8082:80： 将容器的 80 端口映射到主机的 8082 端口。

* --name nginx-test-web：将容器命名为 nginx-test-web。

* ~/nginx/www:/usr/share/nginx/html：将我们自己创建的 www 目录挂载到容器的 /usr/share/nginx/html。

* -v ~/nginx/conf/nginx.conf:/etc/nginx/nginx.conf：将我们自己创建的 nginx.conf 挂载到容器的 /etc/nginx/nginx.conf。

* -v ~/nginx/logs:/var/log/nginx：将我们自己创建的 logs 挂载到容器的 /var/log/nginx。

## 启动以上命令后进入 ~/nginx/www 目录：
    cd ~/nginx/www


