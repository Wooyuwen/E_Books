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
>> > > Load balancing 
>> > >
>> > > Web acceleration
>> > >
>> > > Security and anonymity(匿名)
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

> > 大应用拆分为小模块
> >
> > 小模块不属于集群中
> >
> > 通过远程调用的方式依赖各个独立的模块完成业务的处理
>
> 服务提供者：微服务的具体提供者，其他通过接口调用即可；
>
> 服务消费者：按照提供者接口编程即可。



***



***



## 8 HTTP 攻击与防护

> `SQL注入`:攻击者在请求参数的时候，包含了恶意的脚本
>
> `XSS攻击`:跨站点脚本攻击，攻击者通过构造恶意的浏览器脚本文件，使其在其他用户的浏览器运行进而进行攻击。





