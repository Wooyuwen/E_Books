## Minimum Spanning Trees(最小生成树)



`Theorem 23.1`：G（V,E）是一个连通的无向图，A是G中最小生成树的子集（边集）

（S,V-S）是G中不妨害A的任意一个割，(u,v)是横跨(S,V-S)的一条轻边，则(u,v)对A是安全的

> #### Kruskal's algorithm

运用引理23.2

> #### Prim's algorithm



using binary heaps :`O(ElgV)`

using Fibonacci heaps:  Prim's  access`O(E+VlgV)`  |V|<<|E|

