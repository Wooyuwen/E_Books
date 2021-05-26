## 表达式与语句

> 表达式和语句是常规语言里描述计算过程的两个最基本层次，他们描述计算的执行顺序，实现语言的基本操作语义
>
> 表达式描述计算值的过程，常见控制手段是**优先级**、**括号**等
>
> 语句是命令，基本语句是程序里的基本动作。常规命令式语言中与数据有关的最基本动作主要的就是赋值
>
> 语句层控制提供一批控制结构，每种控制结构产生一种特定的计算流程
>
> > - 产生一些规范的计算序列
> > - 一些机制（如break,continue），用于改变规范的计算序列
>
> 程序员可以通过不同结构的组合应用，实现应用所需的特定控制流程
>
> 一些语言中，语句和表达式之间的界限模糊
>
> 表达式是描述计算的最基本手段
>
> 表达式是一种抽象，理解程序里表达式的意义，需要考虑两点：
>
> - 表达式实现的计算过程
> - 表达式的求值过程对运行环境的影响，如果没有影响，称为“引用透明”，否则有“副作用”，如C++中的a++
>
> 表达式表示形式：中前后缀
>
> 不同语言对于顺序点有不同的定义
>
> Java语言的表达式可能有副作用，定义：
>
> > - 运算对象或者参数总是从左到右逐个计算
> > - 每个计算的副作用将立即体现（每个计算动作之后都有顺序点）


