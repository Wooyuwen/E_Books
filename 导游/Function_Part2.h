#ifndef BASICPART_H_
#define BASICPART_H_
#include "Graph.h"
#include<cstddef>
#include<string>
#include<cstdio>
#include<queue>
#include<stack>


using namespace std;
#define maxn 100
#define LL 9999

int parent[maxn];       //每个顶点的父节点，用于还原最短路上各个顶点
bool visited[maxn];     //用于判断顶点是否已经在最短路径中，或已找到最短路径
node d[maxn];          //源点到每个顶点的估算距离，最后结果为源点到所有顶点的最短路
priority_queue<node> q; //优先级队列

template <class InfoType, class ValType>
void Graph<InfoType, ValType>::FindPath(int v1, int v2)//采用Dijkstra算法
{
	int n = GetVerticesNum();
	for (int i = 0; i < n; i++)
	{
		d[i].id = i;
		d[i].w = LL;           //权值上限
		parent[i] = -1;        //每个顶点都没有父节点
		visited[i] = false;   //未找到最短路
	}
	d[v1].w = 0;       //源点到源点的最短路径为1
	q.push(d[v1]);
	while (!q.empty())
	{
		node cd = q.top();
		q.pop();
		int u = cd.id;
		if (visited[u])
			continue;
		visited[u] = true;  //顶点u加入已求得最短路顶点集中
		EdgeNode<InfoType,ValType>* p = NodeTable[u].adj;
		while (p != NULL)
		{
			int v = p->dest;
			if (!visited[v] && d[v].w > d[u].w + p->cost)
			{
				d[v].w = d[u].w + p->cost;
				parent[v] = u;
				q.push(d[v]);
			}
			p = p->link;
		}
	}
	stack<int> path;
	int pl = v2;
	while (pl != v1)
	{
		path.push(pl);
		pl = parent[pl];
	}
	cout << NodeTable[v1].name << "--";
	while (!path.empty())
	{
		int temp;
		temp = path.top();
		path.pop();
		cout << NodeTable[temp].name << "--";
	}
}

int graph[30][30];

template <class InfoType, class ValType>      //查询两个景点之间所有的路径，采用DFS算法
void Graph<InfoType, ValType>::Find_All_Path(int v1,int v2 ) {
	int n = GetVerticesNum();
	int Visited[maxn],stack[maxn],top=0;
	for (int t = 0; t < n; t++)
		Visited[t] = 0;
	for (int i = 0; i < n; i++)
	{
		int j = 0;
		EdgeNode<InfoType, ValType>* p = NodeTable[i].adj;
		while (p != NULL)
		{
			j = p->dest;
			graph[i][j] = p->cost;
			graph[j][i] = p->cost;
			p = p->link;
		}
	}
	DFS(v1, v2,top,stack,Visited);                                                                                                                                                                                        
}

template <class InfoType, class ValType>      //查询两个景点之间所有的路径，采用DFS算法
void Graph<InfoType, ValType>::DFS(int v1,int v2,int& top,int* stack,int* Visited) {
	if (v1 == v2) {
		for (int i = 0; i < top; i++)
			cout << NodeTable[stack[i]].name << "-->";
		cout << NodeTable[v2].name << endl;
		return;
	}
	Visited[v1] = 1;
	stack[top++] = v1;
		int nm = GetVerticesNum();
		for (int w = 1; w < nm; w++) {
			if (!Visited[w] && graph[v1][w])
				DFS(w, v2,top,stack,Visited);
		}
	Visited[v1] = 0;
	top--;
}

#endif
