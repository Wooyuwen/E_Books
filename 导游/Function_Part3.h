#ifndef FUNCTION_PART3_H_
#define FUNCTION_PART3_H_
#include "Graph.h"
#include<cstddef>
#include<fstream>
using namespace std;

template <class InfoType, class ValType>        //��������
void Graph<InfoType, ValType>::InsertVertex(int i, InfoType cd,InfoType nm, InfoType infom)
{
	NodeTable[i].code = cd; NodeTable[i].name = nm; NodeTable[i].info = infom;
	VerticesNum++;
}
//****************************************************************************************

template <class InfoType, class ValType>         //����·��
void Graph<InfoType, ValType>::InsertEdge(int v1, int v2,ValType weight)
{
	EdgeNode <InfoType,ValType> *p = new EdgeNode<InfoType,ValType>; 
    EdgeNode <InfoType, ValType> *q = NodeTable[v1].adj;
	NodeTable[v1].adj = p;
	p->dest = v2; p->cost = weight; p->link = q;
	EdgeNode <InfoType, ValType> *p1 = new EdgeNode<InfoType, ValType>;
	EdgeNode <InfoType, ValType> *q1= NodeTable[v2].adj;
	NodeTable[v2].adj = p1;
	p1->dest = v1; p1->cost = weight; p1->link = q1;
	EdgesNum++;
}
//****************************************************************************************

template <class InfoType, class ValType>         //�޸ľ�����Ϣ
void Graph<InfoType, ValType>::ChangeVertexInfo(int v1)
{
	cout << "����������Ҫ�޸ĵľ�����Ϣ��������:" << endl;
	string change; cin >> change;
	NodeTable[v1].info = change;
}
//****************************************************************************************

template <class InfoType, class ValType>       //�޸�·������
void Graph<InfoType, ValType>::ChangeEdgeInfo(int v1, int v2)
{
		cout << "�������µ�·������:";
		ValType change; cin >> change; 
		if (v1 != -1 && v2 != -1)
	{
		EdgeNode<InfoType, ValType> *p = NodeTable[v1].adj;
		while (p != NULL)
			if (p->dest == v2)
			{
				p->cost = change; break;//�ҵ���Ӧ�ߣ��޸���Ϣ
			}
			else
				p = p->link; //��������һ����
	} 
}
//****************************************************************************************

template <class InfoType, class ValType>       //ɾ������
void Graph<InfoType, ValType>::RemoveVertex(int v1)
{
    if (v1 != -1)
    {
        EdgeNode<InfoType, ValType> *p = NodeTable[v1].adj;
        while (p != NULL)
        { //ѭ��ɾ��
            NodeTable[v1].adj = p->link;
            delete p;
            p = NodeTable[v1].adj;
        }
		NodeTable[v1].code = "to be writed";
		NodeTable[v1].name = "to be writed";
		NodeTable[v1].info = "to be writed";
    }
}
//****************************************************************************************

template <class InfoType, class ValType>      //ɾ��·��
void Graph<InfoType, ValType>::RemoveEdge(int v1, int v2)
{
    EdgeNode<InfoType,ValType>*p=NodeTable[v1].adj;
    if(p->dest==v2)
    {
        NodeTable[v1].adj=p->link;
        delete p;
    }
    else
    while(p!=NULL)
    {
		if (p->link != NULL && p->link->dest == v2)
		{
			EdgeNode<InfoType, ValType>*dl = p->link;
			p->link = dl->link;
			delete dl; break;
		}
		else
			p = p->link;
    }
	EdgeNode<InfoType, ValType>*p1 = NodeTable[v2].adj;
	if (p1->dest == v1)
	{
		NodeTable[v2].adj = p1->link;
		delete p1;
	}
	else
		while (p1 != NULL)
		{
			if (p1->link != NULL && p1->link->dest == v1)
			{
				EdgeNode<InfoType, ValType>*dl1 = p1->link;
				p1->link = dl1->link;
				delete dl1; break;
			}
			else
				p1 = p1->link;
		}
}

#endif