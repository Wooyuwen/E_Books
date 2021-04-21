/**
 * @file command.cpp
 * @author your name (you@domain.com)
 * @brief 
 * @version 0.1
 * @date 2021-04-19
 * 
 * @copyright Copyright (c) 2021
 * 
 */

#include<iostream>
using namespace std;

class Application{
    private:
      string name;
    public:
      void Add(Document* doc){};
};

class Command{
    public:
      virtual ~Command();
      virtual void Execute()=0;
    protected:
       Command();  
};



class Document{
    private:
     string text;
    public :
     Document(string name);
     void Open(){};    
};