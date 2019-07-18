# Kafka Docker 安装

参考：
* [https://www.jianshu.com/p/263164fdcac7](https://www.jianshu.com/p/263164fdcac7)
* [https://blog.csdn.net/designer01/article/details/81604774](https://blog.csdn.net/designer01/article/details/81604774)


下载docker镜像
zookeeker: 
    
    sudo docker pull wurstmeister/zookeeper:latest 
kafka: 
    
    sudo docker pull wurstmeister/kafka:latest


## 先启动zookeeper:

    sudo docker run -d --name zookeeper --publish 2181:2181 --volume /etc/localtime:/etc/localtime wurstmeister/zookeeper:latest
---
    sudo docker run -d --name zookeeper --publish 2181:2181 wurstmeister/zookeeper:latest
---
 sudo docker run -d --name zookeeper-sit-dev --publish 2181:2181 --volume /etc/localtime:/etc/localtime wurstmeister/zookeeper:latest

## zookeeper启动完成后再启动kafka:

    sudo docker run -d --name kafka --publish 9092:9092 \
    --link zookeeper \
    --env KAFKA_ZOOKEEPER_CONNECT=zookeeper:2181 \
    --env KAFKA_ADVERTISED_HOST_NAME=kafka所在宿主机的IP \
    --env KAFKA_ADVERTISED_PORT=9092 \
    --volume /etc/localtime:/etc/localtime \
    wurstmeister/kafka:latest
---
    sudo docker run -d --name kafka-sit-dev --publish 9092:9092 \
    --link zookeeper-sit-dev \
    --env KAFKA_ZOOKEEPER_CONNECT=zookeeper-sit-dev:2181 \
    --env KAFKA_ADVERTISED_HOST_NAME=zhdjkafka.chinaeast2.cloudapp.chinacloudapi.cn \
    --env KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://zhdjkafka.chinaeast2.cloudapp.chinacloudapi.cn:9092 \
    --env KAFKA_ADVERTISED_PORT=9092 \
    --volume /etc/localtime:/etc/localtime \
    wurstmeister/kafka:latest

    # 注意上面的IP zhdjkafka.chinaeast2.cloudapp.chinacloudapi.cn ，对应的ip改为自己所想提供kafka服务的宿主机ip。
---
    sudo docker run -d --name kafka --publish 9092:9092 \
    --link zookeeper \
    --env KAFKA_ZOOKEEPER_CONNECT=zookeeper:2181 \
    --env KAFKA_ADVERTISED_HOST_NAME=10.211.55.2 \
    --env KAFKA_ADVERTISED_PORT=9092 \
    wurstmeister/kafka:latest
 
 * 进入到kafka容器中:
    
        sudo docker exec -it d475034723b3 bash
    ---
        # 进入到/opt/kafka_2.12-2.2.0/bin
        
        cd /opt/kafka_2.12-2.2.0/bin

 * 在zk上创建一个主题（指定分区数，副本数），所有的信息都是存放在zk上: 
        
        ./kafka-topics.sh --create --zookeeper   zookeeper:2181 --replication-factor 1 --partitions 1 --topic drTest
    ---
        # zidj sit-dev 环境
        ./kafka-topics.sh --create --zookeeper   zookeeper-sit-dev:2181 --replication-factor 1 --partitions 1 --topic zhdjTest

 * 查看在关联的zk上有多少主题：
        
        kafka-topics.sh --list --zookeeper zookeeper:2181
    ---
        # zidj sit-dev 环境
        kafka-topics.sh --list --zookeeper zookeeper-sit-dev:2181

* 创建一个生产者，明确生产者发送消息的broker，和消息的主题：
    
        kafka-console-producer.sh --broker-list localhost:9092 --topic drTest
    ---
        # zidj sit-dev 环境
        kafka-console-producer.sh --broker-list localhost:9092 --topic zhdjTest

* 创建一个消费者。这里有点疑惑，有的文档说bootstrap-server后面加zk的地址，但是经过我的测试，需要添加broker的地址 

        kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic drTest --from-beginning
    ---
        # zidj sit-dev 环境
        kafka-console-consumer.sh --bootstrap-server localhost:9092 --topic zhdjTest --from-beginning