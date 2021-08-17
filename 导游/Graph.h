#ifndef GRAPH_H_
#define GRAPH_H_
#include<cstddef>
using namespace std;

//���ڽӱ��ʾ������ͼ�ĳ�����������
const int DefaultSize = 100;
template <class InfoType, class ValType> class Graph;
template <class InfoType,class ValType> class EdgeNode       //�߽����ඨ��
{ 
    template <class InfoType, class ValType>
    friend class Graph;
    int dest;                 //�ߵ���һ����λ��
	ValType cost;            //���ϵ�Ȩֵ�����ñ�·������
    EdgeNode<InfoType,ValType> *link; //�߽����ָ��
public:
    EdgeNode() {}                                                 //���캯��
    EdgeNode(int d,InfoType n,ValType c) : dest(d),cost(c), link(NULL) {} //���캯��
};
struct node
{
	template <class InfoType, class ValType>
	friend class Graph;
	int id;       //����ڵ㣬id��Դ�ڵ�Ĺ������,���ȼ�������Ҫ
	int w;
	friend bool operator<(node a, node b)//������С��
	{
		return a.w > b.w;
	}
};

template <class InfoType, class ValType>      //��������ඨ��
class VertexNode
{ 
    friend class EdgeNode<InfoType,ValType>;
    friend class Graph<InfoType, ValType>;
    InfoType code;           //������
    InfoType name;           //��������
    InfoType info;           //����ľ�����Ϣ
    EdgeNode<InfoType,ValType> *adj; //���߱��ͷָ��
};

template <class InfoType, class ValType>       //ͼ���ඨ��
class Graph
{
    
private:
    VertexNode<InfoType, ValType> *NodeTable; //��������������ͷ��㣩
    int VerticesNum;                           //��ǰ������
    int VerticesMaxNum;                        //��󶥵���
    int EdgesNum;                              //��ǰ����
    int GetVertexPos(const InfoType &vertex);

public:
	//Function Part 1 ��ѯ�йؾ������Ϣ
    Graph(int size);                                                //���캯��
    ~Graph();                                                       //��������
	int GetVerticesNum() { return VerticesNum; };
	string Getname(int v) { return NodeTable[v].name; };
    void GetVertexInfo(int v1);           //ȡ��Ӧ�������Ϣ
    void GetEdgeInfo(int v1, int v2); //ȡ�ߵ�Ȩֵ������ע
    int first_neighbor(int v);          //ȡ����v�ĵ�һ���ڽӶ���
    int next_neighbor(int v1, int v2);  //ȡ����v1�;���v2����һ�ڽӾ���

    //Function Part 2 ������������֮������·��
    void FindPath(int v1, int v2);      //�����������֮������·��
    void Find_All_Path(int v1, int v2); //�����������֮�����е�·��  
	void DFS(int v1, int v2,int& top, int*stack,int* Visited);//�������·���ĸ�������

    //Funtion Part 3 ���ӣ����£�ɾ���йض���͵�·����Ϣ
    void InsertVertex(int i,InfoType cd, InfoType nm, InfoType infom);        //��ͼ�в���һ���¶���
    void InsertEdge(int v1, int v2, ValType weight); //��ͼ�в���һ���±�
    void ChangeVertexInfo(int v1);                   //���¾�����Ϣ
    void ChangeEdgeInfo(int v1, int v2);             //����·����Ϣ
    void RemoveVertex(int v);                        //��ͼ��ɾ��һ������
    void RemoveEdge(int v1, int v2);                 //��ͼ��ɾ��һ����
};
#endif
