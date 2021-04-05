## 12  Binary Search Trees

> #### Querying a binary search tree
>>`Theorem 12.2` O(h) for` Minimum`,`Maximum`,`Successor`,`Predecessor` and `Search` 
>> 调用`Minimum`后，连续调用`k-1`次`Successor`花费时间为`O(n)`

### Insertion and Deletion
> #### Insertion
>![1617606209037](C:\Users\Administrator\AppData\Roaming\Typora\typora-user-images\1617606209037.png)

> > Run Time :`O(h)`

> **Transplant(T,u,v)**
> #### Deletion 
>
> <img src="C:\Users\Administrator\AppData\Roaming\Typora\typora-user-images\1617616144934.png"/>
>
> > Run Time :`O(h)`
>
> **Theorem 12.3** we can implement the dynamic-set operations Insertion and Deletion in `O(h)`time

### Randomly built binary search trees
> **Theorem 12.4** The expected height of a randomly built binary search tree on n distinct keys is `O(lgn)`
> 

