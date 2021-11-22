## 基本概念

#### 修饰符、关键字

> > ##### `final`: （类，方法，变量）
>
> > - 初始化：定义时或者静态代码块
>
> > - 类  ：不能继承，类中的成员变量可以设计为final，final中的成员方法会被隐式指定为final方法。
> > - 方法：不能被重写，一个类的private方法会被隐式的指定为final方法。
> > - 成员变量：必须要附初始值（`直接赋值` `全部在构造方法中赋值`），只能初始化一次。**如果修饰的是引用类型，则说明引用的地址的值不能修改，但是引用的对象的值可变**。
> > - **final static**：因为是static，会在对象创建前分配空间，因为是final，必须在分配空间的时候赋值，常用来修饰常量。final static double PI=3.14
>
> > ###### `static`：（方法、变量）
> >
> > - 变量（**类变量**）：被所有的对象所共享，在内存中只有一个副本，当且仅当在类初次加载时会被初始化。
> >
> > - 代码块：优化程序性能，在**类初次被加载**时，会按照static块的顺序来执行每个static块，并且执行一次，因此很多时候将只需进行一次的初始化操作放在static代码块中进行。static代码块能出现在类中任何地方。
> >
> >   ```java
> >   static{
> >       System.out.println("test static");
> >   }
> >   ```
> >
> > - 静态变量并没有像c++的作用域，作用域的限制只能通过private/public/protected来修饰。
> >
> > - **静态方法和静态变量都可以通过对象访问,因为静态方法不依赖具体实例，所以必须实现。**
>



**序列化/反序列化**：

> - **序列化**： 将数据结构或对象转换成二进制字节流的过程
> - **反序列化**：将在序列化过程中所生成的二进制字节流转换成数据结构或者对象的过程
>



**transient**:反序列化，对应对象只存于内存而不会存于磁盘中，如银行卡号等贵重东西

> - `transient` 只能修饰变量，不能修饰类和方法。
> - `transient` 修饰的变量，在反序列化后变量值将会被置成类型的默认值。例如，如果是修饰 `int` 类型，那么反序列后结果就是 `0`。
> - `static` 变量因为不属于任何对象(Object)，所以无论有没有 `transient` 关键字修饰，均不会被序列化。



> `synchronized`**: **方法
>
> > - 同一时间只能被一个线程访问，可以搭配四个访问修饰符使用。
>
> `volatile`**：（**成员变量）
>
> > - 所修饰的成员变量每次被线程访问时，都强制从共享内存中重新读取该成员变量的值；
> > - 当成员变化时，会强制线程将变化后的值写回内存。
>
> ` protected`:
>
> > - 和private一样不能修饰类，同一包内所有类都可以访问，但是不同包只能通过继承，而且只能调用继承的方法，而不能使用父类实例的`protected`方法。
> >
> > 访问控制：
> >
> > - 父类方法声明为public，子类public
> >- 父类protected方法，子类public 或protected
> > - 父类private方法，子类.................子类没有XD





####  基本数据类型

> `boolean `不确定大小，编译后会使用其他数据类型来表示，将布尔值编译为Java虚拟机int数据类型的值：32位CPU一次处理数据是32位。Boolean数组会编译位Byte数组
>
> `byte 8`
>
> `char 16` :字符编码，`a`-97 `A`-75 `0`-48
>
> `short 16`
>
> `int 32`
>
> `float 32`
>
> `long 64`
>
> `double 64`

> ```java
> byte b1=(byte)128;//-128
> ```
>
> 计算机中的数据都是由补码形式存储的
>
> 除了Double和float都实现了常量池



#### ThreadLocal

> 关于创建线程局部变量的类
>
> 目的是为了解决多线程访问资源时的共享问题
>
> ```java
> public void set(T value) {
>  Thread t = Thread.currentThread();
>  ThreadLocalMap map = getMap(t);
>  if (map != null)
>      map.set(this, value);
>  else
>      createMap(t, value);
> }
> ```
>
> ```java
> ThreadLocalMap getMap(Thread t) {
>  return t.threadLocals;
> }
> ```
>
> ```java
> class Thread implements Runnable {
>  /* ThreadLocal values pertaining to this thread. This map is maintained
>      * by the ThreadLocal class. */
> 
>     ThreadLocal.ThreadLocalMap threadLocals = null;
> }
> ```
>
> ```java
> public T get() {
>   Thread t = Thread.currentThread();
>   ThreadLocalMap map = getMap(t);
>   if (map != null) {
>     ThreadLocalMap.Entry e = map.getEntry(this);
>     if (e != null) {
>       @SuppressWarnings("unchecked")
>       T result = (T)e.value;
>       return result;
>     }
>   }
>   return setInitialValue();
> }
> ```
>
> 
>
> ThreadLocalMap：当前Thread对象的threadLocals变量，即实际上ThreadLocal的值是放入了当前线程的一个ThreadLocalMap实例中，所以只能在本线程访问
>
> ThreadLocalMap对应Entry类型：
>
> ```java
> static class Entry extends WeakReference<ThreadLocal<?>> {
>   /** The value associated with this ThreadLocal. */
>   Object value;
> 
>   Entry(ThreadLocal<?> k, Object v) {
>     super(k);
>     value= v;
>   }
> }
> ```
>
> 
>
> 对象存放在堆上，实例创建的类持有，值线程持有
>
> 如何被其他线程访问：InheritableThreadLocal：将某个线程的ThreadLocal值在其子线程创建时传递过去



#### String/StringBuilder/StringBuffer

> 创建之后不能修改存在内存中的值（hashcode
>
> StringBuffer/StringBuilder
>
> `StringBuilder`和`StringBuffer`都继承自`AbstractStringBuilder`类，在ASB中也使用字符数组保存字符串，但是没用final修饰，StringBuilder和StringBuffer都是调用父类构造方法实现的，字符串操作也是父类定义的方法如expandCapacity、append、insert、indexOf等公共方法
>
> **应用场景**：
>
> - String：常量声明
> - 频繁进行字符串的运算并运行在多线程下，如XML解析、HTTP参数解析与封装
> - 频繁进行字符串解析并单线程运行，如SQL语句拼装，JSON封装等
>
> String作为参数按值传递
>
> 常量池：JVM在堆中分配的一块空间，用来存储显式的数据
>
> 为什么设计为不可变：
>
> - 便于实现字符串池
> - 使用多线程安全
> - 避免安全问题（网络连接和数据库连接中字符串常作为参数）
> - 加快字符串处理速度：保证了hashcode的唯一性，创建对象时其hashcode可以直接缓存，如Map喜欢将String作为Key的原因



####  JVM内存结构

> 共用：
>
> > `堆`:存放对象的实例和数组，虚拟机管理的最大的一块内存
> >
> > `方法区`;放置已经被虚拟机加载的类信息，常量，静态变量，编译器编译的代码，理论上没有内存限制
>
> 私有：
>
> > - `程序计数器` 存放当前线程执行字节码的行号
> >
> > - `虚拟机栈` 存放线程执行所需要的变量，对象的引用，方法出口信息，动态链接等信息
> >
> > - `本地方法栈`和虚拟机栈类似，区别：`JNI` `（Java Native Interface）`和本地C代码交互的API
> >
> 



## 面向对象


> - `this `
>
>   > 关键字指向当前对象实例，存于堆中该实例所在内存中
>   >
>   > 存储的为该实例的地址
>   >
>   > 可以由实例方法和构造方法调用，不能被静态方法调用
>   >
>   > 主要用来区分实例变量和局部变量
>
> - `静态方法`（static）
>
>   > - 只能调用静态方法和静态变量
>   >
>   > - 静态方法可以通过类名直接调用或者通过类的实例对象去调用
>
> - `继承` `extends`
>
>   > - 代码复用
>   >
>   > - **构造方法无法继承，私有的数据无法直接访问，调用父类的方法访问**
>   >
>   > - 默认继承Object类
>   >
>   > - 只允许单继承
>   >
>   > - 子类会自动调用父类无参数的构造方法，带参数的构造方法需要用关键字super显式调用
>   >
>   >   若父类没有默认构造函数，则必须显式调用父类带参数构造函数。
>   >
>   >   - 构造函数执行顺序：先父类，后子类
>   >
>   >   - 类内部的构造顺序：static类的引用= new ...，先构造，只做一次；类的引用=new ...，次之，每次构造都做一次，如果声明时没有加"=new ..."仅神明，不构造；再执行构造函数。



#### 静态分派和动态分派

> 函数调用在class文件中存储的是符号引用
>
> `类加载`期或者`运行`期确定目标方法的直接调用
>
> **解析**
>
> > 所有方法调用中的目标方法在class文件中都是常量池中的引用，
> >
> > - 在`类加载`的`解析`阶段，会将其中的一部分符号引用转化为直接引用。  前提：方法在程序真正执行前就有一个可确定的调用版本，并且这个方法的调用版本在运行期间是不可改变的，在编译期间就确定下来，这类方法的调用成为`解析`    这类 **非虚方法**有：`静态方法`、`私有方法`、`实例构造器`、`父类方法`。
> >
> >   与之相反的是虚方法(final方法除外)



#### 抽象类和抽象方法

> - 抽象类不能被实例化
> - 抽象类中不一定含有抽象方法，但是抽象方法一定属于抽象类
> - 抽象类中的抽象方法只是声明，不包含方法体（jdk1.8有默认实现）
> - 构造方法和类方法（用static修饰的方法）不能声明为抽象方法



#### 创建的对象存储

> - 寄存器：最快的存储区，位于处理器的内部，但是数量有限
>
> - 堆栈：位于通用RAM（随机访问存储器），常存储对象的引用；堆栈指针的上下移动可以实现内存的分配和释放。
>
>   > `hints:`创建程序时，Java系统必须知道存储在堆栈内所有项的确切生命周期
>
> - 堆：通用的内存池，用于存放所有的对象，常用操作符为`new`关键字。
>
> - 常量存储：通常直接存放在程序代码内部，比较安全，永远不会改变。
>
> - 非RAM存储：如果数据完全存活于程序之外，不受程序的任何控制，在程序没有运行时也可以存在：`持久化对象` `流对象`。在流对象中，对象转化成字节流，通常被发送给另一台机器。在持久化对象中，对象被放在磁盘上，因此，即使程序终止，他们仍能保持自己的状态，这种存储方式的技巧在于：将对象转化为可以存放在其他媒介上的事物，在需要时，可以恢复成常规的、基于RAM的对象。JAVA提供了对轻量级持久化的支持。



#### 内部类

> `普通内部类`:（需要外部类实例）
>
> > - 内部类可使用外部类的变量和方法
> > - 外部类可以创建内部类实例
> > - 作用域关键字用法相同
> > - 需要外部类**实例**创建内部类对象 
> > - `不能定义静态变量、能定义常量`
>
> `静态内部类`(用static修饰)
>
> > - 只能访问外部类的static方法和变量
> > - 可以直接创建，不需要外部类引用
>
> `局部内部类`
>
> > - 定义在程序块中，只在块内有效；块外不能创建和引用
> > - 只能用abstract和final修饰
> > - 可以访问外部类成员
> > - 可访问块中的final局部变量
>
> `匿名内部类`
>
> > - 没有引用名的对象
> > - 所有变量都是final类型
> >
> > ```java
> >new Test().show();
> > ```
> > 
> > - 匿名类：继承父类或者实现接口
> >
> >   ```java
> >    new Test(){
> >           @override show()
> >   }.show();
> >   ```



## 多态

#### 重写

> - 存在于继承体系中，指子类实现了一个与父类在方法声明上完全相同的方法
>
> - 需要满足里式替换原则：
>
> - > - 访问权限不能变小
>   > - 返回类型不能扩大
>   > - 异常类型


#### 重载

>- 代码美观
>- 方便记忆
>- 存在同一个类中，指一个方法与已经存在的方法名称上相同，但是参数类型/个数/顺序至少有一个不同
>- 返回值不同，不一定是重载


> ##### 引用的类型转换
>
> - 子类可以自动视为父类
> - 父类变成子类需要显示的类型转换（小转大不变，大转小要变）
> - 除了继承关系，否则不允许类型转换
>
> 里氏替换法则：
>
> - 子类对象能够替换其父类对象被使用
> - 子类的引用可以直接赋值给父类的引用



#### 抽象类

> 抽象类必须被继承实现：可以用来定义某些共性



 #### 接口

> 把需要实现的方法和共有常量定义在接口里：
>
> - 接口的变量默认为`public final static`（常量）
> - 接口的方法默认为`public abstract` (需要类来实现)
>
> 借口的定义：`default`和`public`两种类型：
>
> > public的接口，必须定义在同名文件里
>
> - 可以有内部接口（类似于内部类）、没有局部接口
>
> - 可以用匿名类实现接口
>
> java 1.8允许接口实现方法 `default`
>
> **invokedynamic**：lambda表达式：函数接口：只有一个抽象函数的接口，可省略接口名，方法名甚至是变量类型（java强类型推断）
>
> 接口可以extends其他接口，一个类可以实现多个接口
>
> 接口的私有方法：方便接口内复用
>
> **接口实现**：一个类可以实现多个接口，接口之间用逗号分隔
>
> **二义问题解决**：继承的父类有同名方法、实现的接口有同名默认方法——>**父类优先**
>
> 接口的静态方法：方便复用/只能通过**接口名.方法名**调用，继承该接口的接口，不能继承静态方法
>
> 实现该接口的类，也不能继承静态方法



#### Java泛型类

> 类的泛型（模板类）
>
> - 把用到的数据类型抽象为泛型
> - 在类创建的时候才指定类型
> - 此模板可以接受合适类型的对象
>
> 目的：安全，编译期能进行类型检查
>
> 模板可为类：可限定类型必须是某个指定类型或者子类，或是实现了某个接口，够用extends
>
> **静态方法不能使用类的泛型**（泛型类作为参数）因为泛型的确定需要类实例化
>
> <?>：限定泛型的范围，提高代码可读性：程序员能只读功能



#### Java泛型接口、泛型方法

> 静态方法支持泛型方法

**没有泛型对象数组**

JAVA泛型只在编译时处理



常用Java泛型类:

ArrayList 视为变长数组

LinkedList视为链表

## Object 方法

#### public `boolean equals()`  

自反，对称，传递，一致

Instanceof Cast，再比较关心的数据域



> ```java
>  @Override
>  public boolean equals(Object o) {
>      if (this == o) return true;
>      if (o == null || getClass() != o.getClass()) return false;
> 
>      EqualExample that = (EqualExample) o;
> 
>      if (x != that.x) return false;
>      if (y != that.y) return false;
>      return z == that.z;
>  }
> }
> ```
>
> 等价包含相等
>
> 引用相等指二者指向同一个对象，若不是同一个对象，但是成员变量相同——等价
>
> 覆盖equals方法实现自己的比较方法，需要记得覆盖hashCode方法，否则equals返回true，但是`hashCode`返回值不一样->会错误认为两个对象不一样



#### public native `int hashCode()`

> - 返回哈希值，等价的两个对象散列值一定相同
>
> - 覆盖equals方法时应总是覆盖hashCode()方法
>
> - ```java
>   @Override
>   public int hashCode() {
>       int result = 17;
>       result = 31 * result + x;
>       result = 31 * result + y;
>       result = 31 * result + z;
>       return result;
>   }
>   ```



#### public string toString()

> 默认返回 ClassName@442142c的形式，@后数值为散列的无符号十六进制表示



#### protected native `Object clone()` throws CloneNotSupportedException

> 一个类不显示重写clone()，其他类就不能直接调用该实例的clone()方法
>
> ```java
> public class CloneExample {
>  private int a;
>  private int b;
> 
>  @Override
>  public CloneExample clone() throws 		    CloneNotSupportedException {
>      return (CloneExample)super.clone();
>  }
> }
> ```
>
> ```java
> CloneExample e1 = new CloneExample();
> try {
>  CloneExample e2 = e1.clone();
> } catch (CloneNotSupportedException e) {
>  e.printStackTrace();
>     
> }
> ```
>
> 如果没有实现Cloneable()接口，就会抛出异常
>
> #### 	浅拷贝
>
> > 拷贝对象和原始对象的引用同一个对象
>
> #### 深拷贝
>
> > 拷贝对象和原始对象的引用类型引用不同的对象
>
> **TIPS**：使用`clone()`方法来拷贝一个对象即复杂又有风险，会抛出异常还需要类型转换
>
> 可以使用拷贝构造函数或者拷贝工厂来拷贝一个对象
>
> 父类子类方法调用优先级：
>
> ```java
> this.func(this)
> super.func(this)
> this.func(super)
> super.func(super)
> ```



## 反射

> 类在第一次使用时才动态加载到JVM中，也可以使用`Class.forName("com.mysql.jdbc.Driver")`这种方式来控制类的加载，该方法返回一个Class对象
>
> 反射可以提供运行时类信息，并且这个类可以在运行时才加载进来，甚至在编译时期该类的.class不存在也可以加载进来
>
> **程序在运行时取得、甚至修改一个已知名称的类的内部信息，可以动态的创建和编译**
>
> **程序在运行时加载、使用编译期间未知的类**：可加载一个运行时才知名称的类，获得其完整结构
>
> 
>
> Java类：代码里面的类
> 
> Class类：一种Object，当一个类被加载，JVM便自动产生一个Class对象
>
> ```java
>Class myClass = Test.class;
> 
>Test test = new Test();
> Class myClass = test.getClass();
>```
> 
> Class类可以获取对应class的信息：
> 
> > myclass.getPackage()/getSuperClass()
> >
> > myclass.getFields()/getMethods/getModifiers
>>
> > 通过返回的信息，可以：
>>
> > > - 构造类的对象
> > > - 修改域和对象的访问控制权限
> > > - 修改域的数值
> > > - 调用相应的方法



## instanceof 关键字

> 布尔计算(boolean)
>
> Object instanceof Class
>
> a instanceof B
>
> > 判断a是不是B这个类或者接口的实例
> >
> > 返回true
> >
> > > 如果B是对应的类a=new B();
> > >
> > > 或者，是a对应的类的父类
> > >
> > > 或者，是a对应的类实现的接口



## GC

> GC的时候，所有线程都暂停，直到GC结束

栈上分配：Java的对象一般都是分配在堆内存中的，而JVM开启了栈上分配后，允许把线程私有的对象（其它线程访问不到的对象）打散分配在栈上。这些分配在栈上的对象在方法调用结束后即自行销毁，不需要JVM触发垃圾回收器来回收，因此提升了JVM的性能。



#### 四种引用

> > - **强引用**：存在强引用就不会回收对象实例，JVM结束后回收
> >
> > - **软引用**：内存不足时回收，常用来实现内存敏感缓存，软引用可以和一个引用队列(ReferenceQueue)联合使用。如果软引用所引用对象被垃圾回收，JAVA虚拟机就会把这个软引用加入到与之关联的引用队列中
> >
> >   ```java
> >       ReferenceQueue<String> referenceQueue = new ReferenceQueue<>();
> >       String str = new String("abc");
> >       SoftReference<String> softReference = new SoftReference<>(str, referenceQueue);
> >               
> >       str = null;
> >       // Notify GC
> >       System.gc();
> >               
> >       System.out.println(softReference.get()); // abc
> >               
> >       Reference<? extends String> reference = referenceQueue.poll();
> >       System.out.println(reference); //null
> >   ```
> >
> >   
> >
> > - **弱引用**：GC回收，与软引用的区别：只具有**弱引用**的对象拥有**更短暂**的**生命周期**。在垃圾回收器线程扫描它所管辖的内存区域的过程中，一旦发现了只具有**弱引用**的对象，不管当前**内存空间足够与否**，都会**回收**它的内存。不过，由于垃圾回收器是一个**优先级很低的线程**，因此**不一定**会**很快**发现那些只具有**弱引用**的对象。生命周期基本由垃圾收集器决定
> >
> > - **虚引用**：主要用来跟踪对象被垃圾回收器回收的活动。程序可以通过判断引用**队列**中是否已经加入了**虚引用**，来了解被引用的对象是否将要进行**垃圾回收**。如果程序发现某个虚引用已经被加入到引用队列，那么就可以在所引用的对象的**内存被回收之前**采取必要的行动。
>



> 可达性分析：通过构造引用链，侦测没有依赖的对象
>
> > 当一个对象到GC Root没有路径时，可回收
> >
> > GC Root：正在运行的程序可访问的引用变量
> >
> > - 虚拟机栈（局部变量）中的对象引用
> > - 本地方法栈中native方法的对象引用
> > - 方法区中类的静态属性的对象引用
> > - 方法区中常量的对象引用
>
> 
>
> #### 如何进行内存回收
>
> 标记清除算法：从root出发，标记存活的对象，未标记的对象清除，问题：内存空间不连续
>
> 标记整理算法：标记完后，整理存货对象；特点：拷贝整理太慢，整理之后内存连续
>
> 复制算法：适合存活率低场景
>
> 分代算法：
>
> 垃圾回收器：虚拟机对垃圾回收算法的实现/yt
>
> ##### JAVA会发生内存泄漏
>
> > 长生命周期对象持有短周期对象的引用



## 异常

> java.lang.Throwable可以用来表示任何可以作为异常抛出的类，分为两种`Error`和`Exception`

#### Error （可以不检查）

> 其中`Error`用来表示JVM无法处理的错误：脱离程序员控制的，例如栈溢出，编译时检查不到
>
> `VirtualMachineError`
>
> > `StackOverFlowError`
> >
> > `OutOfMemoryError`
>
> `AWTError`



#### Exception

> > #### IOException（必须检查）
> >
> > - 文件找不到`FileNotFoundException`
> > - 读写文件时发生I/O错误`EOFException`
>
> 
>
> > #### RuntimeException（可以不检查）
> >
> > - 网络连接失败
> > - 参数非法
> > - 空引用
> > - 数组越界
> > - 类未找到异常
> > - 算数异常



> `Exception`分为两种：
>
> - `受检异常`：提供机制，强制程序员写异常处理，需要运用try...catch..语句捕获并进行处理，并且可以从异常中恢复；                         (编译器会检查)
> - `非受检异常`：提供机制，让程序员可以在发生异常后，进行处理，例如除0会引发`Arithmetic Exception`（算数异常），此时程序奔溃并且无法恢复             （编译器不会检查）



> `java`异常检测机制:
>
> > ##### 捕获异常:
> >
> > > try/catch/finally
> >
> > ##### 抛出异常
> >
> > > 方法可以不处理异常，而将异常抛出给调用者
> > >
> > > - 方法一：try/catch/finally
> > > - 方法二：throws给调用者的调用者：直到main方法,main方法也能throws给谁
> >
> > ##### 自定义异常
> >
> > > 异常可以是java类也可以是自己定义的异常类
> > >
> > > ```java
> > > void testException(integer i)throws MyException{
> > >     if(i==null){
> > >         MyException e=new MyException("Input i:null");
> > >         throw e;
> > >     }
> > >     System.out.println("Input i: "+i);
> > > }
> > > ```
> >
> > ##### Java异常的捕捉
> >
> > > ```java
> > > try{//可能会抛出异常的语句}
> > > 
> > > catch(XXException e){  //...}
> > > 
> > > catch(XXException e){  //...}
> > > 
> > > //多态特性：如果捕捉到子类的异常会进入父类的catch，因此父类的catch永远不能出现在子类前面
> > > finally{ do sth;}
> > >     		  //
> > >     		  //永远会执行，除非...
> > >               //finally语句块中抛出异常且未处理
> > >               //前面的代码使用了System.exit()语句
> > >               //程序所在线程死亡
> > >               //CPU出现异常被关闭
> > > ```



## JAVA 文件操作



#### Java File 类

> `import java.io.File ;` 用来与操作系统交互,实现各种文件操作（删除，重命名等）
>
> 构造方法:  File file =new File(String pathName)
>
> 为什么要有字符流而不全用字节流：字符流转换为字节流也需要消耗资源
>
> > ##### 字节流：
> >
> > > `InputStream`
> > >
> > > ```java
> > >         FileInputStream fin = new FileInputStream(srcFile);
> > >         FileOutputStream fout = new FileOutputStream(destFile);
> > >         byte[] bytes = new byte[1024];
> > >         while (fin.read(bytes) != -1) {
> > >             fout.write(bytes);
> > >             fout.flush();
> > >         }
> > >         fin.close();
> > >         fout.close();
> > > ```
> > >
> > > `BufferedInputStream`
> > >
> > > ```java
> > >         BufferedInputStream bis = new BufferedInputStream(new FileInputStream(srcFile));
> > >         BufferedOutputStream bos = new BufferedOutputStream(new FileOutputStream(destFile));
> > >         byte[] bytes = new byte[16];
> > >         int size = 0;
> > >         while ((size = bis.read(bytes)) >= 0) {
> > >             bos.write(bytes, 0, size);
> > >             bos.flush();
> > >         }
> > >         bis.close();
> > >         bos.close();
> > > ```
>
> > ##### 字符流
> >
> > > 字节流转字符流，编码可指定，默认为平台默认字符码
> > >
> > > 构造方法：字节流，编码
> > >
> > > ```java
> > >         InputStreamReader isr= new InputStreamReader(new FileInputStream(srcFile));
> > >         OutputStreamWriter osw= new OutputStreamWriter (new FileOutputStream(destFile), "GBK");
> > >         char[] bytes = new char[16];
> > >         int size = 0;
> > >         while ((size = isr.read(bytes)) >= 0) {
> > >             osw.write(bytes, 0, size);
> > >             osw.flush();
> > >         }
> > >         isr.close();
> > >         osw.close();
> > > ```
> > >
> > > `FileWriter/FileReader`
> > >
> > > ```java
> > >         FileReader fr = new FileReader(srcFile);
> > >         FileWriter fw = new FileWriter(destFile);
> > >         char buf [] = new char [16];
> > >         int len = 0;
> > >         while((len = fr.read(buf)) >= 0 ){
> > >             fw.write(buf, 0, len);
> > >         }
> > >         fr.close();
> > >         fw.close();
> > > ```
> > >
> > > `BufferedReader/BufferedWriter`：带缓冲的写法
> > >
> > > ```java
> > >         BufferedReader bufReader = new BufferedReader(new InputStreamReader(new FileInputStream(srcFile)));
> > >         BufferedWriter bufWriter = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(destFile)));
> > >         String input = null;
> > >         while ((input = bufReader.readLine()) != null) {
> > >             bufWriter.write(input);
> > >             bufWriter.newLine();
> > >         }
> > >         bufReader.close();
> > >         bufWriter.close();
> > > ```



## Date类

> `import java.util.Date`
>
> `import java.text.SimpleDateFormat `
>
> ```java
> Date date = new Date();//获取当前时间
> 
> long time = date.getTime();//获取从GMT 1970-01-01 00：00至今的毫秒数
> 
> SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss") //设置时间格式
>     //HH代表24小时 hh代表12小时
> 
> String sysTime = df.format(date) //Date -> String
> ```



## Calendar类

> `import java.util.Calendar;`
>
> `Calendar c = Calendar.getInstance();`：获取当前时间，静态方法？
>
> ```java
> int year = c.get(Calendar.YEAR); 
> int month = c.get(Calendar.MONTH);  
> ```
>
> 
>
> ```java
> Calendar c = Calendar.getInstance();
> 
> Date time = c.getTime();//Calendar -> Date
> c.setTime(time);//Date -> Calendar
> 
> String str="2021-04-30";
> SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
> Date date = sdf.parse(str);//字符串格式的时间转换为Date对象
> 
> 冷知识：
> c.get(Calendar.MONTH);//注意这里Calendar的月份是按照0-11算的！！
> ```



## Timer 定时器

> 使用场景：程序的周期性工作
>
> ```java
> import java.util.Timer;
> import java.util.TimerTask;
> ```
>
> ```java
>         Timer timer = new Timer();
>         timer.schedule(new TimerTask(){
>             @Override
>             public void run() {
>                  //todo:
>             }
>         },200,100);
>     }
> timer.cancel();
> ```
>
> TimerTask为抽象类，需要实现run方法



## Log 日志

> 使用单例模式，注意并发



## 线程同步 

> 根据变量同步
>
> > 加锁(synchronized)
> >
> > ```java
> > Object a = new Object();
> > synchronized(a){
> > //临界区
> > }
> > 
> > ```
> >
> > 线程的执行过程
> >
> > ```java
> > class MyRunnable implements Runnable{
> > @OverRide
> > public void run(){
> >    System.out.println("Hello World!");
> > }
> > }
> > 
> > public static void main(String[] args){
> >  Thread myThread new Thread(new MyRunnable());
> >  myThread.start();
> > }
> > ```
> >
> > 用户线程+LWP的实现
> >
> > > - 线程管理在用户空间内完成
> > > - 系统调用由LWP完成，降低进程被完全阻塞的风险
> >
> > 线程在Java的实现：
> >
> > > 在JDK1.2之前:Green Threads：由用户线程机制实现
> > >
> > > 目前无规定：Linux/Windows：LWP 1:1的机制实现
> >
> > 线程调度：
> >
> > > 协同式
> > >
> > > 抢占式
> > >
> > > > - 优先级
> > > > - 时间片
> > >
> > > 操作系统调度函数：Round Robin,LSF,FIFO
> >
> > 线程的状态（JVM中有7中状态，不同于操作系统中的五种线程状态）
> >
> > > 初始态
> > >
> > > 就绪态
> > >
> > > 可运行态
> > >
> > > 阻塞态
> > >
> > > 无限期等待
> > >
> > > 有限等待
> > >
> > > 死亡态



## 线程池

> 基于池化思想管理线程的工具
>
> 减少处理任务时创建销毁线程开销的代价
>
> 避免线程数量膨胀导致的过分调度问题



### ThreadPoolExecutor

> ![image-20211109202917406](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20211109202917406.png)
> 线程池主要组成部分：任务队列和工作线程管理
>
> ```java
>  public static ExecutorService newCachedThreadPool()
>  public static ExecutorService newFixedThreadPool(int nThreads)
>  public static ExecutorService newSingleThreadExecutor()
>  public static ScheduledExecutorService newScheduledThreadPool(int corePoolSize)
> ```
>
> 
>
> [详细介绍](https://tech101.cn/2019/10/02/ThreadPoolExecutor%E5%AE%9E%E7%8E%B0%E5%8E%9F%E7%90%86)
>
> ![image-20211109215726371](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20211109215726371.png)




## 集合类

![image-20211023161128437](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20211023161128437.png)

##### **Map接口**(HashMap/TreeMap)：

> > 管理键值对：每个值对应一个全局唯一的键
> >
> > 以空间换时间：通过键查找，可以比通过值查找快
> >
> > Map为泛型接口:interface Map<K,V>
>
> 
>
> HashMap（线程不安全）和HashTable的区别：
>
> > > HashMap继承自AbstractMap类
> > >
> > > HashTable继承自Dictionary类
> > >
> > > 不过都实现了Map,Clonable,Serializable这三个接口
>

##### HashMap的底层实现原理：

> > Hash值的计算：
> >
> > Hash值=（hashcode）^(hashcode >>> 16)
> >
> > 数组下标:**hash&(16-1) = hash%16**
> >
> > Hash冲突：单线链表到达一定长度后效率会很低，jdk1.8后，加入了红黑树，单线表到达一定长度后会变成一个红黑树(单线表长度>8,数组长度>64，若不大于64，则需要扩容)
> >
> > 底层扩容原理：
> >
> > > 数组长度变成2倍0.75
> > >
> > > 触发条件：负载因子>0.75
>
> **put**: 
>
> 1. 当桶数组 table 为空时，通过扩容的方式初始化 table
> 2. 查找要插入的键值对是否已经存在，存在的话根据条件判断是否用新值替换旧值OnlyIfAbsent（仅为NULL时才替换）
> 3. 如果不存在，则将键值对链入链表中，并根据链表长度决定是否将链表转为红黑树
> 4. 判断键值对数量是否大于阈值，大于的话则进行扩容操作
>
> **get**:计算hashcode，然后定位到具体的桶中
>
> **resize()**:
>
> 1. 计算新桶数组的容量 newCap 和新阈值 newThr
> 2. 根据计算出的 newCap 创建新的桶数组，桶数组 table 也是在这里进行初始化的
> 3. 将键值对节点重新映射到新的桶数组里。如果节点是 TreeNode 类型，则需要拆分红黑树。如果是普通节点，则节点按原顺序进行分组。
>
> 
>
> jdk1.8:红黑树效率提高到logn,HashEntry 改为 Node
>
> 
>
> 遍历方式：
>
> > EntrySet：高效
> >
> > KeySet：低效
>
> 
>
> **并发问题**： HashMap 扩容的时候会调用 `resize()` 方法，就是这里的并发操作容易在一个桶上形成环形链表；这样当获取一个不存在的 key 时，计算出的 index 正好是环形链表的下标就会出现死循环。

##### ConcurrentHashMap

> jdk1.7
>
> > Segment+HashEntry(value和链表是由volatile修饰的)
> >
> > 但是volatile不能保证操作的原子性，Put操作还是需要加锁（以Segment为单位）
> >
> > get由于可见性则不需要加锁
>
> jdk1.8
>
> > 其中抛弃了原有的 Segment 分段锁，而采用了 `CAS + synchronized` 来保证并发安全性。



**源码分析**：

> **size()**：counterBase+counterCells(counterCell[])
>
> **初始化**：put操作中检查，initTable
>
> ```java
> 
>  private final Node<K,V>[] initTable() {
>      Node<K,V>[] tab; int sc;
>      while ((tab = table) == null || tab.length == 0) {
>          if ((sc = sizeCtl) < 0)
>              Thread.yield(); // lost initialization race; just spin
>          else if (U.compareAndSwapInt(this, SIZECTL, sc, -1)) {
>              try {
>                  if ((tab = table) == null || tab.length == 0) {
>                      int n = (sc > 0) ? sc : DEFAULT_CAPACITY;
>                      @SuppressWarnings("unchecked")
>                      Node<K,V>[] nt = (Node<K,V>[])new Node<?,?>[n];
>                      table = tab = nt;
>                      sc = n - (n >>> 2);
>                  }
>              } finally {
>                  sizeCtl = sc;
>              }
>              break;
>          }
>      }
>      return tab;
>  }
> 
> 
> ```
>
> `private transient volatile sizeCTL`：如果为负数則説明有其他线程在进行初始化操作，保证线程并发安全，没有获取的线程自选来尝试获得该变量，所以获得的线程需要修改`sizeCTL`值来使其他线程走出自旋进行接下来的操作。这里出初始化采用了单例中的两重检验方式
>
> `get`：volatile关键字实现可见性，Node节点中的数据添加关键字volatile
>
> `put`：更新记录数量：addCount()实现，需要考虑是否超过阈值而进行扩容，baseCount当前记录数量，如果table过小，则需要进行扩容，首先找到对应的index，如果头节点为空，`CAS`更新，若不为空且有线程在进行扩容，帮助扩容helptransfer()，否则synchronized锁住头节点，进行类似HashMap的put操作
>
> `addCount`：table中元素个数达到容器阈值sizeCtl时，进行扩容，put方法最后调用`addCount(long x,int check)`：计算baseCount、检测是否需要扩容操作
>
> `扩容过程`：
>
> 	1. 单线程下构建一个nextTable，大小为原来两倍
> 	2. 多线程transfer将原数组的内容复制到nextTable中
>
> `size计算`:baseCount + CounterCells[0...n-1].value，如果两个线程同时执行CAS修改baseCount值，则失败的线程会执行方法体中的逻辑，使用CounterCell记录元素个数的变化：元素个数保存在baseCount中，部分元素的变化个数保存在CounterCell数组中

> 
>
> > 为什么抛弃分段锁：
> >
> > > - ReentrantLock占用内存(Sync继承了AQS)
> > > - 很少会发生hash冲突，锁粒度太大
> > > - JVM优化



##### **Collection接口**(Set/List)：

> > 如何方便支持不同类型的类的对象集：
> >
> > > 如String对象集，Integer对象集
> > >
> > > 泛型接口：interface Collection <E>
> >
> > 支持的操作：
> >
> > > add,remove,contains,isempty,size...
> > >
> > > addAll：把另一个集合所有数据加进来
> > >
> > > toArray：转换为数组
> > >
> > > iterator：返回一个Iterator对象，用于遍历数据
> >
> > 基于index的次序管理



##### **Collection/Set**

> - **HashSet**(Collection/Set/)：
>
> > 通过hashCode值来确定元素在内存中的位置
> >
> > 方便快速查找元素，基于HashMap实现，元素唯一性的实现：通过元素的两个方法`hashCode`  和`equals`完成，如果两个元素a和b的hashCode()返回值->判断a.equals(b)，实际上是判断引用是否一样
>
> - **TreeSet**(Collection/Set)：
>
> > 有序的Set，使用RB树来存储元素
>
> > 大小关系：通过元素的compareTo方法来获得，因此元素需要实现Comparable接口
>



##### Collection/Queue

> 两类操作，是否抛出异常
>
> add/offer
>
> remove/poll
>
> element/peek



##### Collection/List

> - ArrayList 能自动增长容量的数组 toArray：返回数组 asList：返回列表
>
>   继承自AbstractList, add remove都要调用System.arraycopy()
>
> - LinkedList 双链表：继承自AbstractSquentialList，查询、删除等操作for遍历，性能比ArrayList差
>
> - Vector线程安全  
>
>   >Stack{empty/peek/pop/push/search}
>
> 线程安全的list:



## 数组

> 数组也是对象
>
> 特性：如果有两个类A和B，如果B继承（extends）了A，那么A[]类型的引用就可以指向B[]类型的对象
>
> ```java
> String[][] ss = new String[2][3];
>      System.out.println(ss.getClass().getName());
>      //打印出的数组类的名字为    [[Ljava.lang.String;
> ```
>
> [[代表维度
>
> 数组怎么转化为List类型：
>
> > -  Arrays.asList( T[]):返回的类型为java.util.Arrays.ArrayList静态内部类，不支持add()
> > - 将上面的静态内部类转化为java.util.ArrayList
> > - Collections.addAll() 高效，支持增删
> >
> > java泛型类型为引用,所以int[]数组转换后会变为List<int[]>
> >
> > ```java
> > Arrays.asList( int[] ) -> List<int [] >
> > ```
> >


## JAVA 8



### Lambda

> 实现匿名内部类的方式，接口名函数名一同省掉，前提是必须有相应的函数接口(内部只有一个抽象方法的接口)

### Stream

> 数组创建流：`Arrays.stream()`或者`Stream.of()`
>
> 集合创建流：`.stream()`

##### 操作流：

> - filter()：接收一个Predicate接口filter(element -> element.contains("...")
>
> - forEach()：接收一个Consumer接口，参数类型为:类名::方法名
>
> - map()：把一个流中的元素转化为新的流中的元素，接收一个Function接口，参数类型为T，返回结果为R，map(String::length)
>
> - 匹配:接收一个Predicate接口，同filter()
>
>   anyMatch()
>
>   allMatch()
>
>   noneMatch()
>
> - reduce()：把Stream中的元素组合起来，主要有两种用法：
>
>   Optional<T> reduce(BinaryOperator<T> accumulator)
>
>   T reduce(T identity, BinaryOperator<T> accumulator)
>
> - collect()：转换流

