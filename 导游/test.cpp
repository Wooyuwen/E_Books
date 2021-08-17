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

void function(Graph<string, int>&A)     //功能函数
{
	int n;
	cout << "请输入您想要选择的服务编号:";
	cin >> n;
	switch (n)
	{
	case 1: {
		int num; cout << "请输入想要查询的景点的编号："; cin >> num;//查询相关景点的信息
		A.GetVertexInfo(num); cout << endl;//函数定义Function_Part1.h 73行
		function(A);
		break;
	}
	case 2: {
		cout << "请输入您想要查询的两个景点的编号:";
		int v1, v2; cin >> v1 >> v2;
		cout << "最短路径为:";
		A.FindPath(v1, v2);//函数定义Function_Part2.h 20行
		cout << "总距离为:" << d[v2].w << "m" << endl;
		function(A);
		break;
	}
	case 3: {
		cout << "请输入您想要查询的该两个景点的编号:";
		int v1, v2; cin >> v1 >> v2;
		cout << "所有路径为:"<<endl;
		A.Find_All_Path(v1, v2); cout << endl;//函数定义Function_Part2.h 72行
		function(A);
		break;
	}
	case 4: {
		cout << "请输入您想要添加的景点的名称和信息:";
		string name, info; cin >> name >> info;
		int vn = A.GetVerticesNum();
		string cd = "code";
		A.InsertVertex(vn-1, cd, name, info); cout << endl;//函数定义Function_Part3.h 9行
		ofstream outfile("vertex.txt", ios::app);//将新景点写入vertex.txt文件
		outfile << vn-1<<"  "<< name << " " << info << endl;
		outfile.close();
		ofstream outfile1("direction.txt", ios::app);
		outfile1 << vn-1 << name;
		outfile1.close();
		cout << "新添加的景点已经写入系统" << endl;
		function(A);
		break;
	}
	case 5: {
		cout << "请输入您想要删除的景点的编号：";
		int num; cin >> num;
		A.RemoveVertex(num); cout << endl;//函数定义Function_Part3.h 60行
		cout << "删除成功" << endl;
		function(A);
		break;
	}
	case 6: {
		cout << "请输入您想要添加的路径（输入对应相邻两个景点的编号以及路径对应长度）:";
		int v1, v2,weight;
		cin >> v1 >> v2 >>weight;
		A.InsertEdge(v1, v2, weight); cout << endl;//函数定义Function_Part3.h 17行
		ofstream outfile("edge.txt", ios::app);//将新景点写入edge.txt文件
		outfile << A.Getname(v1)<< "  " << A.Getname(v2) << " " << weight<< endl;
		outfile.close();
		cout << "写入成功" << endl;
		function(A);
		break;
	}
	case 7: {
		cout << "请输入您想要删除的路径（输入对应相邻两个景点的编号）:";
		int v1, v2; cin >> v1 >> v2;
		A.RemoveEdge(v1, v2); cout << endl;//函数定义Function_Part3.h 79行
		cout << "删除成功" << endl;
		function(A);
		break;
	}
	case 8: {
		cout << "请输入您想要修改的景点的编号：";
		int num; cin >> num;
		A.ChangeVertexInfo(num);//函数定义Function_Part3.h 32行
		cout << "新修改后的景点信息:";
		A.GetVertexInfo(num); cout << endl;
		function(A);
		break;
	}
	case 9: {
		cout << "请输入您想要修改的路径（输入对应相邻两个景点的编号）:";
		int v1, v2; cin >> v1 >> v2;
		A.ChangeEdgeInfo(v1, v2);//函数定义Function_Part3.h 41行
		cout << "新修改后的路径信息:" << endl;
		A.GetEdgeInfo(v1, v2);
		function(A);
		break;
	}
	case 10: {//退出使用
		cout << "       感谢使用!      " << endl;
		break;                  
	}
	default: {
		cout << "输入服务序号无效，请重新输入" << endl;
		function(A);
		break;
	}
	}
}

int main(){
	Graph<string, int> A(31);
	ifstream in("direction.txt");//输出用户使用指南
	string line;
	while (getline(in, line)) {
		stringstream ss(line);
		string fo;
		while (ss >> fo)
			cout << fo << endl;
	}
	function(A);//执行功能
	system("pause");
	return 0;
}
