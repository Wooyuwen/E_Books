#!/usr/bin/env python
# coding: utf-8

import socket
from threading import Thread
import time
import os

# 服务器IP地址和端口号
# IP = "0.0.0.0"
IP = "127.0.0.1"
PORT = 9999

# 最大连接次数，便于调试，也可用于限制用户数量
_MAX_CONN_ = 1000

# 服务端socket
server_socket = None

#服务器文件目录
filepath = "e:/server/"

# 管理 socket 连接列表，元素：socket_connection
socket_lists = list()
# 管理客户端，形式：{socket_connection.fileno(), username}
# socket_connection.fileno是用户连接的文件描述符
CLIENTS = dict()

Files = list()
Files.append("file1.txt")
Files.append("file2.txt")
Files.append("file3.txt")

# 头部字段
_HEADER_NAME_ = "00000"  # user send name of the client
_HEADER_UMSG_ = "00001"  # user's get files request
_HEADER_GFile_ = "00010"  # user's get files content
_HEADER_PMSG_ = "00011"  # user's put files request
_HEADER_ChangePath_ = "00100"  # user change the server's file path
_HEADER_SearchPath_ = "10100"  # user search the server's file path
_HEADER_UCLO_ = "00111"  # user close connection
_HEADER_OMSG_ = "01000"  # other users' message
_HEADER_UMSGB_ = "01001"  # user message back
_HEADER_ENTER_ = "01010"  # user enter the file system
_HEADER_EXIT_ = "01011"   # user leave the file system
_HEADER_USERS_ = "01100"  # users in the file system
_HEADER_TIME_ = "01101"  # time of the file system
_HEADER_SCLO_ = "01111"  # server close connection


# 启动服务器
def startServer():
    global server_socket
    global socket_lists
    print("Server is starting")
    # 创建套接字socket
    try:
        server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    except:
        print("Failed to cereate socket")
        return

    # 端口重用，解决端口冲突问题
    server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    # 绑定端口
    server_socket.bind((IP, PORT))
    # 监听连接请求
    server_socket.listen(_MAX_CONN_)

    print("Server", socket.gethostbyname("localhost"), "listening...")

    for _ in range(_MAX_CONN_):
        try:
            socket_connection, address = server_socket.accept()  # 等待连接，此处自动阻塞
        except:
            # 服务器关闭了，就不再接受请求
            # print("Failed to accept new connection")
            break
        print("New connection", socket_connection.getpeername(),
              "fileno:", socket_connection.fileno())

        # 存储该socket连接
        socket_lists.append(socket_connection)

        # 每当有新的连接过来，自动创建一个新的线程，
        # 并将 连接对象 和 访问者的文件描述符 作为参数传递给线程的执行函数

        t = Thread(target=recvMsg, args=(
            socket_connection, socket_connection.fileno()))
        t.start()
    return


# 关闭服务器
def closeServer():
    sendToAll("Dell Inspiration", _HEADER_SCLO_)

    if server_socket is not None:
        server_socket.close()
    print("Server is closed")
    return


# 将消息发送给指定单一用户
def sendToOne(myFileno, msg, HEADER):
    msg = HEADER + msg
    for socket_connection in socket_lists:
        try:
            fileno = socket_connection.fileno()
        except:
            continue
        if fileno == myFileno:
            try:
                socket_connection.sendall(msg.encode("utf-8"))
            except:
                # 逻辑上不会输出这一条，即断开的连接都会被移除
                print("Send error in sendToOne, maybe the connection is closed")
            return
    return


# 将消息发给其他用户
def sendToOthers(myFileno, msg, HEADER):
    msg = HEADER + msg
    for socket_connection in socket_lists:
        try:
            fileno = socket_connection.fileno()
        except:
            continue
        if fileno != myFileno:
            try:
                socket_connection.send(msg.encode())
            except:
                # 逻辑上不会输出这一条，即断开的连接都会被移除
                print("Send error in sendToOthers, maybe one connection is closed")
                continue
    return


# 将消息发给所有用户
def sendToAll(msg, HEADER):
    msg = HEADER + msg
    for socket_connection in socket_lists:
        try:
            socket_connection.send(msg.encode())
        except:
            print("Send error in sendToAll, maybe one connection is closed")
            continue
    return

# 接收消息并处理
def recvMsg(socket_connection, fileno):
    global CLIENTS
    while True:
        try:
            msg = socket_connection.recv(1024)
        except:
            break

        # 解码
        msg = msg.decode("utf-8")

        if not msg or len(msg) < 5:
            continue

        HEADER = msg[0: 5]
        msg = msg[5:]

        # 收到用户发来的用户名并保存
        if HEADER == _HEADER_NAME_:
            # 判断用户名不能为空，为空则跳出循环，断开连接
            if msg == "":
                break

            sendMsgUsers = ""
            for user in CLIENTS.values():
                sendMsgUsers = sendMsgUsers + user + "\\"

            sendMsgUsers += msg
            sendToOne(fileno, sendMsgUsers, _HEADER_USERS_)

            # 进行映射存储
            CLIENTS[fileno] = msg
            print("This username is", msg)

            sendToAll(getLocalTime(), _HEADER_TIME_)
            sendToAll(CLIENTS[fileno], _HEADER_ENTER_)

        # 下载文件请求，并且配上时间
        elif HEADER == _HEADER_UMSG_:
            sendToAll(getLocalTime(), _HEADER_TIME_)
            
            global filepath
            filename = None
            sendMsg = None
            if len(msg.split(".")[1])<5:#用户没有自定义文件存储位置
                filename = filepath+str(msg)
                sendMsg = "Client "+CLIENTS[fileno] + " has downloaded " + msg + " ... \n"
            else:
                filename=filepath+msg.split("@")[0]#用户指定了文件存储位置
                sendMsg= "Client "+CLIENTS[fileno] + " has downloaded " + msg.split("@")[0] + " ... \n"

            if os.path.isfile(filename):  # 判断文件存在
                # 1.先发送文件大小，让客户端准备接收
                size = os.stat(filename).st_size  # 获取文件大小
                sendToOne(fileno, str(size)+"#"+str(msg), _HEADER_GFile_)#发送数据长度和文件名和可能的自定义文件路径
                #socket_connection.send(str(size).encode("utf-8"))  # 发送数据长度
                sendMsg = sendMsg+" filesize = "+str(size)+" bytes \n"

                socket_connection.recv(1024)#接收确认

                # 2.发送文件内容
                #socket_connection.recv(8192)  # 接收确认

                f = open(filename, "rb")
                for line in f:
                    socket_connection.send(line)  # 发送数据
                f.close()
                # 加上用户名并发送


            sendToOne(fileno, sendMsg, _HEADER_UMSGB_)
            sendToOthers(fileno, sendMsg, _HEADER_OMSG_)
        
        #客户端发来文件
        elif HEADER == _HEADER_PMSG_:
            sendToAll(getLocalTime(), _HEADER_TIME_)
            #接收长度
            client_response=socket_connection.recv(1024)
            file_size=int(client_response.decode("utf-8"))

            putMsg = None #将要输出到系统消息界面的信息

            filename = None #将要保存到本地的文件名

            if len(msg.split(".")[1])<5:
               putMsg= "Client "+CLIENTS[fileno] + " has uploaded " + msg + " ... \n"
               #创建要从客户端下载的文件名
               filename =str(filepath)+msg
            
            else:
               putMsg= "Client "+CLIENTS[fileno] + " has uploaded " + msg.split("@")[0] + " ... \n"
               filename =str(filepath)+msg.split("@")[0]

            #准备好接收
            socket_connection.send("1".encode("utf-8"))

            f = open(filename, "wb")
            received_size = 0

            #发送文件
            while received_size < file_size:
                size = 0  # 准确接收数据大小，解决粘包
                if file_size - received_size > 8192: # 多次接收
                    size = 8192
                else:  # 最后一次接收完毕
                    size = file_size - received_size

                data = socket_connection.recv(size)  # 多次接收内容，接收大数据
                data_len = len(data)
                received_size += data_len
                details="服务器已接收："+str(int(received_size/file_size*100))+"%"+"\n"
                sendToOne(fileno, details, _HEADER_UMSGB_)
                sendToOthers(fileno, details, _HEADER_OMSG_)
                f.write(data)

            f.close()
           
            sendToOne(fileno, putMsg, _HEADER_UMSGB_)
            sendToOthers(fileno, putMsg, _HEADER_OMSG_)
                
        # 断开连接
        elif HEADER == _HEADER_UCLO_:
            break

        else:
            # 非法信息，断开连接
            print("Received error message: " + HEADER + msg)
            break

    # 接收不到消息，说明需要断开连接
    closeSocket(socket_connection, fileno)
    return


# 关闭一个socket连接
def closeSocket(socket_connection, fileno):
    sendToOthers(fileno, getLocalTime(), _HEADER_TIME_)
    sendToOthers(fileno, CLIENTS[fileno], _HEADER_EXIT_)
    try:
        socket_connection.close()
    except:
        # 服务器关闭失败就代表连接已断开
        pass
    print("End connection, fileno:", fileno)

    # 清除用户数据
    try:
        socket_lists.remove(socket_connection)
    except:
        pass
    try:
        CLIENTS.pop(fileno)
    except:
        pass
    return


# 获得本地时间，时间戳
def getLocalTime():
    t = time.localtime()
    h = str(t.tm_hour).zfill(2)
    m = str(t.tm_min).zfill(2)
    s = str(t.tm_sec).zfill(2)
    return str(h) + ":" + str(m) + ":" + str(s)

# 监听键盘，让某个用户下线，需输入端口号

def getKeyboard():
    print("Input q to close server, input port number to offline one user.\n")
    _input_ = "0"
    while _input_ is not "q":
        # 关闭服务器
        _input_ = str(input())
        if _input_ == "q":
            closeServer()
        # 过滤掉不是端口号的输入
        elif _input_ == "" or len(_input_) > 5 or not str.isdecimal(_input_):
            continue
        # 踢出用户
        else:
            for socket_connection in socket_lists:
                if str(socket_connection.fileno()) == _input_:
                    socket_connection.close()
    return


if __name__ == "__main__":
    socket_lists = list()
    CLIENTS = dict()

    # 创建一个线程用来监听键盘输入
    t = Thread(target=getKeyboard)
    t.start()

    # 启动服务器
    startServer()
    # closeServer()
