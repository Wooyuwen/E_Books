## Junit 

> JUnit是一个开源的Java语言的单元测试框架，专门针对Java设计，使用最广泛。JUnit是事实上的单元测试的标准框架，任何Java开发者都应当学习并使用JUnit编写单元测试。



```java
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.
    Assert.*;
```



> `@Test`：把带有其的方法识别为测试方法
>
> `@Disabled`:忽略此测试方法
>
> `@BeforeEach`:每一个测试方法之前运行//Junit5换成BeforeEach，下同
>
> `@AfterEach`:~~之后运行
>
> `@BeforeAll`:方法必须是静态，所有测试方法之前开始运行
>
> `@AfterAll`:~~之后开始运行
>
> beforeAll>>constructor>>beforeEach>>test<<afterEach<<afterAll



> ##### assertionEquals(expected,actual):
>可以给出期望值，在Assertion类中定义



> ##### assertTrue()/assertFalse()/assertNotNull()/...



### 注意

> - 所有测试方法用`@Test`
> - 测试方法必须使用public void进行修饰，不能带任何参数
> - 新建一个源代码目录来存放测试代码，与项目业务代码分开
> - 测试类所在的包名应该和被测试类所在的包名保持一致
> - 测试方法间不能有任何的依赖
> - 测试类使用Test作为类名的后缀（好习惯）
> - 测试方法使用test作为方法前缀(好习惯)



