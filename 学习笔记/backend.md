##  缓存架构

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

##  负载均衡

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



## Linux服务器参数

> ip:192.168.238.131/防火墙已关闭
>
> 用户:wyw
>
> m:62**
>
> ### ssh连接：
>
> jar打包:mvn package，然后把target文件夹中的jar包提至上层目录
>
> war打包：
>
> - 首先修改pom文件:'jar'->'war'，将内嵌的tomcat去除，选择使用外置的tomcat
>
> - mvn clean，mvn package 同理



***



## Hadoop

> 分布式系统基础架构
>
> HDFS、MapReduce、Hbase



### HDFS

> 分布式文件系统，以流处理模式来存储文件，一次写入，多次读取
>
> ![image-20211121185810744](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20211121185810744.png)
>
> 分片冗余，本地校验，维护多个数据副本
>
> **block**：128MB，单个文件不占用整个块（相较于传统文件磁盘）
>
> **主从模式**：namenode、datanode，需要保证namenode的可用性（本地主从备份，网络备份）
>
> 1. 高可靠性。Hadoop系统中数据默认有三个备份，并且Hadoop有系统的数据检查维护机制，因而提供了高可靠性的数据存储。
> 2. 扩展性强。Hadoop在普通PC服务器集群上分配数据，通过并行运算完成计算任务，可以很方便的为集群扩展更多的节点。
> 3. 高效性。Hadoop能够在集群的不同节点之间动态的转移数据。并且保证各个节点的动态平衡，因此处理速度非常快。
> 4. 高容错性。Hadoop能够保存数据的多个副本，这样就能够保证失败时，数据能够重新分配



### HBase

> 分布式列数据库，快速随机访问海量结构化数据，提供对数据随时读写访问
>
> 利用HDFS提供的容错能力和MapReduce提供的高性能计算能力来实现海量数据的高速存储和读取
>
> 行式存储和列式存储的区别：
>
> > 行式数据库适合频繁的增删改查，或者选取所有属性
> >
> > 列式数据库适合随机读取，单个列也可作为索引，可以针对各列并发执行运算
>
> **Rowkey**：类似于mysql中的主键
>
> **ClolumnFamily**：列族，类似于关系型数据库的列
>
> **存储机制**：表模式定义为列族，对应OLAP
>
> 三种查询方式：
>
> 1. 基于Rowkey的单行查询
> 2. 基于Rowkey的范围查询
> 3. 全表扫描 



### Hive

> 基于`Hadoop`的一个数据仓库工具，用来进行数据提取、转化、加载，这是一种可以存储、查询和分析存储在`Hadoop`中的大规模数据的机制



## Spark

> `Spark ` 是基于内存计算的大数据并行计算框架。基于此说说上面`hadoop`（由Apache开发的分布式系统基础架构，用户可以在不了解分布式底层细节的情况下，开发分布式程序。）中组件的缺点
>
> 磁盘IO开销大。每次执行都需要从磁盘读取并且计算完成后还需要将中间结果存放于磁盘
>
> 表达能力有限。大多数计算都需要转化为Map和Reduce两个操作，难以描述复杂的数据处理

> Spark 优点：
>
> - 编程模型不限于map和reduce，具有更加灵活的编程模型
> - spark提供内存计算，带来更高的迭代效率且封装了良好的机器学习算法
> - 采用了基于图DAG的任务调度机制 
>
> ![image-20211121223502404](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20211121223502404.png)
>
> 



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
> auth 123456
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
> > volatile-lru/lfu/ttl/random：已设置过期时间的数据集
> >
> > allkeys-lru/ttl/random：所有数据集
>
> #### 缓存模式
>
> > 只读缓存
> >
> > 读写缓存
>
> #### 常见问题
>
> > **缓存穿透**：缓存和数据库中都没有数据，而用户不断发起请求id-1或不存在的特别大数据，因为数据库的id都是从1开始自增上去的--接口校验，权限校验/BloomFilter：判断key在数据库中是否存在
> >
> > **缓存击穿**：一个Key非常热点，高并发请求，Key失效时，持续的大并发穿破缓存，直接请求数据库--设置热点数据永不过期/加上互斥锁
> >
> > **缓存雪崩**：多个key大面积失效--随机有效时间



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

> 应用容器引擎，打包应用到一个轻量级、可移植的容器中，然后发布到Linux机器上，实现虚拟化
>
> 应用场景：
>
> - Web应用的自动化打包和发布
> - 自动化测试和持续集成、发布
> - 在服务器环境中部署和调整数据库或者其他后台应用
>
> **常用命令**：
>
> > docker ps  (-a)
> >
> > docker run -it image /bin/bash 创建进入容器伪终端
> >
> > docker start/stop/restart
> >
> > -d：后台运行，不会进入容器，需要使用`docker exec`，用`attach`退出会停止容器
> >
> > docker exec -it id /bin/bash
> >
> > -p：映射宿主机端口至容器端口 `docker port`查看
> >
> > -P：端口随即映射
> >
> > ```shell
> > docker run -d -p 127.0.0.1:5001:5000 training/webapp python app.py
> > ```
> >
> > 



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



## RPC 

> Remote Procedure Call：远程过程调用协议，RPC适合内部服务间的通信调用，如同调用本地函数功能，相比于HTTP更小众
>
> 1 通讯协议
>
> 2 寻址
>
> 3 传输控制，传输效率高：二进制传输
>
> 为什么要用RPC：
>
> > - 微服务
> > - 分布式架构系统
> > - 服务可重用
> > - 系统间交互调用
>
> 常用RPC框架：
>
> > 传统的webservice框架：apache CXF、 apache Axis2
> >
> > 新兴的微服务框架：Dubbo、springcloud、apache Thrift、ICE、GRPC
>
> 调用过程：
>
> ![image-20211118195725927](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20211118195725927.png)
>
> 1. 调用者（客户端Client）以本地调用的方式发起调用；
>
> 　　2. Client stub（客户端存根）收到调用后，负责将被调用的方法名、参数等打包编码成特定格式的能进行网络传输的消息体；
> 　　3. Client stub将消息体通过网络发送给服务端；
> 　　4. Server stub（服务端存根）收到通过网络接收到消息后按照相应格式进行拆包解码，获取方法名和参数；
> 　　5. Server stub根据方法名和参数进行本地调用；
> 　　6. 被调用者（Server）本地调用执行后将结果返回给server stub；
> 　　7. Server stub将返回值打包编码成消息，并通过网络发送给客户端；
> 　　8. Client stub收到消息后，进行拆包解码，返回给Client；
> 　　9. Client得到本次RPC调用的最终结果。



## Dubbo

> 开源RPC框架
>
> 提供 **服务自动注册、自动发现**等高效服务治理方案
>
> 接口工程：存放实体bean和业务接口
>
> 服务提供者：业务接口的实现类，调用数据持久层
>
> 服务消费类：处理浏览器客户端发送的请求



## SpringBoot启动

> 一：启动类上注解：**@SpringBootApplication**
>
> 二：启动类中的main方法
>
> 启动类注解所包含三个核心注解
>
> - **@SpringBootConfiguration**：包含了@Configuration，指明当前类为配置类
>
> - **@EnableAutoConfiguration**：注册当前启动类的根，自动注入，开启SpringBoot注解功能，借助@Import的支持，收集和注册特定场景相关的bean定义，即将所有符合自动配置条件的Bean定义加载到IOC容器
>
> - **@ComponentScan**：扫描并加载符合条件的组件如(@Component、@Repository)，未指定则扫描当前所在包及其子孙包内的Bean，将这些Bean定义加载到IOC容器中
>
> ```java
>SpringApplication.run(MainApplication.class,args);
> //启动SpringBoot项目
> ```
> 
> 
>



## SpringBoot IOC

> IOC容器负责实例化、定位、配置应用程序中的对象及建立这些对象间的依赖。交由Spring容器统一进行管理，从而实现松耦合
>
> **Spring如何从bean定义创建对象**：
>
> 1. 容器启动，创建和初始化IoC 容器
> 2. 扫描包下所有class，通过反射解析class类的信息，包括注解信息
> 3. 基于反射实例的对象，对其进行封装
> 4. 将实例的对象放入集合中保存
> 5. 可以通过getBean获取集合中对象，ApplicationContext容器--框架核心 
>



> **Bean的定义**：
>
>  1 @Component，组件扫描，衍生：@Service@Repository@Controller
>
>  2  自定义Java配置类使用@Bean注释的bean工厂方法，@Configuration
>
>  3 XML配置文件中声明bean定义



> **Bean属性**：
>
> - 类：一个类可能含有几个bean实例
>
> - 名称：唯一，有默认值
>
> - 依赖：使用其他bean来执行作业，spring首先需要创建这些依赖项
>
> - 作用域：框架在运行时创建的特定类的实例数，描述了创建新对象的条件
>
>   > **作用域类型**：
>   >
>   > 单例：无状态对象最佳选择
>   >
>   > 原型：多个实例
>   >
>   > 请求
>   >
>   > 会话
>   >
>   > 全局会话
>   >
>   > 应用级别Application
>   >
>   > **如何设置作用域**：@Scope("prototype")（RequestScope等）
>
> - 初始化模式：Spring默认启动时创建所有单例bean，使用@Lazy延迟创建
>
> - 初始化回调：在Spring初始化对象之后运行逻辑(可选的依赖注入)
>
> - 破坏回调：destroyMethod



#### Bean生命周期

> 实例化
>
> 属性赋值
>
> 初始化
>
> 销毁



> > **@SpringBootApplication**：开启Spring的组件扫描和SpringBoot自动配置功能
> >
> > **@SpringBootConfiguration**：@Configuration的二次封装，可以理解配置类的注解，会将当前类内声明的一个或多个以@Bean注解标记的方法的实例纳入到spring容器中
> >
> > **@Configuration**：标注配置类（含多个bean注释的方法）
> >
> > **@Autowired**：对类成员变量、方法及构造函数进行标注，完成自动装配的工作，类似@Resource@Inject
> >
> > **@EnableCaching**：注解驱动的缓存管理功能。自spring版本3.1起加入了该注解。如果你使用了这个注解，扫描每一个spring bean，查看是否已经存在注解对应的缓存，已存在则创建一个代理拦截方法调用，使缓存的bean执行处理。
> >
> > **@Slf4j**：日志标准，提供接口和获取具体日志对象的方法



## SpringBoot AOP

> ##### 常见概念
>
> Java代理模式，五种实现方法：
>
> > - 静态代理：编译时产生class字节码文件，效率较高，但是会产生较多代理类
> >
> > - **动态代理**：对接口代理。反射机制实例化代理对象，生成的动态代理类继承Proxy类，并实现公共接口,InvocationHandler接口为被动态代理类回调的接口（调用处理器接口），可通过Proxy.getInvocationHandler获取代理类实例的**调用处理器对象**，调用代理的接口中声明的方法时，最终都会调用处理器中的invoke方法，特点：hashCode，equals和toString同样会分配给invoke：三个方法呈现了类的属性，具有区分度。
> >
> >   使用场景
> >
> >   > - 日志集中打印
> >   > - 事务
> >   > - 权限管理
> >   > - AOP
> >
> > - **CGLIB**：asm字节码技术，只需要对象实例，创建继承实例的对象，对指定的业务类继承并覆盖其中的业务方法，CGLIB可以传入接口或者普通的类，方法访问优化：建立方法索引的方法避免像JDK动态代理一样通过Method反射调用，相比于动态代理对Object的方法重写了clone方法，通过生成字节码实现代理，比反射稍快，不存在性能问题，但需要重写目标类的方法。
> >
> > - AspectJ：SpringBoot使用，修改目标类的字节，编译时插入动态代理的字节码，不会生成新的Class
> >
> > - 基于instrumentation实现动态代理，修改目标类字节码、类装载时动态拦截去修改
>
> 
>
> SpringBoot使用AspectJ实现AOP，内层拦截器
> 
> **拦截器**：
> 
> > - **@Before**：这种拦截器先执行拦截代码，再执行目标代码。如果拦截器抛异常，那么目标代码就不执行了；
> > - **@After**：这种拦截器先执行目标代码，再执行拦截器代码。无论目标代码是否抛异常，拦截器代码都会执行；
> > - **@AfterReturning**：和@After不同的是，只有当目标代码正常返回时，才执行拦截器代码；
> > - **@AfterThrowing**：和@After不同的是，只有当目标代码抛出了异常时，才执行拦截器代码；
> > - **@Around**：能完全控制目标代码是否执行，并可以在执行前后、抛异常后执行任意拦截代码，可以说是包含了上面所有功能。



![image-20211106211500658](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20211106211500658.png)

#### Intercepter和Filter

> ![image-20211106192051573](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20211106192051573.png)
>
> **Intercepter**：拦截用户请求，进行处理，比如判断用户登录情况、权限验证，只针对Controller进行处理，通过HandlerIntercepter接口实现，Java中的拦截器基于反射实现(JDK动态代理)
>
> 两种使用情况：
>
> - 会话拦截：实现spring的HandlerIntercepter接口并注册到mvc的拦截队列中，其中preHandle()方法在调用Handler之前进行拦截，postHandler()方法在视图渲染之前调用，afterCompletion()方法在返回相应之前执行，需要在WebMvnConfigurationSupport中注册拦截器
> - 方法拦截：上面AOP讲到的@Aspect注解，@Before等注解
>
> 
>
> **Filter**：过滤字符编码、业务逻辑判断，主要用于对用户请求进行预处理，同时也进行逻辑判断。Filter在请求进入servlet容器执行service()方法之前就会经过filter过滤（步骤**1**），不像Intreceptor一样依赖于springmvc框架，只需要依赖于servlet。Filter启动是随WEB应用的启动而启动，只需要初始化一次，以后都可以进行拦截。
>
> - 用户授权Filter：检查用户请求，根据请求过滤用户非法请求
> - 日志Filter：记录某些特殊的用户请求
> - 解码Filter：对非标准编码的请求解码
>
> Intercepter和Filter的不同：
>
> > Filter基于函数回调，前者基于反射(AOP之一的动态代理)
> >
> > Filter依赖于Servlet容器，前者依赖mvc框架
> >
> > Filter对几乎所有的请求起作用，前者只能对action请求起作用
> >
> > Intercepter可以访问Action中的上下文，值栈里的对象
>
> 
>
> **Listener**：Java监听器，系统级别监听，随web应用的启动而启动，常见使用模式：观察者模式



#### AOP流程：

> **1 代理创建**：JDK动态代理或者CGLIB：
>
> > - 首先，需要创建代理工厂，代理工厂需要 3 个重要的信息：拦截器数组，目标对象接口数组，目标对象。
> >
> > - 创建代理工厂时，默认会在拦截器数组尾部再增加一个默认拦截器 —— 用于最终的调用目标方法。
> >
> > - 当调用 getProxy 方法的时候，会根据接口数量大余 0 条件返回一个代理对象（JDK or  Cglib）。
> >
> > 注意：创建代理对象时，同时会创建一个外层拦截器，这个拦截器就是 Spring 内核的拦截器。用于控制整个 AOP 的流程。
>
> **2 代理的调用**：
>
> > - 当对代理对象进行调用时，就会触发外层拦截器。
> >
> > - 外层拦截器根据代理配置信息，创建内层拦截器链。创建的过程中，会根据表达式判断当前拦截是否匹配这个拦截器。而这个拦截器链设计模式就是[职责链模式](https://segmentfault.com/a/1190000040450513)。
> >
> > - 当整个链条执行到最后时，就会触发创建代理时那个尾部的默认拦截器，从而调用目标方法。最后返回。



## SpringMVC

> [源码地址](https://github.com/cbeams/spring-framework-i21/blob/master/src/com/interface21/web/servlet/DispatcherServlet.java)
>
> 流程：
>
> ![image-20211108140912620](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20211108140912620.png)
>
> **1 设置属性**
>
> ```java
> // 1. 设置属性
> // Make web application context available
> request.setAttribute(WEB_APPLICATION_CONTEXT_ATTRIBUTE, getWebApplicationContext());
> 
> // Make locale resolver available
> request.setAttribute(LOCALE_RESOLVER_ATTRIBUTE, this.localeResolver);
> 
> // Make theme resolver available
> request.setAttribute(THEME_RESOLVER_ATTRIBUTE, this.themeResolver);
> ```
>
> 
>
> **2 根据Request请求的URL得到对应的handler执行链，也即拦截器和Controller代理对象**
>
> ```java
> // 2. 找 handler 返回执行链
> HandlerExecutionChain mappedHandler = getHandler(request);
> ```
>
> 
>
> **3 得到handler的适配器**
>
> ```java
> // This will throw an exception if no adapter is found
> // 3. 返回 handler 的适配器
> HandlerAdapter ha = getHandlerAdapter(mappedHandler.getHandler());
> ```
> 
>**4 循环执行handler的pre拦截器(Intercepter)**
> 
>```java
> // 4. 循环执行 handler 的 pre 拦截器
> for (int i = 0; i < mappedHandler.getInterceptors().length; i++) {
> 	HandlerInterceptor interceptor = mappedHandler.getInterceptors()[i];
> 	// pre 拦截器
> 	if (!interceptor.preHandle(request, response, mappedHandler.getHandler())) {
> 		return;
> 	}
> }
> ```
> 
>
> 
>**5 执行真正的handler，并返回ModelAndView**
> 
>```java
> // 5. 执行真正的 handler，并返回  ModelAndView(Handler 是个代理对象，可能会执行 AOP )
> ModelAndView mv = ha.handle(request, response, mappedHandler.getHandler());
> ```
> 
>
> 
>**6 循环执行handler的post拦截器**
> 
>```java
> // 6. 循环执行 handler 的 post 拦截器
> for (int i = mappedHandler.getInterceptors().length - 1; i >=0 ; i--) {
> 	HandlerInterceptor interceptor = mappedHandler.getInterceptors()[i];
> 	// post 拦截器
> 	interceptor.postHandle(request, response, mappedHandler.getHandler());
> }
> ```
> 
>
> 
>**7 根据ModelAndView信息得到View实例**
> 
>```java
> View view = null;
> if (mv.isReference()) {
> 	// We need to resolve this view name
> 	// 7. 根据 ModelAndView 信息得到 View 实例
> 	view = this.viewResolver.resolveViewName(mv.getViewName(), locale);
> }
> ```
> 
>

