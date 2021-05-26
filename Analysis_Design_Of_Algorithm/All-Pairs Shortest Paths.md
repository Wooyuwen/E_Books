## All-Pairs Shortest Paths

#### Shortest paths and matrix multiplication
> a dynamic-programming algorithm each major loop of the dynamic program will invoke an operation that is very similar to matrix multiplication,started by developing a O(V4) , then improve it to O(V3lgV)
>
> ![image-20210522184931268](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210522184931268.png)
>
> 指具有m条边从i到j的最短路径长度，`wjj=0`
>
> 执行n-1次循环，改进：每次长度翻倍



#### Floyd's Algorithm


>时间复杂度：O(V3)
>
>![](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210525135604413.png)



#### Johnson's Algorithm

> 时间复杂度：O(V2lgV+VE)/O(VElgV)
>
> 首先使用bellman-ford算法判断是否有负环
>
> > 有：结束
> >
> > 没有：有没有负值边：有：reweighting，没有：直接用Dijskstra算法O(V2lgV+VE)
>
> 计算得w`![image-20210525142644987](C:\Users\wywfd\AppData\Roaming\Typora\typora-user-images\image-20210525142644987.png)
>
> > V中任意两个点：用w的路径和使用w`的路径相同
> >
> > w`中没有负边

