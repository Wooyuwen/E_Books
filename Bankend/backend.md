## 分布式 

> 垂直伸缩
>
> 水平伸缩

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
>> > 反向代理：对客户端隐藏服务器端IP
>
>`旁路缓存`:应用程序需要自己从数据源读取数据，然后将其写入旁路缓存中。下次需要数据可直接访问旁路缓存。
>
>缓存缺点：
>
>> 过期失效：标记失效时间；
>>
>> 失效通知：应用程序在更新数据源的时候，通知清楚缓存中的数据；
>>
>> 缓存适合带有热点的数据，如热卖商品，微博热点，易被多次读取。

## 3 异步架构

