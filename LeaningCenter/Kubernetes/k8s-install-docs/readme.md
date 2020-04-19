# k8s 安装

1) 安装虚拟机
2) 安装k8s

    官方文档：
    
    <https://kubernetes.io/docs/setup/production-environment/tools/kubeadm/install-kubeadm/>

    关闭防火墙

        systemctl stop firewalld & systemctl disable firewalld

    关闭Swap

        sed -i '/ swap / s/^/#/' /etc/fstab

    添加阿里云的Docker仓库：
    
        yum-config-manager --add-repo http://mirrors.aliyun.com/docker-ce/linux/centos/docker-ce.repo 
        yum makecache

    安装docker  
    
    <https://kubernetes.io/docs/setup/production-environment/container-runtimes/#docker>

        # Install Docker CE
        ## Set up the repository
        ### Install required packages.
        yum install -y yum-utils device-mapper-persistent-data lvm2

        ### Add Docker repository.
        yum-config-manager --add-repo \
        https://download.docker.com/linux/centos/docker-ce.repo

        ## Install Docker CE.
        yum update -y && yum install -y \
        containerd.io-1.2.10 \
        docker-ce-19.03.4 \
        docker-ce-cli-19.03.4

        ## Create /etc/docker directory.
        mkdir /etc/docker

        # Setup daemon.
        cat > /etc/docker/daemon.json <<EOF
        {
        "exec-opts": ["native.cgroupdriver=systemd"],
        "log-driver": "json-file",
        "log-opts": {
            "max-size": "100m"
        },
        "storage-driver": "overlay2",
        "storage-opts": [
            "overlay2.override_kernel_check=true"
        ]
        }
        EOF

        mkdir -p /etc/systemd/system/docker.service.d

        # Restart Docker
        systemctl daemon-reload
        systemctl restart docker
    开机启动Docker

        systemctl enable docker
    
    配置K8S的yum源

    官方仓库无法使用，建议使用阿里源的仓库，执行以下命令添加kubernetes.repo仓库：

        cat <<EOF > /etc/yum.repos.d/kubernetes.repo
        [kubernetes]
        name=Kubernetes
        baseurl=http://mirrors.aliyun.com/kubernetes/yum/repos/kubernetes-el7-x86_64
        enabled=1
        gpgcheck=0
        repo_gpgcheck=0
        gpgkey=http://mirrors.aliyun.com/kubernetes/yum/doc/yum-key.gpg
            http://mirrors.aliyun.com/kubernetes/yum/doc/rpm-package-key.gpg
        EOF

    关闭SeLinux , 执行：

        setenforce 0
    Set SELinux in permissive mode (effectively disabling it)

        setenforce 0
        sed -i 's/^SELINUX=enforcing$/SELINUX=permissive/' /etc/selinux/config

        yum install -y kubelet kubeadm kubectl --disableexcludes=kubernetes

        systemctl enable --now kubelet

    Some users on RHEL/CentOS 7 have reported issues with traffic being routed incorrectly due to iptables being bypassed. You should ensure net.bridge.bridge-nf-call-iptables is set to 1 in your sysctl config, e.g.

        cat <<EOF > /etc/sysctl.d/k8s.conf
        net.bridge.bridge-nf-call-ip6tables = 1
        net.bridge.bridge-nf-call-iptables = 1
        EOF
        sysctl --system

    Make sure that the br_netfilter module is loaded before this step. This can be done by running 
    
        lsmod | grep br_netfilter
    To load it explicitly call 
    
        modprobe br_netfilter
    
    <https://kubernetes.io/docs/setup/production-environment/tools/kubeadm/install-kubeadm/#configure-cgroup-driver-used-by-kubelet-on-control-plane-node>

    查看该版本需要的容器镜像版本

            kubeadm config images list
    ---
            k8s.gcr.io/kube-apiserver:v1.17.3
            k8s.gcr.io/kube-controller-manager:v1.17.3
            k8s.gcr.io/kube-scheduler:v1.17.3
            k8s.gcr.io/kube-proxy:v1.17.3
            k8s.gcr.io/pause:3.1
            k8s.gcr.io/etcd:3.4.3-0
            k8s.gcr.io/coredns:1.6.5

    拉取k8s 镜像

        # 拉取镜像
        docker pull mirrorgcrio/kube-apiserver:v1.17.3
        docker pull mirrorgcrio/kube-controller-manager:v1.17.3
        docker pull mirrorgcrio/kube-scheduler:v1.17.3
        docker pull mirrorgcrio/kube-proxy:v1.17.3
        docker pull mirrorgcrio/pause:3.1
        docker pull mirrorgcrio/etcd:3.4.3-0
        docker pull mirrorgcrio/coredns:1.6.5
        # 重命名 tag
        docker tag mirrorgcrio/kube-apiserver:v1.17.3 k8s.gcr.io/kube-apiserver:v1.17.3
        docker tag mirrorgcrio/kube-controller-manager:v1.17.3 k8s.gcr.io/kube-controller-manager:v1.17.3
        docker tag mirrorgcrio/kube-scheduler:v1.17.3 k8s.gcr.io/kube-scheduler:v1.17.3
        docker tag mirrorgcrio/kube-proxy:v1.17.3 k8s.gcr.io/kube-proxy:v1.17.3
        docker tag mirrorgcrio/pause:3.1 k8s.gcr.io/pause:3.1
        docker tag mirrorgcrio/etcd:3.4.3-0 k8s.gcr.io/etcd:3.4.3-0
        docker tag mirrorgcrio/coredns:1.6.5 k8s.gcr.io/coredns:1.6.5
        # 删除多余的 tag
        docker rmi mirrorgcrio/kube-apiserver:v1.17.3
        docker rmi mirrorgcrio/kube-controller-manager:v1.17.3
        docker rmi mirrorgcrio/kube-scheduler:v1.17.3
        docker rmi mirrorgcrio/kube-proxy:v1.17.3
        docker rmi mirrorgcrio/pause:3.1
        docker rmi mirrorgcrio/etcd:3.4.3-0
        docker rmi mirrorgcrio/coredns:1.6.5


3) 复制虚拟机

每个虚拟机添加 hostonly 网络

修改新虚拟机的hostname

    hostnamectl set-hostname k8s-node2

4) 启动k8s

创建集群
在Master主节点（k8s-node1）上执行:

    kubeadm init --pod-network-cidr=192.168.0.0/16 --kubernetes-version=v1.17.3 --apiserver-advertise-address=192.168.56.101
---
    kubeadm join 192.168.56.101:6443 --token 54g0ph.m8gyat3m99ypd5j2 \
    --discovery-token-ca-cert-hash sha256:6f420a8deaf51241168499495ce6b55ca7b361318f914bfe0b2232e84204bc89
---
    mkdir -p $HOME/.kube
    sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
    sudo chown $(id -u):$(id -g) $HOME/.kube/config

运行一下命令 可以看到kube-dns组件是阻塞状态

    kubectl get pod -n kube-system

创建网络

        docker load < calico#node_v3.13.0.tar
        docker load < calico#pod2daemon-flexvol_v3.13.0.tar
        docker load < calico#cni_v3.13.0.tar
        docker load < calico#kube-controllers_v3.13.0.tar

在主节点上，需要执行如下命令：

    curl https://docs.projectcalico.org/manifests/calico.yaml -O
    kubectl apply -f calico.yaml

---
    kubectl get nodes

5) 安装 k8s Web UI (Dashboard)

参考官网：<https://github.com/kubernetes/dashboard>

    kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.0.0-rc5/aio/deploy/recommended.yaml

查看  kubernetes-dashboard
    
    kubectl -n kubernetes-dashboard get svc

增加  type: NodePort ，效果如下

    ---

    kind: Service
    apiVersion: v1
    metadata:
    labels:
        k8s-app: kubernetes-dashboard
    name: kubernetes-dashboard
    namespace: kubernetes-dashboard
    spec:
    type: NodePort
    ports:
        - port: 443
        targetPort: 8443
    selector:
        k8s-app: kubernetes-dashboard

注释以下三行（kubernetes-dashboard deployment）：

    #tolerations:
    #- key: node-role.kubernetes.io/master
    #  effect: NoSchedule

增加了 nodeName: k8s-node1，效果如下

    ……
        spec:
        nodeName: k8s-node1   # 这个是增加的
        containers:
        - name: kubernetes-dashboard
            image: kubernetesui/dashboard:v2.0.0-rc5
            imagePullPolicy: Always
            ports:
            - containerPort: 8443
                protocol: TCP
    ……

    kubectl replace --force -f kubernetesui.yaml

创建sa及绑定为集群管理员

    kubectl create serviceaccount dashboard-admin -n kube-system
    kubectl create clusterrolebinding dashboard-cluster-admin --clusterrole=cluster-admin --serviceaccount=kube-system:dashboard-admin
---
    kubectl get secret -n kube-system
    # 找到新新创建的token
    dashboard-admin-token-cg8cr                      kubernetes.io/service-account-token   3      80s

获取认证token

    kubectl describe sa dashboard-admin  -n kube-system
    # 效果类似下面
    ……
    Tokens:              dashboard-admin-token-cg8cr
    ……
    # 运行命令获取token
    kubectl describe secret dashboard-admin-token-cg8cr -n kube-system
    ……
    token:      eyJhbGciOiJSUzI1NiIsImtpZCI6IkYyRzkzV0xPWW9vSVktYjFoWk40ODBhVUM0akE2bXczR1BHNzhVVWhGc1UifQ.eyJpc3MiOiJrdWJlcm5ldGVzL3NlcnZpY2VhY2NvdW50Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9uYW1lc3BhY2UiOiJrdWJlLXN5c3RlbSIsImt1YmVybmV0ZXMuaW8vc2VydmljZWFjY291bnQvc2VjcmV0Lm5hbWUiOiJkYXNoYm9hcmQtYWRtaW4tdG9rZW4tY2c4Y3IiLCJrdWJlcm5ldGVzLmlvL3NlcnZpY2VhY2NvdW50L3NlcnZpY2UtYWNjb3VudC5uYW1lIjoiZGFzaGJvYXJkLWFkbWluIiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9zZXJ2aWNlLWFjY291bnQudWlkIjoiYjhiMjM3OWItMDUyYi00YmYyLTg2MTktMzVlNWJlODljYmQwIiwic3ViIjoic3lzdGVtOnNlcnZpY2VhY2NvdW50Omt1YmUtc3lzdGVtOmRhc2hib2FyZC1hZG1pbiJ9.b0HZc3kRb_tCpYQpX9SdmhdcxT7XQnTMIykhYs_GdzrCUlMZRNFb1p1g4h5wbyjA10UGkmmCC3fDD2imskBSy9WijdUa4P9PXNtMROVNTqTD9WOAcySHqcAQHynW7_LhyCKbnbS6E-rsOug9lloq9LRwI2gJE7lPw_drnEzfuMmMSOcCzRrb0m8rNDr6Y3Du_BOwXyBZ0r5zoQwAXu6YPDk_E0KskM_EfWZE4QjQVCnP7UJPj5ltxu3HWznaS9rcqTnTbUIkLQeC50-B8XhiL76kktu3K_BeJiF_k_9TQzk2kGd7MunHjikxYKLrE6nwW_n3r46RCstcgApnefnLxw
    ……
此token即为具有clusteradmin权限的token，可用此来登录dashboard。

6) 安装kubernetes-Ingress
7) k8s 的日志，ELK？