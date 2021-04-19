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

class OpenCommand : public Command{
    public :
       OpenCommand(Application*);
       virtual void Execute();
    protected:
       Application* _application;
       char* _response;
};

OpenCommand::OpenCommand(Application* a){
    _application =a;
}

void OpenCommand::Execute(){
    const char* name=AskUser();
    if(name!=0){
        Document* document = new Document(name);
        _application->Add(document);
        document->Open();
    }
}

class Document{
    private:
     string text;
    public :
     Document(string name);
     void Open(){};    
};