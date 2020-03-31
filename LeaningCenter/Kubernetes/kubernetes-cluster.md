# 从零开始搭建Kubernetes集群(转载)

1) 参考 [从零开始搭建Kubernetes集群(一、开篇)](https://www.jianshu.com/p/78a5afd0c597)

2) 参考 [从零开始搭建Kubernetes集群(二、搭建虚拟机环境)](https://www.jianshu.com/p/ad3c712e1d95)

3) 参考 [从零开始搭建Kubernetes集群(三、搭建K8S集群)](https://www.jianshu.com/p/e43f5e848da1)

* 这一步遇到了问题
    
cat  /usr/lib/systemd/system/kubelet.service.d/10-kubeadm.conf

    * 创建集群
    
    在Master主节点（k8s-node1）上执行:

    kubeadm init --pod-network-cidr=192.168.0.0/16 --kubernetes-version=v1.10.0 --apiserver-advertise-address=192.168.56.101

    遇到如下：
    this version of kubeadm only supports deploying clusters with the control plane version >= 1.15.0. Current version: v1.10.0
To see the stack trace of this error execute with --v=5 or higher


 

然后执行：
kubeadm init --pod-network-cidr=192.168.0.0/16 --kubernetes-version=v1.16.2 --apiserver-advertise-address=192.168.99.136
遇到错误 ：

执行 kubeadm reset

修改
/proc/sys/net/bridge/bridge-nf-call-iptables 的内容 为 1

参考 https://www.cnblogs.com/ericnie/p/7749588.html

images=(etcd-amd64:3.0.17 pause-amd64:3.0 kube-proxy-amd64:v1.16.2 kube-scheduler-amd64:v1.16.2 kube-controller-manager-amd64:v1.16.2 kube-apiserver-amd64:v1.16.2 kubernetes-dashboard-amd64:v1.10.1 k8s-dns-sidecar-amd64:1.14.4 k8s-dns-kube-dns-amd64:1.14.4 k8s-dns-dnsmasq-nanny-amd64:1.14.4)
for imageName in ${images[@]} ; do
  docker pull cloudnil/$imageName
  docker tag cloudnil/$imageName gcr.io/google_containers/$imageName
  docker rmi cloudnil/$imageName
done

kubeadm init --kubernetes-version=v1.16.2 --pod-network-cidr=10.244.0.0/16 --apiserver-advertise-address=0.0.0.0 --apiserver-cert-extra-sans=192.168.122.1,192.168.122.2,192.168.122.3,127.0.0.1,k8s-1,k8s-2,k8s-3,192.168.0.1 --skip-preflight-checks


4) 参考 [从零开始搭建Kubernetes集群(四、搭建K8S Dashboard)](https://www.jianshu.com/p/6f42ac331d8a)

5) 参考 [从零开始搭建Kubernetes集群(五、搭建K8S Ingress)](https://www.jianshu.com/p/feeea0bbd73e)

6) 参考 [从零开始搭建Kubernetes集群(六、在K8S上部署Redis 集群)](https://www.jianshu.com/p/65c4baadf5d9)

7) 参考 [从零开始搭建Kubernetes集群(七、如何监控K8S集群日志)](https://www.jianshu.com/p/b264b6cf9340)
