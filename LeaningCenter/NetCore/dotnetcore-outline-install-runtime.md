# dotnetcore  centos 离线安装

* 下载对应版本的二进制包

* 解压到 /usr/dotnet/ 目录
多个版本的话 建立 dotnet 目录，然后里面存放 各个版本
        # 多个版本的话，多个版本一次解压
* 安装完.netcore后我们需要配置一个快捷方式，也可以配置环境变量，否则CentOS不认识dotnet命令

        sudo ln -s /usr/dotnet/dotnet /usr/local/bin/

* [https://github.com/dotnet/core/blob/master/Documentation/build-and-install-rhel6-prerequisites.md](https://github.com/dotnet/core/blob/master/Documentation/build-and-install-rhel6-prerequisites.md)