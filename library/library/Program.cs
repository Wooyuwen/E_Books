using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace library
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());
            bool Continue = false;
            do
            {
                //生成登录界面
                Login login = new Login();

                //界面转换
                login.ShowDialog();
                //保存工号staff_id
                String Login_id = login.Login_id;
                
                if (login.DialogResult == DialogResult.OK)
                {
                    login.Dispose();
                    //生成主界面
                    Library myLibrary = new Library(Login_id);
                    myLibrary.ShowDialog();

                    //判断是否是注销，如果是，则Continue = 1
                    if (myLibrary.DialogResult == DialogResult.OK)
                    {
                        Continue = true;
                    }
                    else
                    {
                        Continue = false;
                    }
                }
                else if (login.DialogResult == DialogResult.Cancel)
                {
                    login.Dispose();
                    return;
                }
            } while (Continue);
        }
    }
}
