#ifndef FUNCTION_Part1_h_
#define FUNCTION_Part1_h_
#include "Graph.h"
#include<cstddef>
#include<string>
#include<fstream>
#include<sstream>
using namespace std;

template <class InfoType, class ValType>
Graph<InfoType, ValType>::Graph(int size) : VerticesNum(0), VerticesMaxNum(DefaultSize), EdgesNum(0)
{ //构造函数
	ValType  e, k, j;
	InfoType tail, head;
	ValType weight;
	NodeTable = new VertexNode<InfoType, ValType>[VerticesMaxNum]; //创建顶点表数组
	if (NodeTable == NULL)
	{
		cerr << " error" << endl;
		exit(1);
	}
	for (int i = 0; i < VerticesMaxNum; i++) {
		NodeTable[i].adj = NULL;
	}
	ifstream fin("vertex.txt", ios::in);
	//for (ValType i = 0; i < size; i++)
	int i = 0;
	while(fin)
		{
			string code;
			string  name, info;
			fin>> code >> name >> info;
			InsertVertex(i, code, name, info);
			i++;
		}
	ifstream din("edge.txt", ios::in);
	din >> e; //输入边数
	for (int i = 0; i < e; i++)
		{
			din >> tail >> head >>weight;
			//依次输入边的两个端点与权值信息
			k = GetVertexPos(tail);
			j = GetVertexPos(head);  //获取一条边两个端点的位置
			InsertEdge(k, j,weight); //插入一条边
		}
}
//***************************************************************************************

template <class InfoType, class ValType>
Graph<InfoType, ValType>::~Graph()
{ //析构函数
	for (ValType i = 0; i < VerticesNum; i++)
	{ //删除各边链表中的顶点
		EdgeNode <InfoType, ValType> *p = NodeTable[i].adj;
		while (p != NULL)
		{ //循环删除
			NodeTable[i].adj = p->link;
			delete p;
			p = NodeTable[i].adj;
		}
	}
		delete[] NodeTable; //释放顶点表数组空间
}
//****************************************************************************************

template <class InfoType, class ValType>
int Graph<InfoType, ValType>::GetVertexPos(const InfoType &vertex)
{ //给出顶点vertex在图中的位置
	for (ValType i = 0; i < VerticesNum; i++)
		if (NodeTable[i].name == vertex)
			return i;
	return -1;
}
//****************************************************************************************

template <class InfoType, class ValType>
void Graph<InfoType, ValType>::GetVertexInfo(int v1)
{
	cout << NodeTable[v1].name << ":";
    cout << NodeTable[v1].info << " " << endl;
}
//****************************************************************************************

template <class InfoType, class ValType> 
void Graph<InfoType, ValType>::GetEdgeInfo(int v1, int v2)
{ //获取以v1与v2为两个端点的一条边的权值
    //若该边不存在于图中则返回权值0
    if (v1 != -1 && v2 != -1)
    {
        EdgeNode<InfoType,ValType> *p = NodeTable[v1].adj;
        //边链表头指针
        while (p != NULL)
			if (p->dest == v2)
			{
				cout << "--" << p->cost << "m" << "--"; return;//找到对应边，输出信息
			}
            else
				if(p->link == NULL && p->dest != v2)
		        {
				cout << "很抱歉,查找失败,不存在该路径" << endl; return;
			    }
	            else
                p = p->link; //否则找下一条边
    }
}
//****************************************************************************************

template <class InfoType, class ValType>
int Graph<InfoType, ValType>::first_neighbor(int v)
{ //给出顶点v的第一个邻接顶点的位置
    //如果不存在则返回-1
    if (v != -1)
    { //v存在
        EdgeNode<InfoType, ValType> *p = NodeTable[v].adj;
        //边链表头指针
        if (p != NULL)
            return p->dest;
        //若第一个邻接顶点存在，则返回该边的另一个顶点
    }
    return -1; //若不存在，则返回-1
}
//****************************************************************************************

template <class InfoType, class ValType>
int Graph<InfoType, ValType>::next_neighbor(int v1, int v2)
{ //给出顶点v1的某邻接顶点v2的下一个邻接顶点的位置
    //若没有下一个邻接顶点则返回-1
    if (v1 != -1)
    { //v1存在
        EdgeNode<InfoType, ValType> *p = NodeTable[v1].adj;
        //边链表头指针
        while (p != NULL)
        { //寻找某邻接顶点v2
            if (p->dest == v2 && p->link != NULL)
                return p->link->dest;
            else
                p = p->link;
        }
    }
    return -1;
}

#endif