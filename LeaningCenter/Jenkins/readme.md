# Docker 安装 Jenkins

参考 [https://www.jianshu.com/p/0391e225e4a6](https://www.jianshu.com/p/0391e225e4a6)

## 输入命令，下载Jenkins官方镜像到服务器上。
    docker pull jenkins/jenkins

## 耐心等待下载完成，输入命令查看下载完成的镜像
    docker images

镜像下载完成，下面就要开始启动容器了。启动容器前，建议大家仔细阅读前面寻找镜像时Docker Hub上关于jenkins镜像的详细说明。
在镜像文档里，我们知道Jenkins访问的端口号是8080，另外还需要暴露一个tcp的端口号50000。我们使用如下命令启动Jenkins镜像。

    docker run -d -p 80:8080 -p 50000:50000 -v jenkins:/var/jenkins_home -v /etc/localtime:/etc/localtime --name jenkins docker.io/jenkins/jenkins

发现 带上 -v /etc/localtime:/etc/localtime 这一句 出错了，先去掉
    
    docker run -d -p 80:8080 -p 50000:50000 -v jenkins:/var/jenkins_home  --name jenkins docker.io/jenkins/jenkins

这里逐条解释下各参数的意义。

* -d 后台运行镜像

* -p 80:8080  将镜像的8080端口映射到服务器的80端口

* -p 50000:50000  将镜像的50000端口映射到服务器的50000端口

* -v jenkins:/var/jenkins_home  /var/jenkins_home目录为jenkins工作目录，我们将硬盘上的一个目录挂载到这个位置，方便后续更新镜像后继续使用原来的工作目录。

* -v /etc/localtime:/etc/localtime  让容器使用和服务器同样的时间设置。

* --name jenkins 给容器起一个别名

## 启动后输入命令docker ps -a查看所有容器，可以看到jenkins已成功启动。
    docker ps -a

## 配置Jenkins
在浏览器输入http://ip进入Jenkins登录页面。页面会提示你到服务器的指定位置获取初始化密码。