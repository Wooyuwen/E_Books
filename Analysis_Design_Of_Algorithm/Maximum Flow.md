##  Maximum Flow



#### 1 Flow networks

> flow network：有向图（无对称边，无自环），每条边有非负容量大小c，两点之间没有边时c=0
>
> 图G中的flow：f: V x V -> R
>
> - f(u,v)<c(u,v)
> - 每个点（除s,t）进出值相同
>
> |f|：s点f差值

##### `Lemma26.1`

> |f ⬆ f ‘|=|f|+|f ‘|



#### Augmenting paths

> (v,u)和图Gf中其他连接的边组成的路径
>
> C f(p)=min{Cf (u,v) : (u,v) is on p }



##### `Lemma26.2`

> fp：VxV->R by
>
> fp(u,v)=Cf(p)  if (u,v) is on p
>
> ​					   else 0



##### `Corollary26.3`

> ![image-20210530141906147](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210530141906147.png)

when we stop : the residual network contains no augmenting path

`maximum-flow minimum-cut`



##### `Lemma26.4`

> ![image-20210530142543726](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210530142543726.png)



##### `Lemma26.5`

> ![image-20210530144626637](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210530144626637.png)



##### `Lemma26.6`

> ![image-20210530144552612](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210530144552612.png)



### Maximum bipartite matching

> ![image-20210530193708726](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210530193708726.png)



> ![image-20210530195418432](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210530195418432.png)



> ![image-20210530195348636](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210530195348636.png)
