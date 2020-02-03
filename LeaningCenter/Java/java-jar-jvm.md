# 启动jar包，内存参数设置

    nohup java -jar -Xms512M -Xmx2048M -XX:PermSize=512M -XX:MaxPermSize=1024M  project.jar
## 
## 说明：
* Xms:堆内存初始大小
* Xmx:堆内存最大值
* PermSize:永久内存初始大小
* MaxPermSize：永久内存最大值
* 堆内存和永久内存区别以及其他参数设置，参考jvm运行机制
## 参数的含义
* -Xms128m JVM初始分配的堆内存
* -Xmx512m JVM最大允许分配的堆内存，按需分配
* -XX:PermSize=64M JVM初始分配的非堆内存
* -XX:MaxPermSize=128M JVM最大允许分配的非堆内存，按需分配
