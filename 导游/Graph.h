#ifndef GRAPH_H_
#define GRAPH_H_
#include<cstddef>
using namespace std;

//用邻接表表示的无向图的抽象数据类型
const int DefaultSize = 100;
template <class InfoType, class ValType> class Graph;
template <class InfoType,class ValType> class EdgeNode       //边结点的类定义
{ 
    template <class InfoType, class ValType>
    friend class Graph;
    int dest;                 //边的另一顶点位置
	ValType cost;            //边上的权值，即该边路径长度
    EdgeNode<InfoType,ValType> *link; //边结点后继指针
public:
    EdgeNode() {}                                                 //构造函数
    EdgeNode(int d,InfoType n,ValType c) : dest(d),cost(c), link(NULL) {} //构造函数
};
struct node
{
	template <class InfoType, class ValType>
	friend class Graph;
	int id;       //顶点节点，id到源节点的估算距离,优先级队列需要
	int w;
	friend bool operator<(node a, node b)//利用最小堆
	{
		return a.w > b.w;
	}
};

template <class InfoType, class ValType>      //顶点结点的类定义
class VertexNode
{ 
    friend class EdgeNode<InfoType,ValType>;
    friend class Graph<InfoType, ValType>;
    InfoType code;           //景点编号
    InfoType name;           //景点名字
    InfoType info;           //景点的具体信息
    EdgeNode<InfoType,ValType> *adj; //出边表的头指针
};

template <class InfoType, class ValType>       //图的类定义
class Graph
{
    
private:
    VertexNode<InfoType, ValType> *NodeTable; //顶点表（各边链表的头结点）
    int VerticesNum;                           //当前顶点数
    int VerticesMaxNum;                        //最大顶点数
    int EdgesNum;                              //当前边数
    int GetVertexPos(const InfoType &vertex);

public:
	//Function Part 1 查询有关景点的信息
    Graph(int size);                                                //构造函数
    ~Graph();                                                       //析构函数
	int GetVerticesNum() { return VerticesNum; };
	string Getname(int v) { return NodeTable[v].name; };
    void GetVertexInfo(int v1);           //取对应景点的信息
    void GetEdgeInfo(int v1, int v2); //取边的权值，见备注
    int first_neighbor(int v);          //取顶点v的第一个邻接顶点
    int next_neighbor(int v1, int v2);  //取景点v1和景点v2的下一邻接景点

    //Function Part 2 查找两个景点之间的最短路径
    void FindPath(int v1, int v2);      //输出两个景点之间的最短路径
    void Find_All_Path(int v1, int v2); //输出两个景点之间所有的路径  
	void DFS(int v1, int v2,int& top, int*stack,int* Visited);//输出所有路径的辅助函数

    //Funtion Part 3 增加，更新，删除有关顶点和道路的信息
    void InsertVertex(int i,InfoType cd, InfoType nm, InfoType infom);        //在图中插入一个新顶点
    void InsertEdge(int v1, int v2, ValType weight); //在图中插入一条新边
    void ChangeVertexInfo(int v1);                   //更新景点信息
    void ChangeEdgeInfo(int v1, int v2);             //更新路径信息
    void RemoveVertex(int v);                        //在图中删除一个顶点
    void RemoveEdge(int v1, int v2);                 //在图中删除一条边
};
#endif
