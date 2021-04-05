## 12  Binary Search Trees

> #### Querying a binary search tree
>>`Theorem 12.2` O(h) for` Minimum`,`Maximum`,`Successor`,`Predecessor` and `Search` 
>> 调用`Minimum`后，连续调用`k-1`次`Successor`花费时间为`O(n)`

### Insertion and Deletion
> #### Insertion
>> <img src = "img\2">

> > Run Time :`O(h)`

> **Transplant(T,u,v)**
>
> #### Deletion 
>
> <img src="img\1">
>
> > Run Time :`O(h)`
>
> **Theorem 12.3** we can implement the dynamic-set operations Insertion and Deletion in `O(h)`time

### Randomly built binary search trees
> **Theorem 12.4** The expected height of a randomly built binary search tree on n distinct keys is `O(lgn)`
> 

