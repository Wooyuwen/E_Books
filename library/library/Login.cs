using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace library
{
    public partial class Login : Form
    {
        SqlConnection con;
        public Login()
        {
            InitializeComponent();
            try
            {
                string strcon = "server=127.0.0.1;database=library;uid=odbcuser;pwd=1";//服务器地址为本机地址，样例是个坑，一直连不上
                //连接数据库
                con = new SqlConnection(strcon);
                //打开数据库，登录界面一直开着没关系，主界面里需要一直开开关关
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }

        private void bt_login_Click(object sender, EventArgs e)
        {
            //从TextBox中获取数据
            Login_id = tb_staff_id.Text;//保存输入的账号
            String usrPwd = tb_pwd.Text;//输入的密码
            
            if (Login_id.Equals("") || usrPwd.Equals(""))
            {
                label1.Text = "用户和密码都不能为空！";
                label1.Show();
            }
            //若不为空，验证输入的数据是否与数据库中的数据匹配
            else
            {
                //加密
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(usrPwd));
                String pwd = BitConverter.ToString(output).Replace("-", "");

                //SQL语句
                String SqlLogin = "select count(*) from staff where staff_id = '" + Login_id + "' and pwd = '" + pwd + "';";
                SqlCommand com = new SqlCommand(SqlLogin, con);
                //若返回参数大于0，则说明输入值有效，安排上，登录
                if (Convert.ToInt32(com.ExecuteScalar()) > 0)
                {
                    //跳转主界面
                    label1.Hide();
                    con.Close();
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                    this.Close();
                }
                //输入不正确，弹出窗口提示不正确
                else
                {
                    label1.Text = "账号或密码错误！";
                    label1.Show();
                }
            }
            //如果想要点击“登录”直接进入主界面查看的话，请将上面的if(){}else{}注释掉，并且将下列语句取消注释
            /*Login_id = "00000";
            MessageBox.Show("后门登录成功！");
            con.Close();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
            this.Close();*/

        }

        //下面的函数不用看了
        private void Pwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_usr_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void label_usr_name_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
             
        }
    }
}
