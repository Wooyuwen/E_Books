#include<iostream>
#include<cstddef>
#include<string>
#include<fstream>
#include<sstream>
#include<string>
#include<queue>
#include<stack>
#include<cstdio>
#include"Graph.h"
#include"Function_Part1.h"
#include"Function_Part2.h"
#include"Function_Part3.h"
using namespace std;

void function(Graph<string, int>&A)     //���ܺ���
{
	int n;
	cout << "����������Ҫѡ��ķ�����:";
	cin >> n;
	switch (n)
	{
	case 1: {
		int num; cout << "��������Ҫ��ѯ�ľ���ı�ţ�"; cin >> num;//��ѯ��ؾ������Ϣ
		A.GetVertexInfo(num); cout << endl;//��������Function_Part1.h 73��
		function(A);
		break;
	}
	case 2: {
		cout << "����������Ҫ��ѯ����������ı��:";
		int v1, v2; cin >> v1 >> v2;
		cout << "���·��Ϊ:";
		A.FindPath(v1, v2);//��������Function_Part2.h 20��
		cout << "�ܾ���Ϊ:" << d[v2].w << "m" << endl;
		function(A);
		break;
	}
	case 3: {
		cout << "����������Ҫ��ѯ�ĸ���������ı��:";
		int v1, v2; cin >> v1 >> v2;
		cout << "����·��Ϊ:"<<endl;
		A.Find_All_Path(v1, v2); cout << endl;//��������Function_Part2.h 72��
		function(A);
		break;
	}
	case 4: {
		cout << "����������Ҫ��ӵľ�������ƺ���Ϣ:";
		string name, info; cin >> name >> info;
		int vn = A.GetVerticesNum();
		string cd = "code";
		A.InsertVertex(vn-1, cd, name, info); cout << endl;//��������Function_Part3.h 9��
		ofstream outfile("vertex.txt", ios::app);//���¾���д��vertex.txt�ļ�
		outfile << vn-1<<"  "<< name << " " << info << endl;
		outfile.close();
		ofstream outfile1("direction.txt", ios::app);
		outfile1 << vn-1 << name;
		outfile1.close();
		cout << "����ӵľ����Ѿ�д��ϵͳ" << endl;
		function(A);
		break;
	}
	case 5: {
		cout << "����������Ҫɾ���ľ���ı�ţ�";
		int num; cin >> num;
		A.RemoveVertex(num); cout << endl;//��������Function_Part3.h 60��
		cout << "ɾ���ɹ�" << endl;
		function(A);
		break;
	}
	case 6: {
		cout << "����������Ҫ��ӵ�·���������Ӧ������������ı���Լ�·����Ӧ���ȣ�:";
		int v1, v2,weight;
		cin >> v1 >> v2 >>weight;
		A.InsertEdge(v1, v2, weight); cout << endl;//��������Function_Part3.h 17��
		ofstream outfile("edge.txt", ios::app);//���¾���д��edge.txt�ļ�
		outfile << A.Getname(v1)<< "  " << A.Getname(v2) << " " << weight<< endl;
		outfile.close();
		cout << "д��ɹ�" << endl;
		function(A);
		break;
	}
	case 7: {
		cout << "����������Ҫɾ����·���������Ӧ������������ı�ţ�:";
		int v1, v2; cin >> v1 >> v2;
		A.RemoveEdge(v1, v2); cout << endl;//��������Function_Part3.h 79��
		cout << "ɾ���ɹ�" << endl;
		function(A);
		break;
	}
	case 8: {
		cout << "����������Ҫ�޸ĵľ���ı�ţ�";
		int num; cin >> num;
		A.ChangeVertexInfo(num);//��������Function_Part3.h 32��
		cout << "���޸ĺ�ľ�����Ϣ:";
		A.GetVertexInfo(num); cout << endl;
		function(A);
		break;
	}
	case 9: {
		cout << "����������Ҫ�޸ĵ�·���������Ӧ������������ı�ţ�:";
		int v1, v2; cin >> v1 >> v2;
		A.ChangeEdgeInfo(v1, v2);//��������Function_Part3.h 41��
		cout << "���޸ĺ��·����Ϣ:" << endl;
		A.GetEdgeInfo(v1, v2);
		function(A);
		break;
	}
	case 10: {//�˳�ʹ��
		cout << "       ��лʹ��!      " << endl;
		break;                  
	}
	default: {
		cout << "������������Ч������������" << endl;
		function(A);
		break;
	}
	}
}

int main(){
	Graph<string, int> A(31);
	ifstream in("direction.txt");//����û�ʹ��ָ��
	string line;
	while (getline(in, line)) {
		stringstream ss(line);
		string fo;
		while (ss >> fo)
			cout << fo << endl;
	}
	function(A);//ִ�й���
	system("pause");
	return 0;
}
