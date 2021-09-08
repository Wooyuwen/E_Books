## 1 分布式 

> 垂直伸缩
>
> 水平伸缩



***



##  2 缓存架构

>`通读缓存`：数据存在通读缓存就直接返回，否则访问数据源，同时将数据放于缓存中。
>
>常见类型：`CDN`和`反向代理`   
>
>> > `反向代理`:A proxy server is a go‑between or intermediary server that forwards requests for content from multiple clients to different servers across the Internet. A **reverse proxy server** is a type of proxy server that typically sits behind the firewall in a private network and directs client requests to the appropriate `backend server`. A reverse proxy provides **an additional level of abstraction and control** to ensure the smooth flow of network traffic between clients and servers.
>> >
>> > `Commen uses for reverse proxy server`:
>> >
>> > > - Load balancing 
>> > >
>> > > - Web acceleration
>> > >
>> > > - Security and anonymity(匿名)
>> >
>> > `反向代理`：反向代理“代理”的是服务器端，对客户端透明。
>
>`旁路缓存`:应用程序需要自己从数据源读取数据，然后将其写入旁路缓存中。下次需要数据可直接访问旁路缓存。
>
>缓存缺点：
>
>> `过期失效`：标记失效时间；
>>
>> `失效通知`：应用程序在更新数据源的时候，通知清楚缓存中的数据；
>>
>> 缓存适合带有热点的数据，如热卖商品，微博热点，易被多次读取。



***



## 3 异步架构

> 使用`消息队列`的异步架构：事件驱动模型。



***



## 4 负载均衡

> `HTTP重定向负载均衡`：`负载均衡服务器`使用一套负载均衡算法计算到后端服务器的地址，返回给用户浏览器，浏览器接到重定向响应后发送请求到新的应用服务器从而实现负载均衡。
>
>> 缺点：加大请求的工作量/因为要先计算到应用服务器的`IP`地址，所以`IP`地址可能暴露在公网
>
>`DNS`负载均衡：
>
>`反向代理负载均衡`:典型的为`Nginx`提供的反向代理和负载均衡功能。
>
>`IP负载均衡`：负载均衡服务器对数据包中的`IP`地址进行转换，从而发送给应用服务器。
>
>`链路负载均衡`：负载均衡服务器对数据包中的`MAC`地址进行修改，应用服务器和负载均衡服务器使用相同的虚拟`IP`。



***



## 5 数据存储  Q:如何改善数据的存储???

>`数据主从复制`: `binlog`复制命令至其他数据库日志(Relay Log)  **但是不能改善存储能力**
>
>→ `数据库分片`:表分片，每片存不同服务器中。常用`HashMap`



***



## 6 搜索引擎

> `URL` --`HTTP下载/解析超链接`--`倒排索引`<单词，地址>



***



## 7 微服务

> 单体架构心在哪功能麻烦，用户增多易出现耗尽连接

> > - 大应用拆分为小模块
> >
> > - 小模块不属于集群中
> >
> > - 通过远程调用的方式依赖各个独立的模块完成业务的处理
>
> 服务提供者：微服务的具体提供者，其他通过接口调用即可；
>
> 服务消费者：按照提供者接口编程即可。



***



***



## 9 HTTP 攻击与防护

> `SQL注入`:攻击者在请求参数的时候，包含了恶意的脚本
>
> `XSS攻击`:跨站点脚本攻击，攻击者通过构造恶意的浏览器脚本文件，使其在其他用户的浏览器运行进而进行攻击。



***



## 10 大数据

> **分布式文件存储`HDFS`架构**  `Hadoop`
>
> > 将数以万计的服务器组成统一的文件存储系统，`NameNode`：控制块，负责元数据的管理(记录文件名，访问权限，数据存储地址等)，真正的文件存储在`DataNode`中。
> >
> > **`MapReduce`**:大量数据需要相应的算法进行数据分析，通过深度学习/机器学习进行预测，从而获取有效的价值
> >
> > `Map`:将每个服务器上启动Map进程，计算后输出一个集合。
> >
> > `Reduce`:在每个服务器上启动多个reduce进程，然后将所有的map输出的集合进行`shuffle`操作:将相同的`ekey`发送到同一个reduce进程，在reduce中完成数据关联的操作.
> >
> > 



***



## Hive

> 基于`Hadoop`的一个数据仓库工具，用来进行数据提取、转化、加载，这是一种可以存储、查询和分析存储在`Hadoop`中的大规模数据的机制



***



## Spark

> `Spark ` 是基于内存计算的大数据并行计算框架。基于此说说上面`hadoop`（由Apache开发的分布式系统基础架构，用户可以在不了解分布式底层细节的情况下，开发分布式程序。）中组件的缺点
>
> 磁盘IO开销大。每次执行都需要从磁盘读取并且计算完成后还需要将中间结果存放于磁盘
>
> 表达能力有限。大多数计算都需要转化为Map和Reduce两个操作，难以描述复杂的数据处理

> Spark 优点：
>
> - 编程模型不限于map和reduce，具有更加灵活的编程模型
>
> - spark提供内存计算，带来更高的迭代效率且封装了良好的机器学习算法
> - 采用了基于图DAG的任务调度机制 



***



## Flink

> 大数据处理的新规。



## MySql

> port:3306
>
> 配置文件：/etc/my.cnf
>
> service mysqld start
>
> mysql -u root -p
>
> 12315
>
> quit
>
> service mysqld stop



## Redis

> port:6379
>
> 配置文件：/etc/redis/6379.conf
>
> systemctl restart redis_6379.service
>
> systemctl status redis_6379.service：查看redis服务状态
>
> redis-cli：启动客户端
>
> auth 12315
>
> shutdown：关闭了redis服务
>
> ### 底层
>
> >  常见对外暴露数据结构：字符串Siring、列表List、散列Hash、集合、有序集合
> >
> >  底层数据结构：
> >
> >  - SDS
> >  - 链表：双向链表
> >  - 字典：基于hashtable实现,ht[2]->ht->hashEnry[]
> >  - 跳表：随机性：插入节点后抛硬币决定是否晋升
> >  - 整数集合：Set底层实现之一
> >  - 压缩列表：每个节点保存字节数组或一个整数值
> >
> >  
>
> #### 跳跃表：Sorted-set
>
> > 删除插入查找logn，空间2n
> >
> > 删除：
> >
> > > 1. 自上而下，查找第一次出现节点的索引，并逐层找到每一层对应的节点。O（logN）
> > > 2. 删除每一层查找到的节点，如果该层只剩下1个节点，删除整个一层（原链表除外）。O（logN）
> >
> > 插入：
> >
> > > 1. 新节点和各层索引节点逐一比较，确定原链表的插入位置。O（logN）
> > > 2. 把索引插入到原链表。O（1）
> > > 3. 利用抛硬币的随机方式，决定新节点是否提升为上一级索引。结果为“正”则提升并继续抛硬币，结果为“负”则停止。O（logN）
>
> #### 持久化
>
> > RDB快照和AOF日志，Memcached不支持持久化
>
> #### 分布式
>
> > Memcached不支持分布式，只能在客户端使用一致哈希来实现分布式存储
> >
> > Redis Cluster实现了分布式支持
>
> #### 内存管理
>
> > 长期不用的数据会被交换到磁盘，M不会
> >
> > M内存分段，解决内存碎片但是利用率下降
>
> #### 数据淘汰策略
>
> > volatitle-lru/ttl/random：已设置过期时间的数据集
> >
> > allkeys-lru/ttl/random：所有数据集
>
> 缓存穿透：缓存和数据库中都没有数据，而用户不断发起请求id-1或不存在的特别大数据，因为数据库的id都是从1开始自增上去的--接口校验，权限校验/BloomFilter：判断key在数据库中是否存在
>
> 缓存击穿：一个Key非常热点，高并发请求，Key失效时，持续的大并发穿破缓存，直接请求数据库--设置热点数据永不过期/加上互斥锁
>
> 缓存雪崩：多个key大面积失效--随机有效时间



## Kafka

> P/S
>
> - 快速持久化：通过磁盘顺序读写与零拷贝机制，可以在O(1)的系统开销下进行消息持久化
> - 高吞吐：在一台普通的服务器上既可以达到10W/s的吞吐速率
> - 高堆积：支持topic下消费者较长时间离线，消息堆积量大
> - 完全的分布式系统：Broker、Producer、Consumer原生自动支持分布式，依赖zookeeper自动实现复杂均衡
> - 支持Hadoop数据并行加载：对于像Hadoop的一样的日志数据和离线分析系统，但要求实时处理的限制



## Nginx

> port:80
>
> 配置文件：/usr/local/nginx/conf/nginx.conf
>
> nginx
>
> nginx -s stop
>
> nginx -s reload：修改配置文件后可刷新



## TOMCAT

> port:8080
>
> service tomcat start 
>
> service tomcat stop



## Docker

> 



## RabbitMQ

> port:15672
>
> systemctl start rabbitmq-server.service
>
> MQ:
>
> > 消息中间件的组成：
> >
> > 2.1Broker：消息服务器，作为server提供消息核心服务
> >
> > 2.2Producer：消息生产者，负责生产消息发送给broker
> >
> > 2.3Consumer：消息消费者，业务处理方，负责从broker获取消息并进行业务逻辑处理
> >
> > 2.4Topic：主题，发布订阅模式下的消息同一汇集地，不同生产者向topic发送消息，由MQ服务器分发到不同的订阅者，实现消息的广播
> >
> > 2.5Queue：PTP模式下，特定生产者向特定queue发送消息，消费者订阅特定的queue完成指定消息的接收
> >
> > 2.6Message：消息体，根据不同通信协议定义的固定格式进行编码的数据包，来封装业务数据，实现消息的传输
>
> 消息中间价模式分类：
>
> > 点对点：queue
> >
> > 发布订阅：使用topic作为通信载体，每个消息所有消费者都可消费
>
> 消息中间件的优势：
>
> > 系统解耦：
> >
> > 交互系统之间没有直接的调用关系，只是通过消息传输，系统侵入性不强，耦合度低
> >
> > 提高系统响应时间：响应要求不高的使用消息队列
