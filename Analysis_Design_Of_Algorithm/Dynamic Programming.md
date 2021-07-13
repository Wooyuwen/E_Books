# 动态规划



#### Four Steps:

> - Characterize the structure of an optimal solution
> - Recursively define the value of an optimal solution
> - Compute the value of an optimal solution, typically in a bottom-up fashion
> - Construct an optimal solution from computed information

#### 15.1 Rod cutting

> 切棒子
>
> TOP-DOWN：2^n
>
> top-down with memorization
>
> bottom-up method



#### 15.2 矩阵链乘法

> matrix-chain
>
> ```cpp
> PRINT-OPTIMAL-PARENS(s,i,j)
> if i==j
>    print "A";
> else Print "("
>      Print-Optimal-Parens(s,i,s[i,j])
>      Print-Optimal-Parens(s,s[i,j]+1,j)
>      print ")"
> ```
>



#### 15.3 Elements of Dynamic Programming

> ##### Optimal Substructures
>
> > characterize the optimal substructures
>
> ```java
> RECURSIVE-MATRIX-CHAIN(p,i,j)
> if i==j
>     return 0
>  m[i,j]=infinite
>  for k=i to j-1
>      q= RECURSIVE-MATRIX-CHAIN(p,i,j)+
>         RECURSIVE-MATRIX-CHAIN(p,k+1,j)
>         + pi-1pkpj
> ```
>
> `指数时间`
>
> 自底向上
>
> ```java
> n=p.length-1
> let m[1..n,1..n]and s[1..n-1,2..n]be new tables
> for i=1 to n
>     m[i,i]=0
> for l=2 to n
>     for i=1 to n-l+1
>         j=i+l-1
>         m[i,j]=infinitive
>         for k=i to j-1
>             q=m[i,k]+m[k+1,j]+pi-1pkpj
>             if q<m[i,j]
>                m[i,j]=q
>                s[i,j]=k
> return s and m
> ```
>
> `theta(n3)`

> `Optimal substructure`
>
> `Overlapping subproblems`
>
> 寻找最优子结构的套路：
>
> > - 寻找一个切入点
> > - 假设方法已经给了你
> > - 根据所给方法，来决定子问题的选择
>
> 选择子结构选择方式的不同：
>
> > - 原问题需要用多少个子问题
> > - 我们有多少中方法确定一个问题中的子问题



#### 15.4 最长公共子序列（LCS）

> 可以不连续
>
> #### `Step1`Characterizing a longest common subsequence
>
> > 前缀 X4 <A B C B> OF <A B C B D A B>

> ##### Theorem 15.1(Optimal substructure of an LCS) LCS的最优子结构
>
> > 设X=<x1,x2,x3,x4,..,xm>和Y=<y1,y2,y3,..,yn>为两个序列，并设Z=<z1,z2,..,zk>为X和Y的任意一个LCS
> >
> > 1 如果xm=yn，那么zk=xm=yn，且Zk-1时Xm-1和Yn-1的一个LCS
> >
> > 2 如果xm!=yn，那么zk!=xm蕴含Z是Xm-1和Y的一个LCS
> >
> > 3 如果xm!=yn，那么zk!=yn蕴含Z是X和Yn-1的一个LCS
>
> #### `Step2` A recursive solution
>
> > c[i,j]=0                    if i=0 or j=0
> >
> > ​       =c[i-1,j-1]+1    if i,j>0 and xi=yi,
> >
> > ​         max(c[i,j-1],c[i-1,j]) if i,j>0 and xi!=yj
> >
> > 子问题的选择会根据条件而改变，和rod cutting和matrix mutilacation不同
>
> #### `Step 3`Computing the length of an LCS
>
> > table c :store length of LCS
> >
> > table b: store pointers
>
> #### `Step 4`Constructing an LCS
>
> > ```
> > Print-LCS(b,X,i,j)
> > if i==0 or j==0
> >    return
> > if b[i,j]=="左上的箭头"
> >     Print-LCS(b,X,i-1,j-1)
> >     print xi
> > elseif b[i,j]=="向上的箭头"
> >     Print-LCS(b,X,i-1,j)
> >     else Print-LCS(b,X,i,j-1)
> > ```
> >
> > #### Improving the code
> >
> > > safely change the code and improve efficiency



### 15.5 最优二叉查找树(Oprimal binary search trees)

> - 最优二叉树结构：最优二叉树的子树依然是最优二叉树，一个空子树依然有假键值di-1
>
>   含有键值ki,..,kj的optimal trees含有假键值di-1,di,...,dj(i>=1,j<=n,j>=i-1)
>
> - 递归方法：递归的找到最优二叉搜索树的方法，最后解得结果为e[1,n]
>   - 当j=i-1时，我们仅有假键值di-1，搜索代价为e[i,i-1]=qi-1；
>   - 当j>=i时，我们需在ki,..,kj中要找到根kr
>
> - 计算最优二叉查找树的代价
>
> - ```java
>   Optimal-Bst
>   for i = 1 to n+1
>       e[i,i-1]=qi-1
>       w[i,i-1]=qi-1
>   for l=1 to n
>       for i=1 to n-l+1
>           j=i+l-1
>           e[i,j]=infinitive
>           w[i,j]=w[i,j-1]+pj+qj
>           for r=i to j
>               t= e[i,r-1]+e[r+1,j]+w[i,j]
>               if t<e[i,j]
>                  e[i,j]=t
>                  root[i,j]=r
>    return e and root
>   ```
>
>   ######   **时间复杂度**:`theta（n^3）`
>
>   

