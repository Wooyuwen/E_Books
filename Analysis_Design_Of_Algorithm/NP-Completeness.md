## NP-Completeness



P：多项式时间内可解的问题

NP：多项式时间内可验证的问题，P属于NP

NPC：一个问题属于NP，且与NP中的任何问题是一样“难的”



**intractable problems**: superpolynomial time

NP-complete:

>   longest paths/
>
>   hamiltonian cycle(visit each vertex once in a directed graph)
>
>   3-CNF(conjunctive normal form) satisfiability



### 34.1 多项式时间



多项式时间内可计算     多项式相关



![image-20210610115744264](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210610115744264.png)

 **引理 34.1** 设Q是定义在一个实例集I上的一个抽象判定问题，e1和e2是I上多项式相关的编码，则e1(Q)属于P当且仅当e2(Q)属于P

**定理34.2** P={L：L能被一个多项式时间的算法所接受}（接受不一定能判定，判定非黑即白）



### 34.2 多项式时间的验证



**哈密顿回路**

> HAM-CYCLE = {<G>:G是一个哈密顿图}



**验证算法**



### 34.3 NP完全性和可规约性（多用来证明语言属于P）

> 可规约性：语言L1在多项式时间内可以规约为语言L2，写作L1<=pL2，如果存在一个多项式时间可计算的函数f：{0,1}* ->{0,1}**，满足对所有的x属于{0,1}*，都有:
>
> ​					x ∈ L1 当且仅当 f (x) ∈ L2
>
> 则称函数f为规约函数（L1->L2），计算f的多项式时间算法F称为规约算法



**引理34.3**：如果L1、L2∈{0,1}*是满足L1<=pL2的语言，则L2∈P蕴含L1∈P



**NP完全性**

> 语言L∈{0，1}* 是NP完全的，如果：
>
> 1 L∈NP
>
> 2 每一个L‘ ∈NP，有L’ <= pL



**定理34.4**：如果任何NP完全问题是多项式时间可求解的，则P=NP。等

价的，如果NP中任何问题不是多项式时间可求解的，则所有NP完全问题都不是多项式时间可求解的。



如何证明一个语言是NPC的方法的基础

**引理34.8**：如果L是一种满足对某个L'∈NPC，有L'<=pL的语言，则L是NP难度的。此外，如果L∈NP，则L∈NPC。(对照NPC完全性定义)



**公式可满足性**（Formula satisfiability）

SAT语言描述：

> n个布尔变量x1,x2,...,xn；
>
> m个布尔连接词：与、或、非、蕴含或者是当且仅当；
>
> 括号

真值赋值：公式中各变量所取的一组值

可满足性赋值：使公式值为1的变量取值，有可满足性赋值的公式是可满足公式公式

SAT={<formula>:formula是一个可满足性公式}

定理34.9：布尔公式的可满足性问题是NPC

证明思路：1属于NP 2 找到一个NPC L'然后证明L‘ <= pL



**3-CNF可满足性**  NPC

布尔公式中的文字(literal)是指一个变量或者变量的“非”。如果一个公式可以表示为所有子句的“与”，且每个子句都是一个或者多个文字的或，称该布尔公式为合取范式，或者CNF(conjuntive normal form)，公式中每个子句恰好有三个不同的文字，则称该布尔公式为3合取范式，或3-CNF。

证明方法：仿照SAT证明方式



