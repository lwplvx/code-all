# CMD

* [如何在Windows系统下安装多个Redis实例](https://jingyan.baidu.com/article/ab0b56309448b9c15afa7d3a.html) 

C:\Users\Gray>E:\redis-2.8.17\redis-server.exe --service-install E:\redis-2.8.17\redis6369.conf --service-name RedisServer6369 --port 6369

# 端口占用
    netstat -ano
    netstat -ano|findstr "49157"
    tasklist|findstr "2720"
    taskkill /f /t /im Tencentdl.exe。