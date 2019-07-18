# Redis Docker 安装

1) 查找 Docker Hub 上的redis镜像
    
        docker search  redis

2) 拉取官方的镜像

        docker pull  redis

3) 等待下载完成后，就可以在本地镜像列表里查到 REPOSITORY 为redis,标签为 latest 的镜像。

        docker images redis

4) 使用redis镜像

        docker run -p 6379:6379 -v $PWD/data:/data  -d redis:3.2 redis-server --appendonly yes

    ---
    命令说明：

    -p 6379:6379 : 将容器的6379端口映射到主机的6379端口
    -v $PWD/data:/data : 将主机中当前目录下的data挂载到容器的/data
    redis-server --appendonly yes : 在容器执行redis-server启动命令，并打开redis持久化配置

5) 查看容器启动情况

        docker ps

6) 连接、查看容器

    使用redis镜像执行redis-cli命令连接到刚启动的容器,主机IP为172.17.0.1

        docker exec -it de27350fee10 redis-cli
        
        127.0.0.1:6379> info
        # Server
        redis_version:5.0.5
        redis_git_sha1:00000000
        redis_git_dirty:0
        redis_build_id:7983a619928f1f2d
        redis_mode:standalone
        os:Linux 4.9.125-linuxkit x86_64
        arch_bits:64
        multiplexing_api:epoll
        atomicvar_api:atomic-builtin
        gcc_version:6.3.0
        process_id:1
        run_id:d3bdd3e42d640ce2a0117b7e7efbc619fad6f120
        tcp_port:6379
        uptime_in_seconds:363
        ... ...


参考:
* [https://www.runoob.com/docker/docker-install-redis.html](https://www.runoob.com/docker/docker-install-redis.html)
