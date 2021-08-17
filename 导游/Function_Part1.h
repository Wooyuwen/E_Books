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
{ //���캯��
	ValType  e, k, j;
	InfoType tail, head;
	ValType weight;
	NodeTable = new VertexNode<InfoType, ValType>[VerticesMaxNum]; //�������������
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
	din >> e; //�������
	for (int i = 0; i < e; i++)
		{
			din >> tail >> head >>weight;
			//��������ߵ������˵���Ȩֵ��Ϣ
			k = GetVertexPos(tail);
			j = GetVertexPos(head);  //��ȡһ���������˵��λ��
			InsertEdge(k, j,weight); //����һ����
		}
}
//***************************************************************************************

template <class InfoType, class ValType>
Graph<InfoType, ValType>::~Graph()
{ //��������
	for (ValType i = 0; i < VerticesNum; i++)
	{ //ɾ�����������еĶ���
		EdgeNode <InfoType, ValType> *p = NodeTable[i].adj;
		while (p != NULL)
		{ //ѭ��ɾ��
			NodeTable[i].adj = p->link;
			delete p;
			p = NodeTable[i].adj;
		}
	}
		delete[] NodeTable; //�ͷŶ��������ռ�
}
//****************************************************************************************

template <class InfoType, class ValType>
int Graph<InfoType, ValType>::GetVertexPos(const InfoType &vertex)
{ //��������vertex��ͼ�е�λ��
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
{ //��ȡ��v1��v2Ϊ�����˵��һ���ߵ�Ȩֵ
    //���ñ߲�������ͼ���򷵻�Ȩֵ0
    if (v1 != -1 && v2 != -1)
    {
        EdgeNode<InfoType,ValType> *p = NodeTable[v1].adj;
        //������ͷָ��
        while (p != NULL)
			if (p->dest == v2)
			{
				cout << "--" << p->cost << "m" << "--"; return;//�ҵ���Ӧ�ߣ������Ϣ
			}
            else
				if(p->link == NULL && p->dest != v2)
		        {
				cout << "�ܱ�Ǹ,����ʧ��,�����ڸ�·��" << endl; return;
			    }
	            else
                p = p->link; //��������һ����
    }
}
//****************************************************************************************

template <class InfoType, class ValType>
int Graph<InfoType, ValType>::first_neighbor(int v)
{ //��������v�ĵ�һ���ڽӶ����λ��
    //����������򷵻�-1
    if (v != -1)
    { //v����
        EdgeNode<InfoType, ValType> *p = NodeTable[v].adj;
        //������ͷָ��
        if (p != NULL)
            return p->dest;
        //����һ���ڽӶ�����ڣ��򷵻ظñߵ���һ������
    }
    return -1; //�������ڣ��򷵻�-1
}
//****************************************************************************************

template <class InfoType, class ValType>
int Graph<InfoType, ValType>::next_neighbor(int v1, int v2)
{ //��������v1��ĳ�ڽӶ���v2����һ���ڽӶ����λ��
    //��û����һ���ڽӶ����򷵻�-1
    if (v1 != -1)
    { //v1����
        EdgeNode<InfoType, ValType> *p = NodeTable[v1].adj;
        //������ͷָ��
        while (p != NULL)
        { //Ѱ��ĳ�ڽӶ���v2
            if (p->dest == v2 && p->link != NULL)
                return p->link->dest;
            else
                p = p->link;
        }
    }
    return -1;
}

#endif