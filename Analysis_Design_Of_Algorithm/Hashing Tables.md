##### 11.1 Direct-address tables

##### 11.2 Hash Tables

> ###### Collision resolution by chaining

***
##### 11.3 Hashing Functions

###### a good hash function contains：

> - `hashing by division /`
>
> - `hashing by multiplication/`
>
> - `universal hashing`(use randomization to provide provably good performance)

#### scheme: simple uniform hashing
> universal hashing: `H` be a finite collection of hash functions that map a given universe ` U` of keys into the range` {0,1,...,m-1}`.Such a collection is said to be `universal` if for each pair of distinct keys `k,l `belongs to` U`, the number of hash functions h belongs to `H `for which` h(k)=h(l)` is at most `|H|/m`.

##### **Theorem 11.3**

> universal functions `h`: if k not in the table, the expected length `E[ n h(k)]`of the list that key `k` hashes to is at most the load factor `α=n/m.` if key in table ++

***

##### 11.4 Open Addressing

> each table contains either an element of the dynamic set or NIL.
> fewer collisions and faster retrieval.
> 查找一个元素可以按照该元素插入的顺序，遇到NIL即可停止，这也要求DEL时要设置被删slot为deleted状态。

###### Uniform Hashing 一致散列
> the probe sequence of each key is equally likely to be any of the m! permutations of <0,1,..,m-1>.
> `<h(k,0),h(k,1),...,h(k,m-1)>` is a permutation of `<0,1,...,m-1>`.
> three method :
> > Linear probing
> > Quadratic probing
> > **Double hashing**  :`h(k,i)=(h1(k) + ih2(k))modm` both `h1` and `h2` are auxiliary hash functions 
> > **`hints:`h2(k)  must be relatively prime to the hash-table size m for the entire hash table to be searched** 
> > **一致散列哈希** 
> >
> > > upper bound 不成功查找:`1/(1-α)` 成功查找:`1/αln(1/(1-α))`


