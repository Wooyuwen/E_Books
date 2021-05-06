# Java__BaseConcepts

> - JRE: java运行环境，为`Java`的运行提供了所需的环境。它是一个JVM程序，主要包括了JVM的标准实现和一些`Java`基本类库
> - JDK:`Java`开发工具包，提供了`Java`的开发以及运行环境



#### java的加载和运行

> ###### 编译：`.java`文件->多个 `.class`文件（字节码文件）[常量池？？静态常量池]
>
> 运行：java.exe在DOS窗口使用
>
> - 启动JVM
> - JVM启动类装载器`ClassLoader`
> - `ClassLoader`会去硬盘上搜索`.class`文件
> - JVM将字节码文件 **解释** 成二进制文件并运行。
>
> `classpath`：能让ClassLoader去指定路径下载字节码文件，在用户环境变量中设置



#### 标识符、关键字、字面值（数据）

> 驼峰命名法
>
> 标识符只能由：`数字`   `字母` `下划线` `$` 组成
>
> 字面值：整数型、浮点型、布尔型、字符串型、字符型
>
> #### 修饰符关键字
>
> > ##### `final`: （类，方法，变量）
> >
> > - 类  ：不能继承，类中的成员变量可以设计为final，final中的成员方法会被隐式指定为final方法。
> > - 方法：不能被重写，一个类的private方法会被隐式的指定为final方法。
> > - 成员变量：必须要附初始值（`直接赋值` `全部在构造方法中赋值`），只能初始化一次。**如果修饰的是引用类型，则说明引用的地址的值不能修改，但是引用的对象的值可变**。
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
> >   
> >
> > - 静态变量并没有像c++的作用域，作用域的限制只能通过private/public/protected来修饰。
> >
> > - **静态方法和静态变量都可以通过对象访问,因为静态方法不依赖具体实例，所以必须实现。**
>
> > **`synchronized`**:（**方法**）
> >
> > - 同一时间只能被一个线程访问，可以搭配四个访问修饰符使用。
>
> > **`volatile`**：（**成员变量**）
> >
> > - 所修饰的成员变量每次被线程访问时，都强制从共享内存中重新读取该成员变量的值；
> > - 当成员变化时，会强制线程将变化后的值写回内存。
>
> > ###### **` protected`**:
> >
> > - 和private一样不能修饰类，同一包内所有类都可以访问，但是不同包只能通过继承，而且只能调用继承的方法，而不能使用父类实例的`protected`方法。
> >
> > 访问控制：
> >
> > - 父类方法声明为public，子类public
> > - 父类protected方法，子类public 或protected
> > - 父类private方法，子类.................子类没有XD





####  变量、数据类型、精度损失

> `boolean `
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
> 整数字面值没有超出byte,short,char的取值范围时可以直接复制该字面值，方便程序员的编程



#### 三元运算符

> 布尔表达式?表达式1：表达式2



***



####  运行时内存分析

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
> >   
> >   
> >   `native`:用作java和其他语言进行协同作用，通知操作系统，该函数由操作系统实现
>
> java完全采用动态内存分配方式，每创建新对象都使用new关键字来构建此对象的动态实例。




****





# 面向对象（封装、继承、多态）


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
> - 抽象类中的抽象方法只是声明，不包含方法体
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

> **`普通内部类`**:
>
> > - 内部类可使用外部类的变量和方法
> > - 外部类可以创建内部类实例
> > - 作用域关键字用法相同
> > - 需要外部类**实例**创建内部类对象 
> > - `不能定义静态变量、能定义常量`
>
> **`静态内部类`**(用static修饰)
>
> > - 只能访问外部类的static方法和变量
> > - 可以直接创建，不需要外部类引用
>
> **`局部内部类`**
>
> > - 定义在程序块中，只在块内有效；块外不能创建和引用
> > - 只能用abstract和final修饰
> > - 可以访问外部类成员
> > - 可访问块中的final局部变量
>
> **`匿名内部类`**
>
> > - 没有引用名的对象
> >
> > ```java
> > new Test().show();
> > ```
> >
> > - 匿名类：继承父类或者实现接口
> >
> >   ```java
> >   new Test(){
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
> 可以有内部接口、没有局部接口、可以用匿名类实现接口
>
> java 1.8允许接口实现方法 `default`
>
> 接口可以extends其他接口，一个类可以实现多个接口
>
> 接口的私有方法：方便接口内复用



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
> **静态方法不能使用类的泛型**（泛型类作为参数）



#### Java泛型接口、泛型方法

> 静态方法支持泛型方法

**没有泛型对象数组**

JAVA泛型只在编译时处理



常用Java泛型类:

ArrayList 视为变长数组



## Object 方法

### public `boolean equals()`  

> ```java
>     @Override
>     public boolean equals(Object o) {
>         if (this == o) return true;
>         if (o == null || getClass() != o.getClass()) return false;
> 
>         EqualExample that = (EqualExample) o;
> 
>         if (x != that.x) return false;
>         if (y != that.y) return false;
>         return z == that.z;
>     }
> }
> ```
>
> 等价包含相等
>
> 引用相等指二者指向同一个对象，若不是同一个对象，但是成员变量相同——等价



### public native `int hashCode()`

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



### public string toString()

> 默认返回 ClassName@442142c的形式，@后数值为散列的无符号十六进制表示



### protected native `Object clone()` throws CloneNotSupportedException

> 一个类不显示重写clone()，其他类就不能直接调用该实例的clone()方法
>
> ```java
> public class CloneExample {
>     private int a;
>     private int b;
> 
>     @Override
>     public CloneExample clone() throws CloneNotSupportedException {
>         return (CloneExample)super.clone();
>     }
> }
> ```
>
> ```java
> CloneExample e1 = new CloneExample();
> try {
>     CloneExample e2 = e1.clone();
> } catch (CloneNotSupportedException e) {
>     e.printStackTrace();
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



## 异常

> java.lang.Throwable可以用来表示任何可以作为异常抛出的类，分为两种`Error`和`Exception`



### Error （可以不检查）

> 其中`Error`用来表示JVM无法处理的错误：脱离程序员控制的，例如栈溢出，编译时检查不到
>
> `VirtualMachineError`
>
> > `StackOverFlowError`
> >
> > `OutOfMemoryError`
>
> `AWTError`



### Exception

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
> > > finally{ do sth;}//永远会执行
> > >                  //finally语句块中抛出异常且未处理
> > >                  //前面的代码使用了System.exit()语句
> > >                  //程序所在线程死亡
> > >                  //CPU出现异常被关闭
> > > ```



## JAVA 文件操作



### Java File 类

> `import java.io.File ;` 用来与操作系统交互,实现各种文件操作（删除，重命名等）
>
> 构造方法:  File file =new File(String pathName)
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

