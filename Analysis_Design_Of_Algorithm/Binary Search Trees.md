## 12  Binary Search Trees

> #### Querying a binary search tree
>>`Theorem 12.2` O(h) for` Minimum`,`Maximum`,`Successor`,`Predecessor` and `Search` 
>> 调用`Minimum`后，连续调用`k-1`次`Successor`花费时间为`O(n)`

### Insertion and Deletion
> #### Insertion
>> <img src = "img\1.png">

> > Run Time :`O(h)`

> **Transplant(T,u,v)**
>
> #### Deletion 
>
> <img src="img\2.png">
>
> > Run Time :`O(h)`
>
> **Theorem 12.3** we can implement the dynamic-set operations Insertion and Deletion in `O(h)`time

### Randomly built binary search trees
> **Theorem 12.4** The expected height of a randomly built binary search tree on n distinct keys is `O(lgn)`



TIPS：

堆：Build_Max_Heap时间复杂度为O(n)，为什么不是nlgn呢？ Max_Heapify：要求左右子树都为最大堆~

优先级队列：最大优先级队列:插入，返回最大值，提取最大值，更新键值。

