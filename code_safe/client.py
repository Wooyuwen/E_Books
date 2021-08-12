#!/usr/bin/env python
# coding: utf-8

import socket
import threading
import tkinter as tk
import tkinter.messagebox
import os


# 服务器IP地址和端口号
IP = "127.0.0.1"
PORT = 9999

# socket连接
client_socket = None

# 在线用户列表
USERS = list()

# 本用户文件夹名
foldername = None

# 用户文件夹列表
client_filepath = list()

# 服务器文件夹
serverpath = "e:/server/"

# 主界面名称
GROUPNAME = "文件传输管理系统"

# 头部字段
_HEADER_NAME_ = "00000"
_HEADER_UMSG_ = "00001"  # user's get files request
_HEADER_GFile_ = "00010"  # user's get files content
_HEADER_PMSG_ = "00011"  # user's put files request
# _HEADER_ChangePath_ = "00100"  # user change the server's file path
# _HEADER_SearchPath_ = "10100"  # user search the server's file path
_HEADER_UCLO_ = "00111"  # user close connection
_HEADER_OMSG_ = "01000"  # other users' message
_HEADER_UMSGB_ = "01001"  # user message back
_HEADER_ENTER_ = "01010"
_HEADER_EXIT_ = "01011"
_HEADER_USERS_ = "01100"
_HEADER_TIME_ = "01101"
_HEADER_SCLO_ = "01111"  # server close connection

# 定义一些颜色
_COLOR_BLACK_ = "#282828"
_COLOR_GRAY_ = "#E8E8E8"
_COLOR_GRAY2_ = "#F5F5F5"
_COLOR_LEFT_ = "#CCCCCC"
_COLOR_GREEN_ = "#008000"
_COLOR_WHITE_ = "#FFFFFF"

# 连接服务器
def connectServer(ev=None):
    global client_socket
    # 如果当前存在连接，则不连接
    if client_socket is not None:
        # closeConnect()
        popup("警告", "请先退出登录", "warnning")
        return

    # 在连接服务器之前还需要判断用户输入的用户名不为空，除去前后空格，限制长度
    name = userName.get().strip()
    name = name[0: 20]
    global foldername
    #获取文件名
    foldername = str(name)
    #如果文件目录已经存在则添加默认路径至文件目录集，否则额外创建一个文件夹
    localpath="e:/client/"+str(foldername)+"/"
    client_filepath.append(localpath)
    #客户端默认文件夹不存在，则创建新的默认用户端文件夹
    if not os.path.isdir(localpath):
         create_folder(localpath)
    
    # 不能包含'\'
    if name == "" or ('\\' in name):
        popup("提示", "需要输入有效昵称（不能 为空 或 只有空格 或 包含\\）", "info")
        return

    try:
        # 创建套接字socket
        client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    except:
        popup("错误", "创建socket失败", "error")
        return

    try:
        # 连接服务器
        client_socket.connect((IP, PORT))
    except:
        client_socket = None
        popup("错误", "连接服务器失败", "error")
        return

    # 发送用户名
    if not sendToService(name, _HEADER_NAME_):
        return

    # 新建线程，用于接收请求消息
    t = threading.Thread(target=recvMsg)
    t.start()
    return


# 断开连接
def closeConnect(needClear=True):
    global client_socket
    if client_socket is not None:
        sendToService("Sorry, I lost, balabala...",
                      _HEADER_UCLO_, needPopup=False)#形同虚设
        try:
            client_socket.close()
        except:
            # print("Connection has been closed")
            pass

        client_socket = None
        global client_filepath
        client_filepath.clear()

    # 清除数据
    if needClear is True:
        clearAll()
    return


# 统一格式发送消息给服务器
def sendToService(msg, HEADER, needPopup=True):
    global client_socket
    if client_socket == None:
        if needPopup is True:
            popup("提示", "请先连接", "info")
        return False

    msg = HEADER + msg

    try:
        client_socket.send(msg.encode("utf-8"))
    except ConnectionAbortedError:
        if needPopup is True:
            popup("错误", "发送失败，请检查连接", "error")
        return False
    except ConnectionResetError:
        if needPopup is True:
            popup("错误", "发送失败，服务器已关闭", "error")
        return False
    except:
        if needPopup is True:
            popup("错误", "发送失败", "error")
        return False

    return True


# 与“下载”键绑定的方法
def sendMsg(ev=None):
    # 获取待发送消息
    msg = entryMsg.get('1.0', 'end').strip()
    msg = msg[0: 500]
    if not msg:  # or client_socket == None:
        return
    '''
    if len(msg.split("."))>5:#自定义文件存放
        if ((msg.split(".")).split("@")[1]).split():
            popup("警告","你输入的文件地址有误","warning")
    '''
    # 发送
    if not sendToService(msg, _HEADER_UMSG_):
        return

    # 清空待发消息
    entryMsg.delete('1.0', 'end')

    return


def putMsg(ev=None):
    # 获取待发送消息
    msg = entryMsg.get('1.0', 'end').strip()
    msg = msg[0: 500]
    if not msg:  # or client_socket == None:
        return

    # 发送
    if not sendToService(msg, _HEADER_PMSG_):
        return
    
    filename =None #要上传给服务器的文件名

    if len(msg.split(".")[1])<5:#传输默认文件夹中的文件
         filename = "e:/client/"+foldername+"/"+msg

    else:#上传指定目录的文件
         filename = msg.split("@")[1]+msg.split("@")[0]

    if(os.path.isfile(filename)):
        size = os.stat(str(filename)).st_size  # 准备发送给服务器要传输的文件size
        client_socket.sendall(str(size).encode("utf-8"))

        insertTextMsg("上传的文件大小: "+str(size)+" bytes \n", 'bluecolor')

        client_socket.recv(1024)  # 接收确认

        f = open(filename, "rb")
        for line in f:
            client_socket.send(line)  # 发送数据
        f.close()

    else:
        popup("警告","你输入的文件不存在","warning")

    # 清空待发消息
    # toSendMsg.set("")
    entryMsg.delete('1.0', 'end')
    updateFiles()
    updateUserFiles()

    return

# 接收消息

# 查询服务器文件夹的位置
def ShowDir(ev=None):
    global serverpath
    insertTextMsg("服务器文件所在目录："+str(serverpath)+"\n", 'redcolor')
    return


def recvMsg():
    while True:
        try:
            msg = client_socket.recv(1024)
        except:
            # 收不到就断开连接
            break

        # 解码
        msg = msg.decode("utf-8","ignore")

        # 处理接收的消息，返回值为True表示应该跳出循环
        if recvMsgHandle(msg) == True:
            break

    # 接收不到消息，说明已经断开了连接
    closeConnect()
    return


def create_folder(foldernames):
    isCreated = os.path.exists(foldernames)
    if not isCreated:
        os.makedirs(foldernames)
        return

    else:
        return


# 处理接收的消息
def recvMsgHandle(msg):
    global USERS
    if not msg or len(msg) < 5:
        return False

    HEADER = msg[0: 5]
    msg = msg[5:]


    # 从服务器接收文件并保存在本地文件夹
    if HEADER == _HEADER_GFile_:
        # 2.接收文件内容
        server_response = msg.split("#")[0]
        file_size = int(server_response)
        client_socket.send("1".encode("utf-8"))

        global foldername
        filename = None
        #用户下载的文件存于默认文件夹
        if len((msg.split("#")[1]).split(".")[1])<5:
         filename = "e:/client/"+str(foldername) + \
              "/"+(msg.split("#")[1])

        #用户自定义一个存储的文件夹，如果不存在则创建相应的文件夹
        else:
          selfdefined_filepath = (msg.split("#")[1]).split("@")[1]
          create_folder(selfdefined_filepath)
          filename = (msg.split("#")[1]).split(
              "@")[1]+(msg.split("#")[1]).split("@")[0]
          #添加新的文件夹路径
          #首先判断改文件路径是否已经存在于客户端的文件夹目录中
          isExist = False
          for i in client_filepath:
               if selfdefined_filepath == i:
                   isExist=True
               else : continue
          if not isExist:
              client_filepath.append(selfdefined_filepath)

        f = open(filename, "wb")
        received_size = 0

        # 发送文件
        while received_size < file_size:
            size = 0  # 准确接收数据大小，解决粘包
            if file_size - received_size > 8192:  # 多次接收
                size = 8192
            else:  # 最后一次接收完毕
                size = file_size - received_size

            data = client_socket.recv(size)  # 多次接收内容，接收大数据
            data_len = len(data)
            received_size += data_len
            details = "已接收："+str(int(received_size/file_size*100))+"%"+"\n"
            insertTextMsg(details, 'bluecolor')
            f.write(data)

        f.close()
        updateUserFiles()

    # 文件系统其它用户操作显示
    if HEADER == _HEADER_OMSG_:
        # 显示系统消息平面
        insertTextMsg(msg, 'bluecolor')

    # 自己发的请求消息
    elif HEADER == _HEADER_UMSGB_:
        # 显示到系统消息平面
        insertTextMsg(msg, 'greencolor')

    # 用户连接文件系统提示
    elif HEADER == _HEADER_ENTER_:
        # 新增的一个成员信息
        inUsers = False
        for user in USERS:
            if user == msg:
                inUsers = True
                break
        if inUsers == False:
            USERS.append(msg)
            updateFiles()
            updateUserFiles()

        # 显示到文件系统消息界面
        msg = '【系统提示：' + msg + '进入文件系统】\n'
        insertTextMsg(msg, 'redcolor')

    # 用户离开文件系统提示
    elif HEADER == _HEADER_EXIT_:
        # 删除的成员信息
        for user in USERS:
            if user == msg:
                USERS.remove(msg)
                break

        # 显示到文件系统
        msg = '【系统提示：' + msg + '离开文件系统】\n'
        insertTextMsg(msg, 'redcolor')

    # 完整的成员信息
    elif HEADER == _HEADER_USERS_:
        USERS_tmp = msg.split("\\")
        USERS = USERS + USERS_tmp
        updateFiles()
        updateUserFiles()

    # 时间戳
    elif HEADER == _HEADER_TIME_:
        if len(msg) > 8:
            msgNext = msg[8:]
            msg = msg[0: 8]
            insertTextMsg('\t\t    ' + msg + '\n', 'graycolor')
            recvMsgHandle(msgNext)
        else:
            insertTextMsg('\t\t    ' + msg + '\n', 'graycolor')

    # 断开连接
    elif HEADER == _HEADER_SCLO_:
        msg = '【服务器提示：' + msg + ' 断开您的连接】\n'
        insertTextMsg(msg, 'redcolor')
        closeConnect()
        return True

    else:
        # popup("错误", "无法识别收到的请求：" + HEADER + msg, "error")
        return

    return False


# 将各种信息输出到文件系统消息界面
def insertTextMsg(msg, textColor='bluecolor'):
    textMsg.config(state=tk.NORMAL)
    textMsg.insert(tk.END, msg, textColor)
    textMsg.config(state=tk.DISABLED)
    textMsg.see(tk.END)
    return


# 刷新服务器文件列表
def updateFiles():
    # 先删后加
    textFiles.config(state=tk.NORMAL)
    textFiles.delete('1.0', 'end')
    dirct = serverpath
    for i in os.listdir(dirct):
        textFiles.insert(tk.END, i + "\n")
    textFiles.config(state=tk.DISABLED)
    return

# 刷新服务器文件列表

#刷新客户端文件列表
def updateUserFiles():
    # 先删后加
    textUserFiles.config(state=tk.NORMAL)
    textUserFiles.delete('1.0', 'end')
    if len(client_filepath):
        for i in client_filepath:
            textUserFiles.insert(tk.END, ">" + i + ":\n")
            for j in os.listdir(i):
                if os.path.isdir(j):
                  for k in os.listdir(j):#显示二级文件目录
                      textUserFiles.insert(tk.END, "      "+ k + "\n")
                else :
                    textUserFiles.insert(tk.END, "   "+ j + "\n")

    textUserFiles.config(state=tk.DISABLED)
    return

# 清空消息
def clearMsg(ev=None):
    textMsg.config(state=tk.NORMAL)
    textMsg.delete('1.0', 'end')
    textMsg.config(state=tk.DISABLED)

    updateFiles()
    updateUserFiles()

    entryMsg.delete('1.0', 'end')
    return


# 清空消息请求记录
def clearAll():
    # 清空在线用户列表
    USERS.clear()

    # 清除消息请求纪录
    textFiles.config(state=tk.NORMAL)
    textFiles.delete('1.0', 'end')
    textFiles.config(state=tk.DISABLED)

    textUserFiles.config(state=tk.NORMAL)
    textUserFiles.delete('1.0', 'end')
    textUserFiles.config(state=tk.DISABLED)
    # 清空传输的请求信息
    clearMsg()
    return


# 弹窗显示
def popup(_title_, _message_, _type_):
    # showinfo, showwarning, showerror
    if _type_ == "error":
        tk.messagebox.showerror(title=_title_, message=_message_)
    elif _type_ == "warning":
        tk.messagebox.showwarning(title=_title_, message=_message_)
    # _type_ == "info"
    else:
        tk.messagebox.showinfo(title=_title_, message=_message_)
    return


# 关闭窗口
def closeWindow():
    # 关闭窗口就要断开连接
    closeConnect(needClear=False)
    root.destroy()
    return

# 接下来是改变颜色按键的一些函数，优化用户体验，鼠标滑过按键就会有效果

def changeColorButtonConnEnter(ev=None):
    buttonConn.config(fg=_COLOR_WHITE_)
    return


def changeColorButtonConnLeave(ev=None):
    buttonConn.config(fg=_COLOR_LEFT_)
    return

def changeColorButtonClearEnter(ev=None):
    buttonClear.config(fg=_COLOR_WHITE_)
    return


def changeColorButtonClearLeave(ev=None):
    buttonClear.config(fg=_COLOR_LEFT_)
    return


def changeColorButtonExitEnter(ev=None):
    buttonExit.config(fg=_COLOR_WHITE_)
    return


def changeColorButtonExitLeave(ev=None):
    buttonExit.config(fg=_COLOR_LEFT_)
    return


def changeColorButtonSendEnter(ev=None):
    buttonSend.config(bg=_COLOR_GREEN_)
    buttonSend.config(fg=_COLOR_WHITE_)
    return


def changeColorButtonSendLeave(ev=None):
    buttonSend.config(bg=_COLOR_GRAY2_)
    buttonSend.config(fg=_COLOR_BLACK_)
    return


def changeColorButtonPutEnter(ev=None):
    buttonPut.config(bg=_COLOR_GREEN_)
    buttonPut.config(fg=_COLOR_WHITE_)
    return


def changeColorButtonPutLeave(ev=None):
    buttonPut.config(bg=_COLOR_GRAY2_)
    buttonPut.config(fg=_COLOR_BLACK_)
    return

def changeColorButtonShowDirEnter(ev=None):
    buttonShowDir.config(bg=_COLOR_GREEN_)
    buttonShowDir.config(fg=_COLOR_WHITE_)
    return


def changeColorButtonShowDirLeave(ev=None):
    buttonShowDir.config(bg=_COLOR_GRAY2_)
    buttonShowDir.config(fg=_COLOR_BLACK_)
    return


if __name__ == "__main__":
    # 实例化一个窗口
    root = tk.Tk()
    # 定义窗口标题
    root.title('文件传输管理系统')
    # 绑定窗口关闭按键
    root.protocol("WM_DELETE_WINDOW", closeWindow)

    # 用于定位窗口的各个组件
    _WIN_W_0_ = 0
    _WIN_W_1_ = 60
    _WIN_W_2_ = 310
    _WIN_W_3_ = 710
    _WIN_H_0_ = 0
    _WIN_H_1_ = 50
    _WIN_H_2_ = 450
    _WIN_H_3_ = 560

    # 初始化界面
    Width = _WIN_W_3_
    Height = _WIN_H_3_
    # 获取屏幕的宽和高
    screenWidth = root.winfo_screenwidth()
    screenHeight = root.winfo_screenheight()
    align = '%dx%d+%d+%d' % (Width, Height, (screenWidth -
                                             Width) / 2, (screenHeight - Height) / 2)
    # 定义窗口大小及位置
    root.geometry(align)
    root.resizable(width=False, height=False)

    # 创建一个画布，把组件放置在画布上
    canvas = tk.Canvas(root, width=Width, height=Height, bg=_COLOR_WHITE_)

    try:
        # create_window参数：横向位置，竖向位置，宽，高，组件
        # buttonConn  = tk.Button(root, text = "登入", font = "微软雅黑 12", command = connectServer, default = tk.ACTIVE, fg = _COLOR_LEFT_, bg = _COLOR_BLACK_)
        buttonConn = tk.Label(root, text="登入", fg=_COLOR_LEFT_,
                              font="微软雅黑 11", bg=_COLOR_BLACK_)
        buttonConn.bind("<Enter>", changeColorButtonConnEnter)
        buttonConn.bind("<Leave>", changeColorButtonConnLeave)
        buttonConn.bind("<Button-1>", connectServer)

        # buttonClear = tk.Button(root, text = "清屏", font = "微软雅黑 12", command = clearMsg, fg = _COLOR_LEFT_, bg = _COLOR_BLACK_)
        buttonClear = tk.Label(
            root, text="刷新", font="微软雅黑 11", fg=_COLOR_LEFT_, bg=_COLOR_BLACK_)
        buttonClear.bind("<Enter>", changeColorButtonClearEnter)
        buttonClear.bind("<Leave>", changeColorButtonClearLeave)
        buttonClear.bind("<Button-1>", clearMsg)
        # buttonExit  = tk.Button(root, text = "登出", font = "微软雅黑 12", command = closeConnect, fg = _COLOR_LEFT_, bg = _COLOR_BLACK_)
        buttonExit = tk.Label(root, text="登出", font="微软雅黑 11",
                              fg=_COLOR_LEFT_, bg=_COLOR_BLACK_)
        buttonExit.bind("<Enter>", changeColorButtonExitEnter)
        buttonExit.bind("<Leave>", changeColorButtonExitLeave)
        buttonExit.bind("<Button-1>", closeConnect)

        canvas.create_window(30, 35,  width=50, heigh=30,  window=buttonConn)
        canvas.create_window(30, 490, width=50, heigh=30,  window=buttonClear)
        canvas.create_window(30, 530, width=50, heigh=30,  window=buttonExit)

        # 显示服务器端文件
        # labelUsers = tk.Label(root, text = "在线用户", font = "微软雅黑 14", bg = _COLOR_GRAY_)
        textFiles = tk.Text(root, font="微软雅黑 12", bg=_COLOR_GRAY_)
        textFiles.config(state=tk.DISABLED)
        # 显示客户端文件
        # labelUsers = tk.Label(root, text = "在线用户", font = "微软雅黑 14", bg = _COLOR_GRAY_)
        textUserFiles = tk.Text(root, font="微软雅黑 12", bg=_COLOR_GRAY_)
        textUserFiles.config(state=tk.DISABLED)

        # canvas.create_window((_WIN_W_2_ + _WIN_W_1_)/2, 25,  width = _WIN_W_2_ - _WIN_W_1_, heigh = 50,  window = labelUsers)
        canvas.create_window(185, 190, width=_WIN_W_2_ - _WIN_W_1_,
                             heigh=_WIN_H_3_ - _WIN_H_1_-300, window=textFiles)
        canvas.create_window(185, 430, width=_WIN_W_2_ - _WIN_W_1_,
                             heigh=_WIN_H_3_ - _WIN_H_1_-300, window=textUserFiles)

        # 服务器文件列表，客户端文件列表和输入窗口
        userName = tk.Variable()
        entryUserName = tk.Entry(root, textvariable=userName, bg=_COLOR_GRAY2_)
        entryUserName.bind("<Return>", connectServer)

        labelFilename = tk.Label(
            root, text="服务器文件列表", font="微软雅黑 11", fg=_COLOR_BLACK_, bg=_COLOR_GRAY2_)

        labelUserFilename = tk.Label(
            root, text="客户端文件列表", font="微软雅黑 11", fg=_COLOR_BLACK_, bg=_COLOR_GRAY2_)

        canvas.create_window((_WIN_W_2_ + _WIN_W_1_)/2, _WIN_H_1_ - 12.5,
                             width=_WIN_W_2_ - _WIN_W_1_, heigh=25, window=entryUserName)
        canvas.create_window((_WIN_W_2_ + _WIN_W_1_)/2, _WIN_H_0_+70,
                             width=_WIN_W_2_ - _WIN_W_1_, heigh=25, window=labelFilename)
        canvas.create_window((_WIN_W_2_ + _WIN_W_1_)/2, _WIN_H_0_+310,
                             width=_WIN_W_2_ - _WIN_W_1_, heigh=25, window=labelUserFilename)

        # 显示所有请求消息
        groupname = tk.StringVar()
        groupname.set(GROUPNAME)
        labelMsg = tk.Label(root, textvariable=groupname,
                            font="微软雅黑 14", bg=_COLOR_GRAY2_)
        textMsg = tk.Text(root, font="微软雅黑 12", bg=_COLOR_GRAY2_)
        textMsg.config(state=tk.DISABLED)
        textMsg.tag_configure("redcolor", foreground="red")
        textMsg.tag_configure("greencolor", foreground="green")
        textMsg.tag_configure("bluecolor", foreground="blue")
        textMsg.tag_configure(
            "graycolor", foreground=_COLOR_BLACK_, font="微软雅黑 10")

        canvas.create_window(510, 25, width=_WIN_W_3_ - _WIN_W_2_,
                             heigh=_WIN_H_1_ - _WIN_H_0_,  window=labelMsg)
        canvas.create_window(510, 250, width=_WIN_W_3_ - _WIN_W_2_,
                             heigh=_WIN_H_2_ - _WIN_H_1_, window=textMsg)

        # 发送文件参数窗口与多个功能按键
        entryMsg = tk.Text(root, font="微软雅黑 12", bg=_COLOR_WHITE_)
        # buttonSend,从服务器下载指定文件
        buttonSend = tk.Label(root, text="下载(D)",
                              font="微软雅黑 10", bg=_COLOR_GRAY2_)
        buttonSend.bind("<Enter>", changeColorButtonSendEnter)
        buttonSend.bind("<Leave>", changeColorButtonSendLeave)
        buttonSend.bind("<Button-1>", sendMsg)
        # buttonput，将制定文件传送至服务器
        buttonPut = tk.Label(root, text="上传(U)",
                             font="微软雅黑 10", bg=_COLOR_GRAY2_)
        buttonPut.bind("<Enter>", changeColorButtonPutEnter)
        buttonPut.bind("<Leave>", changeColorButtonPutLeave)
        buttonPut.bind("<Button-1>", putMsg)
        # buttonShowDir，显示系统文件的存储位置
        buttonShowDir = tk.Label(root, text="查询(S)",
                             font="微软雅黑 10", bg=_COLOR_GRAY2_)
        buttonShowDir.bind("<Enter>", changeColorButtonShowDirEnter)
        buttonShowDir.bind("<Leave>", changeColorButtonShowDirLeave)
        buttonShowDir.bind("<Button-1>", ShowDir)
        # 创建窗口
        canvas.create_window((_WIN_W_3_ + _WIN_W_2_)/2, (_WIN_H_3_ + _WIN_H_2_)/2-40,
                             width=_WIN_W_3_ - _WIN_W_2_, heigh=_WIN_H_3_ - _WIN_H_2_-80, window=entryMsg)
        canvas.create_window(_WIN_W_3_ - 280, _WIN_H_3_ -
                             50, width=60,  heigh=50,  window=buttonSend)
        canvas.create_window(_WIN_W_3_-350, _WIN_H_3_ -
                             50, width=60,  heigh=50,  window=buttonPut)
        canvas.create_window(_WIN_W_3_-210, _WIN_H_3_ -
                             50, width=60,  heigh=50,  window=buttonShowDir)
    except:
        popup("错误", "初始化界面发生错误，请检查", "error")

    canvas.pack()

    root.mainloop()
