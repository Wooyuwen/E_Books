## Java__Base

### java的加载和运行

> 编译：`.java`文件->多个 `.class`文件（字节码文件）[常量池？？静态常量池]
>
> 运行：java.exe在DOS窗口使用
>
> - 启动JVM
> - JVM启动类装载器`ClassLoader`
> - `ClassLoader`会去硬盘上搜索`.class`文件
> - JVM将字节码文件 **解释** 成二进制文件并运行。
>
> `classpath`：能让ClassLoader去指定路径下载字节码文件，在用户环境变量中设置



***



#### 注释 好的开发习惯：多写注释

> 只生成在源文件中，不存在于class文件中

> **单行注释** ：`//`
>
> **多行注释**: `/* \n\n\n\n   */`
>
> **javadoc多行注释**:`/** \n*\n*\n*\n*\n */`：会被工具解析提取并生成文档



#### Public class

> public class不是必须的
>
> .java文件名称必须和公开类名称相同
>
> 只能有一个public class
>
> 每一个class都可以编写main方法 `public static void main(Stirng args[])`



#### 标识符、字面值（数据）

> 驼峰命名法
>
> 标识符只能由：`数字`   `字母` `下划线` `$` 组成
>
> 字面值：整数型、浮点型、布尔型、字符串型、字符型



####  数据类型、精度损失

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