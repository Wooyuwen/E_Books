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

> **`Spark `**是基于内存计算的大数据并行计算框架。基于此说说上面`hadoop`（由Apache开发的分布式系统基础架构，用户可以在不了解分布式底层细节的情况下，开发分布式程序。）中组件的缺点
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

