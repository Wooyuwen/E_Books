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
    public partial class account : Form
    {
        SqlConnection con;
        public account()
        {
            InitializeComponent();
            try
            {
                //连接数据库
                con = new SqlConnection("server = 127.0.0.1; database = library; uid = odbcuser; pwd = 1");

                con.Open();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
            }
        }

        public void DoHandler(SqlDataReader dr)//dr为用户的信息，传入这个窗口
        {
            tb_staff_id.Text = dr[0].ToString();
            //tb_pwd.Text = dr[1].ToString();
            //tb_pwd2.Text = dr[1].ToString();
            tb_usr_name.Text = dr[2].ToString();
            tb_name.Text = dr[3].ToString();
            tb_phone.Text = dr[4].ToString();
            tb_gender.Text = dr[5].ToString();
            tb_age.Text = dr[6].ToString();
            return;
        }

        private void Button1_Click(object sender, EventArgs e)//更新数据
        {
            if (tb_pwd.Text != tb_pwd2.Text)
            {
                label1.Text = "密码不一致！";
                label1.Show();
                return;
            }
            else if (tb_name.Text.Length.Equals(0))
            {
                label1.Text = "姓名不能为空！";
                label1.Show();
                return;
            }
            string age = tb_age.Text.ToString();
            for (int j = 0; j < tb_age.Text.Length; ++j)
            {
                if (age[j] < '0' || age[j] > '9')
                {
                    label1.Text = "年龄必须为数字！";
                    return;
                }
            }

            con.Open();
            
            String SqlUpdate = "update staff set name = '" + tb_name.Text + "'";
            if (!tb_pwd.Text.Length.Equals(0)) {

                //加密
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(tb_pwd.Text));
                String pwd = BitConverter.ToString(output).Replace("-", "");

                SqlUpdate += ", pwd = '" + pwd + "'";
            }
            if (!tb_usr_name.Text.Length.Equals(0)) { SqlUpdate += ", usr_name = '" + tb_usr_name.Text + "'"; } else { SqlUpdate += ", usr_name = NULL"; }
            if (!tb_phone.Text.Length.Equals(0)) { SqlUpdate += ", phone = '" + tb_phone.Text + "'"; } else { SqlUpdate += ", phone = NULL"; }
            if (!tb_gender.Text.Length.Equals(0)) { SqlUpdate += ", gender = '" + tb_gender.Text + "'"; } else { SqlUpdate += ", gender = NULL"; }
            if (!tb_age.Text.Length.Equals(0)) { SqlUpdate += ", age = '" + tb_age.Text + "'"; } else { SqlUpdate += ", age = NULL"; }
            SqlUpdate += " where staff_id = '" + tb_staff_id.Text + "';"; 
            //SqlUpdate += ", usr_name = '" + tb_usr_name.Text + "', phone = '" + tb_phone.Text + 
            //    "', gender = '" + tb_gender.Text + "', age = '" + tb_age.Text + "' where staff_id = '" + tb_staff_id.Text + "';";
            try
            {
                SqlCommand com = new SqlCommand(SqlUpdate, con);
                com.ExecuteNonQuery();
                label1.Text = "修改成功！";
                label1.Show();
            }
            catch (Exception)
            {
                label1.Text = "格式不正确或输入太长！";
                label1.Show();
            }

            //断开数据库连接
            con.Close();
        }
        
        private void Button2_Click(object sender, EventArgs e)//关闭窗口
        {
            this.Dispose();
            this.Close();
        }

        private void account_Load(object sender, EventArgs e)
        {

        }
    }
}
