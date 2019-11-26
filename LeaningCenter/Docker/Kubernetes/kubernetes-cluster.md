# 从零开始搭建Kubernetes集群(转载)

1) 参考 [从零开始搭建Kubernetes集群(一、开篇)](https://www.jianshu.com/p/78a5afd0c597)

2) 参考 [从零开始搭建Kubernetes集群(二、搭建虚拟机环境)](https://www.jianshu.com/p/ad3c712e1d95)

3) 参考 [从零开始搭建Kubernetes集群(三、搭建K8S集群)](https://www.jianshu.com/p/e43f5e848da1)

* 这一步遇到了问题
    
    * 创建集群
    
    在Master主节点（k8s-node1）上执行:

    kubeadm init --pod-network-cidr=192.168.0.0/16 --kubernetes-version=v1.10.0 --apiserver-advertise-address=192.168.56.101

    遇到如下：
    this version of kubeadm only supports deploying clusters with the control plane version >= 1.15.0. Current version: v1.10.0
To see the stack trace of this error execute with --v=5 or higher


4) 参考 [从零开始搭建Kubernetes集群(四、搭建K8S Dashboard)](https://www.jianshu.com/p/6f42ac331d8a)

5) 参考 [从零开始搭建Kubernetes集群(五、搭建K8S Ingress)](https://www.jianshu.com/p/feeea0bbd73e)

6) 参考 [从零开始搭建Kubernetes集群(六、在K8S上部署Redis 集群)](https://www.jianshu.com/p/65c4baadf5d9)

7) 参考 [从零开始搭建Kubernetes集群(七、如何监控K8S集群日志)](https://www.jianshu.com/p/b264b6cf9340)
