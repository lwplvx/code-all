# 查看mysql进程有两种方法

1) 进入mysql/bin目录下输入
    
        mysqladmin processlist;

2) 启动mysql，输入
    
        show processlist;

如果有SUPER权限，则可以看到全部的线程，否则，只能看到自己发起的线程（这是指，当前对应的MySQL帐户运行的线程）。

    mysql> show processlist; 
    
生成 kill 语句：
---

    SELECT * FROM information_schema.PROCESSLIST
    WHERE info IS NOT NULL
    ORDER BY TIME desc,state,INFO;

---

    SELECT CONCAT('kill ',id,';') AS KillSQL FROM information_schema.PROCESSLIST 
    WHERE 1=1 
    -- info LIKE '%from /*[Article_List]*/%' 
    AND DB='zhdj71-log'
    and time>10;
 