### Single-Source Shortest Paths

问题变形

> Single-destination shortest-paths problem

> Single-pair shortest-path problem（Single-Source将s换成u）

> All-pairs shortest-paths problem



---



引理`Lemma 24.1` ： Subpaths of shortest paths are shortest paths



Properties of shortest paths and relaxation

> - Triangle inequality 三角不等式
> - Upper-bound property：`v.d>=min(s,v)`恒成立
> - Convergence property：收敛性 
> - Path-relaxation property
> - Predecessor-subgraph property



#### 24.1 The Bellman-Ford algorithm (O(VE))

> 解决在含有负权值的边的最短路径问题
>
> 返回值：是否有从源点s出发能到达的负边环
>
> ```java
> BELLMAN-FORD(G,w,s)
> 1    INITIALIZE-SINGLE-SOURCE(G,s)
> 2    for i=1 to |G.V|-1
> 3        for each edge(u,v)∈G.E
> 4            RELAX ·(u,v,w)·
> 5    for each edge(u,v)∈G.E
> 6        if v.d>u.d+w(u,v)
> 7            return FALSE
> 8    return TRUE
> ```



`Lemma 24.2`：假设G没有从s出发能到达的负边环，在|V|-1次循环（2-4行）后，v.d=min(s,v)对所有s能到达的点v都成立



推论 24.3 图G没有negative-weight cycles that are reachable from s，则对于V的所有点v，s到v有路径当且仅当BELLMAN-FORD terminates with `v.d<∞`



`Theorem 24.4`： Correctness of the Bellman-Ford algorithm

> 运用六大性质



#### 24.2 Single-source shortest paths in directed acyclic graphs

> 时间复杂度O(V+E)
>
> ```java
> DAG-SHORTEST-PATHS(G,w,s)
> 1   topologically sort the vertices of G //O(V+E)
> 2   Initialize...
> 3   for each vertex u,taken in topologically sorted order
>         for each vertex v∈G.Adj[u]
>             RELAX(u,v,w)
> ```

`Theorem 24.5`：上面函数执行完后，v.d=min(s,v)对所有顶点都成立，前置子图是一个最短路径树



#### 24.3 Dijkstra's Algorithm O(VlgV+E)

> ```java
> Initialize...
>     S=空集
>     Q=G.V
>     while Q!=空集
>         u = EXTRACT-MIN(Q)
>         S = S∪{u}
>         for each vertex v∈G.Adj[u]
>             RELAX(u,v,w)
> ```



#### 24.4 Difference constraints and shortest paths

> ##### Linear programming
>
> > Systems of difference constraints

 `Lemma 24.8`： x=(x1,x2,x3,...,xn) is a solution to a system Ax<=b of difference constraints,let d be any constant. Then x+d=(x1+d,x2+d,...,xn+d)is a solution to Ax<=b as well



#### 24.5 Proofs of the shortest-paths properties

> 

