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
    public partial class staff_manage : Form
    {
        private SqlConnection con;
        private SqlDataAdapter ada;
        private DataTable dt;
        public staff_manage()
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
                return;
            }

            Fresh();
        }
        
        private void Fresh()
        {
            con.Open();
            //获取员工信息
            String SqlStaff = "select staff_id, pwd, usr_name, name, phone, gender, age from staff";
            try
            {
                ada = new SqlDataAdapter(SqlStaff, con);
                dt = new DataTable();
                ada.Fill(dt);
                ada.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                label1.Text = "更新数据失败！";
            }
            //修改列名
            dt.Columns[0].ColumnName = "工号";
            dt.Columns[1].ColumnName = "密码";
            dt.Columns[2].ColumnName = "用户名";
            dt.Columns[3].ColumnName = "姓名";
            dt.Columns[4].ColumnName = "电话号码";
            dt.Columns[5].ColumnName = "性别";
            dt.Columns[6].ColumnName = "年龄";
            //隐藏密码
            int nRows = dt.Rows.Count;
            for (int i = 0; i < nRows; ++i)
            {
                dt.Rows[i][1] = "";
            }
            dataGridView1.DataSource = dt;
            dt.Dispose();
            con.Close();
        }

        //修改或插入
        private void Button1_Click(object sender, EventArgs e)
        {
            //读取表格到dt
            dt = dataGridView1.DataSource as DataTable;
            //首先判断修改是否符合要求
            int nRows = dt.Rows.Count;
            for (int i = 0; i < nRows; ++i)
            {
                if (dt.Rows[i][3].ToString() == "")
                {
                    label1.Text = "姓名不能为空！";
                    return;
                }
                if (dt.Rows[i][5].ToString() != "" && dt.Rows[i][5].ToString() != "male" && dt.Rows[i][5].ToString() != "female")
                {
                    label1.Text = "性别格式错误！";
                    return;
                }
            }

            con.Open();
            for(int i= 0; i < nRows; ++i)
            {
                //修改成员
                String SqlUpdate;
                if(dt.Rows[i][0].ToString() == "") { continue; }
                String SqlFind = "select count(*) from staff where staff_id = '" + dt.Rows[i][0].ToString() + "';";
                SqlCommand com0 = new SqlCommand(SqlFind, con);
                //若返回参数大于0，则说明存在这个账号，只修改
                if (Convert.ToInt32(com0.ExecuteScalar()) > 0)
                {
                    SqlUpdate = "update staff set name = '" + dt.Rows[i][3].ToString() + "'";
                    if (dt.Rows[i][1].ToString() != "")
                    {
                        //加密
                        MD5 md5 = new MD5CryptoServiceProvider();
                        byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(dt.Rows[i][1].ToString()));
                        String pwd = BitConverter.ToString(output).Replace("-", "");

                        SqlUpdate += ", pwd = '" + pwd + "'";
                    }
                    if (dt.Rows[i][2].ToString() != "") { SqlUpdate += ", usr_name = '" + dt.Rows[i][2].ToString() + "'"; } else { SqlUpdate += ", usr_name = NULL"; }
                    if (dt.Rows[i][4].ToString() != "") { SqlUpdate += ", phone = '" + dt.Rows[i][4].ToString() + "'"; } else { SqlUpdate += ", phone = NULL"; }
                    if (dt.Rows[i][5].ToString() != "") { SqlUpdate += ", gender = '" + dt.Rows[i][5].ToString() + "'"; } else { SqlUpdate += ", gender = NULL"; }
                    if (dt.Rows[i][6].ToString() != "") { SqlUpdate += ", age = '" + dt.Rows[i][6].ToString() + "'"; } else { SqlUpdate += ", age = NULL"; }

                    SqlUpdate += " where staff_id = '" + dt.Rows[i][0].ToString() + "';";
                }
                //否则需要插入
                else
                {
                    SqlUpdate = "insert into staff ( staff_id";
                    if (dt.Rows[i][1].ToString() != "") { SqlUpdate += ", pwd "; }
                    if (dt.Rows[i][2].ToString() != "") { SqlUpdate += ", usr_name "; }
                    SqlUpdate += ", name "; 
                    if (dt.Rows[i][4].ToString() != "") { SqlUpdate += ", phone "; }
                    if (dt.Rows[i][5].ToString() != "") { SqlUpdate += ", gender "; }
                    if (dt.Rows[i][6].ToString() != "") { SqlUpdate += ", age "; }
                    SqlUpdate += ") values ('" + dt.Rows[i][0].ToString() + "'";
                    if (dt.Rows[i][1].ToString() != "")
                    {
                        //加密
                        MD5 md5 = new MD5CryptoServiceProvider();
                        byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(dt.Rows[i][1].ToString()));
                        String pwd = BitConverter.ToString(output).Replace("-", "");
                        SqlUpdate += ", '" + pwd + "'";
                    }
                    if (dt.Rows[i][2].ToString() != "") { SqlUpdate += ", '" + dt.Rows[i][2].ToString() + "'"; }
                    SqlUpdate += ", '" + dt.Rows[i][3].ToString() + "'"; 
                    if (dt.Rows[i][4].ToString() != "") { SqlUpdate += ", '" + dt.Rows[i][4].ToString() + "'"; }
                    if (dt.Rows[i][5].ToString() != "") { SqlUpdate += ", '" + dt.Rows[i][5].ToString() + "'"; }
                    if (dt.Rows[i][6].ToString() != "") { SqlUpdate += ", '" + dt.Rows[i][6].ToString() + "'"; }
                    SqlUpdate += ");";
                }

                try
                {
                    SqlCommand com = new SqlCommand(SqlUpdate, con);
                    com.ExecuteNonQuery();
                    label1.Text = "修改成功！";
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    label1.Text = "修改失败！";
                    con.Close();
                    return;
                }
            }
            con.Close();
            Fresh();
        }

        //删除
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length.Equals(0))
            {
                label1.Text = "输入不能为空！";
                return;
            }
            else if (textBox1.Text == "00000")
            {
                label1.Text = "不能删除超级管理员（工号为00000）！";
                return;
            }
            con.Open();
            String SqlUpdate = "delete staff where staff_id = '" + textBox1.Text + "';";
            try
            {
                SqlCommand com = new SqlCommand(SqlUpdate, con);
                int i = com.ExecuteNonQuery();
                if (i == 0)
                {
                    label1.Text = "找不到该用户，删除失败！";
                }
                else
                {
                    label1.Text = "删除成功！";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString() + "操作失败！");
                label1.Text = "操作失败！";
            }
            finally
            {
                //断开数据库连接
                con.Close();
            }
            Fresh();
        }

        private void bt_exit_Click(object sender, EventArgs e)//退出
        {
            this.Dispose();
            this.Close();
        }
    }
}
