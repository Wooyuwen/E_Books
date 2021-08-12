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

namespace library
{
    public partial class Library : Form
    {
        public String Login_id;//保存账号，通过这个来确认用户是超级管理员还是普通管理员

        SqlConnection con;//SQL连接句柄，在运行过程中会不断开开关关，目的是在切换窗口时能够关闭数据库
        public Library(String Name)
        {
            InitializeComponent();
            Login_id = label_id.Text = Name;//保存账号，并且显示在主界面的左上方
            try
            {
                //连接数据库
                con = new SqlConnection("server = 127.0.0.1; database = library; uid = odbcuser; pwd = 1");

                con.Open();//尝试打开一次，如果不能打开，就会报错
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "打开数据库失败");
                //这里选择不关闭界面
            }
        }

        private void MyLibrary_Load(object sender, EventArgs e)
        {
            //窗口加载时先安排显示内容
            Bt_close_Click(sender, e);
        }

        private void Bt_account_setting_Click(object sender, EventArgs e)//账户设置
        {
            account accountForm = new account();//创建新的窗口

            con.Open();
            String SqlBook = "select * from staff where staff_id = '" + Login_id + "';";
            SqlCommand com = new SqlCommand(SqlBook, con);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();

            accountForm.DoHandler(dr);//传输数据
            dr.Close();
            con.Close();
            this.Hide();
            //accountForm.Show();//显示窗口
            accountForm.ShowDialog();
            this.Show();
        }

        private void Bt_usr_Click(object sender, EventArgs e)//管理用户
        {
            if (Login_id != "00000")
            {
                MessageBox.Show("您没有权限！");
                return;
            }
            staff_manage manage = new staff_manage();//创建新窗口
            this.Hide();
            manage.ShowDialog();
            this.Show();
        }

        private void Bt_search_book_Click(object sender, EventArgs e)//查询书籍
        {
            Bt_close_Click(sender, e);
            label0.Hide();
            label1.Show();
            textBox1.Show();//输入内容
            label2.Show();
            label3.Show();
            checkBox1.Show();
            checkBox2.Show();
            checkBox3.Show();
            checkBox4.Show();
            checkBox5.Show();
            button1.Show();
            label3.Text = "这里显示查找状态信息";
            bt_close.Show();
        }

        private void Search_book(string SqlBook)
        {//这个函数只是将作者合并，由参数SqlBook提供SQL语句
            try
            {
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(SqlBook, con);
                DataTable dt = new DataTable();//新建表格
                sd.Fill(dt);//填装表格
                //下面合并多条查询结果
                int nRows = dt.Rows.Count;
                for (int i = 0; i < nRows; ++i)
                {
                    if (dt.Rows[i][5].ToString() == "1")//添加标签，作者或者译者
                    {
                        dt.Rows[i][4] += "(" + dt.Rows[i][6].ToString() + " 译) ";
                    }
                    else
                    {
                        dt.Rows[i][4] += "(" + dt.Rows[i][6].ToString() + " 作) ";
                    }
                    int j = i;
                    while (j < nRows - 1 && dt.Rows[i][0].ToString() == dt.Rows[j + 1][0].ToString())//合并
                    {
                        ++j;
                        if (dt.Rows[j][5].ToString() == "1")
                        {
                            dt.Rows[j][4] += "(" + dt.Rows[j][6].ToString() + " 译)";
                        }
                        else
                        {
                            dt.Rows[j][4] += "(" + dt.Rows[j][6].ToString() + " 作) ";
                        }
                        dt.Rows[i][4] += dt.Rows[j][4].ToString();
                        dt.Rows[j].Delete();//删除
                    }
                    i = j;
                }
                dt.Columns.Remove("translate");//把翻译这一列去掉
                dt.Columns.Remove("country");//把国籍这一列去掉
                //修改列名
                dt.Columns[0].ColumnName = "编号";
                dt.Columns[1].ColumnName = "ISBN";
                dt.Columns[2].ColumnName = "书名";
                dt.Columns[3].ColumnName = "出版社";
                dt.Columns[4].ColumnName = "作者/译者";
                dt.Columns[5].ColumnName = "出版年份";
                dt.Columns[6].ColumnName = "价格";
                dt.Columns[7].ColumnName = "库存";

                dataGridView1.DataSource = dt;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 280;
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message.ToString() + "查询失败");
                label3.Text = "查询失败！";
            }
            finally
            {
                con.Close();
            }
            return;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length.Equals(0))
            {
                label3.Text = "输入内容不能为空！";
                return;
            }
            else if(textBox1.Text.Length > 30)
            {
                label3.Text = "输入内容的长度不能超过30！";
                return;
            }
            else if(checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false)
            {
                label3.Text = "至少选择一项查询范围！";
                return;
            }
            String SqlBook = "select book.book_id, book.isbn, book.title, book.pub_house, written_by.name as author, translate, country, year, price, quantity " +
            "from ((book left outer join written_by on book.book_id = written_by.book_id) left outer join stock on book.book_id = stock.book_id) left outer join warehouse on stock.address = warehouse.address where price > 0 and (";

            if(checkBox1.Checked == true)
                SqlBook += "book.book_id = '" + textBox1.Text + "' or ";
            if (checkBox2.Checked == true)
                SqlBook += "book.isbn like '%" + textBox1.Text + "%'or ";
            if (checkBox3.Checked == true)
                SqlBook += "book.title like '%" + textBox1.Text + "%' or ";
            if (checkBox4.Checked == true)
                SqlBook += "book.pub_house like '%" + textBox1.Text + "%' or ";
            if (checkBox5.Checked == true)//这里把所有作者都显示在一行，算法在下面
                SqlBook += "book.book_id in (select book_id from written_by where name like '%" + textBox1.Text + "%') or ";
            SqlBook = SqlBook.Substring(0, SqlBook.Length - 3);
            SqlBook += ");";

            Search_book(SqlBook);
        }

        private void Bt_change_book_Click(object sender, EventArgs e)//修改信息
        {
            Bt_close_Click(sender, e);
            label0.Hide();
            label4.Show();
            textBox2.Show();//输入原编号
            label5.Show();
            textBox3.Show();//新编号
            label6.Show();
            textBox4.Show();//新ISBN
            label7.Show();
            textBox5.Show();//新书名
            label8.Show();
            textBox6.Show();//新出版社
            label9.Show();
            textBox7.Show();//新出版年份
            label10.Show();
            textBox8.Show();//新售价
            label11.Show();
            textBox9.Show();//新库存
            //修改/删除/添加作者
            textBox10.Show();
            textBox11.Show();
            checkBox6.Show();
            checkBox7.Show();
            textBox12.Show();
            textBox13.Show();
            checkBox8.Show();
            textBox14.Show();
            textBox15.Show();
            checkBox9.Show();
            button2.Show();
            label12.Show();
            label13.Show();
            label14.Show();
            label14.Text = "这里显示修改状态信息（不修改则无需输入）";
            bt_close.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length.Equals(0))
            {
                label14.Text = "要修改的书籍编号不能为空！";
                return;
            }
            con.Open();
            String SqlSearchBook = "select count(*) from book where book_id = '" + textBox2.Text + "';";
            SqlCommand com = new SqlCommand(SqlSearchBook, con);
            //若返回参数等于0，则说明没有这本书，报错
            if (Convert.ToInt32(com.ExecuteScalar()) == 0)
            {
                label14.Text = "没有这本书！";
                con.Close();
                return;
            }

            //首先考虑修改作者
            //修改/删除书籍链接的作者
            if(!textBox10.Text.Length.Equals(0)&& !textBox11.Text.Length.Equals(0)){
                String SqlSearchAuthor = "select count(*) from written_by where name  = '" + textBox11.Text + "' and country = '" + textBox10.Text + "';";
                SqlCommand com2 = new SqlCommand(SqlSearchAuthor, con);
                //若返回参数等于0，则说明没有链接该作者，报错
                if (Convert.ToInt32(com2.ExecuteScalar()) == 0)
                {
                    label14.Text = "要修改/删除作者" + textBox11.Text + "，但是没有链接该作者！";
                    con.Close();
                    return;
                }
                //删除作者
                else if (checkBox7.Checked)
                {
                    try
                    {
                        SqlCommand com5 = new SqlCommand("delete from written_by where book_id = '" + textBox2.Text + "' and name = '" + textBox11.Text + "' and country = '" + textBox10.Text + "';", con);
                        com5.ExecuteNonQuery();
                        label14.Text = "删除作者成功！";
                    }
                    catch (Exception)
                    {
                        label14.Text = "删除作者失败！";
                        con.Close();
                        return;
                    }
                }
                //判定是否需要修改
                else if (textBox10.Text == textBox12.Text && textBox11.Text == textBox13.Text && checkBox6.Checked == checkBox8.Checked)
                {
                    label14.Text = "作者无需修改！";
                    //con.Close();
                    //return;
                }
                //修改
                else if (!textBox12.Text.Length.Equals(0) && !textBox13.Text.Length.Equals(0))
                {
                    String SqlSearchAuthor2 = "select count(*) from author where name = '" + textBox13.Text + "' and country = '" + textBox12.Text + "';";
                    SqlCommand com3 = new SqlCommand(SqlSearchAuthor2, con);
                    //若返回参数等于0，则说明没有该作者，需要创建
                    if (Convert.ToInt32(com3.ExecuteScalar()) == 0)
                    {
                        String SqlInsertAuthor = "insert into author values ('" + textBox13.Text + "', '" + textBox12.Text + "');";
                        try
                        {
                            SqlCommand com4 = new SqlCommand(SqlInsertAuthor, con);
                            com4.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            label14.Text = "向作者库插入新作者失败！";
                            con.Close();
                            return;
                        }
                    }

                    String SqlUpdateAuthor = "update written_by set name = '" + textBox13.Text + "', country = '" + textBox12.Text;
                    if (checkBox8.Checked)
                    {
                        SqlUpdateAuthor += "', translate = '1'";
                    }
                    else
                    {
                        SqlUpdateAuthor += "', translate = '0'";
                    }
                    SqlUpdateAuthor += " where book_id = '" + textBox2.Text + "' and name = '" + textBox11.Text + "' and country = '" + textBox10.Text + "' and translate = '";
                    if (checkBox6.Checked)
                    {
                        SqlUpdateAuthor += "1';";
                    }
                    else
                    {
                        SqlUpdateAuthor += "0';";
                    }
                    //修改
                    try
                    {
                        SqlCommand com4 = new SqlCommand(SqlUpdateAuthor, con);
                        com4.ExecuteNonQuery();
                        label14.Text = "作者修改成功！";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString() + "链接作者失败！");
                        label14.Text = "作者修改失败！";
                        con.Close();
                        return;
                    }
                }
            }
            //添加作者
            if (!textBox14.Text.Length.Equals(0) && !textBox15.Text.Length.Equals(0))
            {

                String SqlSearchAuthor3 = "select count(*) from author where name = '" + textBox15.Text + "' and country = '" + textBox14.Text + "';";
                SqlCommand com2 = new SqlCommand(SqlSearchAuthor3, con);

                //若返回参数等于0，则说明作者库里没有该作者，需要创建
                if (Convert.ToInt32(com2.ExecuteScalar()) == 0)
                {
                    String SqlInsertAuthor = "insert into author values ('" + textBox15.Text + "', '" + textBox14.Text + "');";
                    try
                    {
                        SqlCommand com3 = new SqlCommand(SqlInsertAuthor, con);
                        com3.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        label14.Text = "向作者库插入新作者失败！";
                        con.Close();
                        return;
                    }
                }

                String SqlUpdateAuthor = "insert into written_by values ('" + textBox2.Text + "', '" + textBox15.Text + "', '" + textBox14.Text;
                if (checkBox9.Checked)
                {
                    SqlUpdateAuthor += "', '1');";
                }
                else
                {
                    SqlUpdateAuthor += "', '0');";
                }
                //插入
                try
                {
                    SqlCommand com3 = new SqlCommand(SqlUpdateAuthor, con);
                    com3.ExecuteNonQuery();
                    label14.Text = "新增作者成功！";
                }
                catch (Exception)
                {
                    label14.Text = "新增作者失败！";
                }

            }

            //更新编号、ISBN、书名、出版社、出版年份、售价和库存
            String SqlUpdate = "";
            if (!textBox9.Text.Length.Equals(0))
            {
                SqlUpdate += "update warehouse set quantity = '" + textBox9.Text + "' where address in (select stock.address from "
                + "stock, warehouse where stock.book_id = '" + textBox2.Text + "');";
            }
            //修改这些信息放在最后，是因为可能会修改书籍编号
            if (!textBox3.Text.Length.Equals(0) || !textBox4.Text.Length.Equals(0) || !textBox5.Text.Length.Equals(0) || !textBox6.Text.Length.Equals(0) || !textBox7.Text.Length.Equals(0) || !textBox8.Text.Length.Equals(0))
            {
                SqlUpdate += "update book set ";
                if (!textBox3.Text.Length.Equals(0)) { SqlUpdate += "book_id = '" + textBox3.Text + "', "; }
                if (!textBox4.Text.Length.Equals(0)) { SqlUpdate += "isbn = '" + textBox4.Text + "', "; }
                if (!textBox5.Text.Length.Equals(0)) { SqlUpdate += "title = '" + textBox5.Text + "', "; }
                if (!textBox6.Text.Length.Equals(0)) { SqlUpdate += "pub_house = '" + textBox6.Text + "', "; }
                if (!textBox7.Text.Length.Equals(0)) { SqlUpdate += "year = '" + textBox7.Text + "', "; }
                if (!textBox8.Text.Length.Equals(0)) { SqlUpdate += "price = '" + textBox8.Text + "', "; }
                SqlUpdate = SqlUpdate.Substring(0, SqlUpdate.Length - 2);
                SqlUpdate += " where book_id = '" + textBox2.Text + "';";
            }
            if(SqlUpdate != "")
            {
                try
                {
                    SqlCommand com2 = new SqlCommand(SqlUpdate, con);
                    int i = com2.ExecuteNonQuery();
                    if (i > 0)
                    {
                        label14.Text = "修改成功！";
                    }
                    else
                    {
                        label14.Text = "没有修改！";
                        con.Close();
                        return;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.Message.ToString() + "更新失败！");
                    label14.Text = "修改失败！";
                }
            }
            
            con.Close();
            String thisId;
            if (!textBox3.Text.Length.Equals(0)) { thisId = textBox3.Text; textBox2.Text = textBox3.Text; }
            else { thisId = textBox2.Text; }
            String SqlBook = "select book.book_id, book.isbn, book.title, book.pub_house, written_by.name as author, translate, country, year, price, quantity " +
            "from ((book left outer join written_by on book.book_id = written_by.book_id) left outer join stock on book.book_id = stock.book_id) left outer join warehouse on stock.address = warehouse.address where book.book_id = '" + thisId + "';";
            Search_book(SqlBook);
            return;
        }

        private void Bt_buy_Click(object sender, EventArgs e)//图书进货
        {
            Bt_close_Click(sender, e);
            label0.Hide();
            label15.Show();
            textBox16.Show();//ISBN
            label16.Show();
            textBox17.Show();//编号
            label17.Show();
            textBox18.Show();//进货价格
            label18.Show();
            textBox19.Show();//购买数量
            button3.Show();
            label19.Show();
            label19.Text = "这里显示添加状态信息";
            bt_close.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (!textBox16.Text.Length.Equals(10) && !textBox16.Text.Length.Equals(13))//ISBN输入格式不满足要求，可以添加判定条件来适应多种格式
            {
                label19.Text = "请输入正确格式的ISBN!";
                return;
            }
            else if (textBox18.Text.Length.Equals(0) || textBox19.Text.Length.Equals(0))
            {
                label19.Text = "请输入进货价格和购买数量！";
                return;
            }

            con.Open();
            String str_id;//图书编号
            bool NeedToAdd = false;//需要为图书添加信息
            String SqlSearchBook = "select count(*) from book where isbn = '" + textBox16.Text + "';";
            SqlCommand com = new SqlCommand(SqlSearchBook, con);
            //若返回参数等于0，则说明没有这本书，需要输入这本书的相关信息
            if (Convert.ToInt32(com.ExecuteScalar()) == 0)
            {
                NeedToAdd = true;
                if (textBox17.Text.Length.Equals(0))
                {
                    label19.Text = "库存里没有该书籍，请添加编号！";
                    con.Close();
                    return;
                }
                else
                {
                    String SqlInsertBook = "insert into book(book_id, isbn) values ('" + textBox17.Text + "', '" + textBox16.Text + "');";
                    try
                    {
                        SqlCommand com2 = new SqlCommand(SqlInsertBook, con);
                        int i = com2.ExecuteNonQuery();
                        if (i == 0)
                        {
                            label19.Text = "添加书籍失败！";
                            con.Close();
                            return;
                        }
                        else
                        {
                            label19.Text = "添加书籍成功！";
                        }
                    }
                    catch (Exception)
                    {
                        label19.Text = "添加书籍失败！";
                        con.Close();
                        return;
                    }
                }
                str_id = textBox17.Text;
            }
            else
            {
                //获取图书编号
                String SqlSearchId = "select book_id from book where isbn = '" + textBox16.Text + "';";
                SqlDataAdapter sd = new SqlDataAdapter(SqlSearchId, con);
                DataTable dt = new DataTable();//新建表格
                sd.Fill(dt);//填装表格
                str_id = dt.Rows[0][0].ToString();
            }

            //加入进货清单
            //先加到账单里，标识为wait，即正在进货中，然后再加到进货清单里
            String SqlInsertBillBuy = "insert into bill values(Convert(varchar(20),Getdate(),120), 'wait', '0', GETDATE(), GETDATE());" +
                "insert into buy values(Convert(varchar(20),Getdate(),120), '" + str_id + "', 'pend', '" + textBox19.Text + "', '" + textBox18.Text + "');";
            try
            {
                SqlCommand com2 = new SqlCommand(SqlInsertBillBuy, con);
                com2.ExecuteNonQuery();
                label19.Text = "创建清单成功！";
            }
            catch (Exception)
            {
                label19.Text = "创建清单失败！";
                con.Close();
                return;
            }
            con.Close();
            if (NeedToAdd)
            {
                MessageBox.Show("请为这本书添加信息！");
                textBox2.Text = str_id;
                Bt_change_book_Click(sender, e);
            }
        }

        private void Bt_pay_back_Click(object sender, EventArgs e)//进货付款/图书退货
        {
            Bt_close_Click(sender, e);
            label0.Hide();
            label20.Show();
            textBox20.Show();//账单编号
            button4.Show();
            button5.Show();
            label21.Show();
            label21.Text = "这里显示状态信息";
            bt_close.Show();
            Show_buy("pend");
        }

        private void Show_buy(String _status)
        {//辅助函数，显示未付款/已付款的清单
            con.Open();
            String SqlShowBuy = "select bill_no, book_id, number, price from buy where _status = '" + _status + "';";
            try
            {
                SqlDataAdapter sd = new SqlDataAdapter(SqlShowBuy, con);
                DataTable dt = new DataTable();//新建表格
                sd.Fill(dt);//填装表格
                //修改列名
                dt.Columns[0].ColumnName = "账单编号";
                dt.Columns[1].ColumnName = "图书编号";
                dt.Columns[2].ColumnName = "购买数量";
                dt.Columns[3].ColumnName = "进货单价";
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Width = 150;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "查询失败");
            }
            con.Close();
        }
        
        private void Button4_Click(object sender, EventArgs e)
        {
            if (textBox20.Text.Length.Equals(0))
            {
                label21.Text = "输入的账单编号不能为空！";
                return;
            }
            String SqlSure = "update buy set _status = 'buy' where bill_no = '" + textBox20.Text + "' and _status = 'pend'; " +
                            "update bill set action = 'buy', total = (select buy.number * buy.price from buy where bill_no ='" + textBox20.Text + "') where bill_no = '" + textBox20.Text + "';";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(SqlSure, con);
                int i = com.ExecuteNonQuery();
                if (i < 2)
                {
                    label21.Text = "付款失败，请检查输入是否正确！";
                }
                else
                {
                    label21.Text = "付款成功！";
                }
            }
            catch (Exception)
            {
                label21.Text = "付款失败！";
            }
            finally
            {
                con.Close();
            }
            Show_buy("pend");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (textBox20.Text.Length.Equals(0))
            {
                label21.Text = "输入的账单编号不能为空！";
                return;
            }
            String SqlSure = "update buy set _status = 'cancel' where bill_no = '" + textBox20.Text + "' and _status = 'pend';";
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(SqlSure, con);
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i == 0)
                {
                    label21.Text = "退货失败，请检查输入是否正确！";
                }
                else
                {
                    label21.Text = "退货成功！";
                }
            }
            catch (Exception)
            {
                label21.Text = "退货失败！";
            }
            finally
            {
                con.Close();
            }
            Show_buy("pend");
        }

        private void Bt_up_Click(object sender, EventArgs e)//到货上架（添加新书）
        {
            Bt_close_Click(sender, e);
            label0.Hide();
            label20.Show();
            textBox20.Show();//账单编号
            textBox21.Show();//库存地址
            textBox22.Show();//零售价格
            label22.Show();
            button6.Show();
            label21.Show();
            label21.Text = "这里显示状态信息";
            bt_close.Show();
            Show_buy("buy");
        }
        
        private void Button6_Click(object sender, EventArgs e)
        {
            if (textBox20.Text.Length.Equals(0))
            {
                label21.Text = "输入的账单编号不能为空！";
                return;
            }

            con.Open();
            String str_id = "";//图书编号
            String number = "";//到货的书籍的数量
            //获取图书编号和进货的数量
            try
            {
                String SqlSelect = "select book_id, number from buy where bill_no = '" + textBox20.Text + "' and _status = 'buy';";
                SqlDataAdapter sd = new SqlDataAdapter(SqlSelect, con);
                DataTable dt = new DataTable();//新建表格
                sd.Fill(dt);//填装表格
                str_id = dt.Rows[0][0].ToString();
                number = dt.Rows[0][1].ToString();
            }
            catch (Exception)
            {
                label21.Text = "获取账单信息失败，请检查输入是否正确！";
                con.Close();
                return;
            }

            String SqlSearchStock = "select count(*) from stock where book_id = '" + str_id + "';";
            SqlCommand com = new SqlCommand(SqlSearchStock, con);
            //若返回参数等于0，则说明没有这本书的库存，需要输入这本书的库存信息
            if (Convert.ToInt32(com.ExecuteScalar()) == 0)
            {
                if (textBox21.Text.Length.Equals(0) || textBox22.Text.Length.Equals(0))
                {
                    label21.Text = "库存内没有该图书，输入库存地址和零售价格的不能为空！";
                    con.Close();
                    return;
                }
                string stockPlace = textBox21.Text;
                string price = textBox22.Text;
                for (int i = 0; i < stockPlace.Length; ++i)
                {
                    if (stockPlace[i] < '0' || stockPlace[i] > '9')
                    {
                        label21.Text = "库存地址必须为整数！";
                        con.Close();
                        return;
                    }
                }
                for (int i = 0; i < price.Length; ++i)
                {
                    if ((price[i] < '0' || price[i] > '9') && price[i] != '.')
                    {
                        label21.Text = "售价必须为数字！";
                        con.Close();
                        return;
                    }
                }
                try
                {
                    String SqlInsertWarehouse = "insert into warehouse values ('" + textBox21.Text + "', '" + number + "');" +
                         "insert into stock values ('" + str_id + "', '" + textBox21.Text + "');" +
                         "update book set price = '" + textBox22.Text + "' where book_id = '" + str_id + "';";

                    SqlCommand com2 = new SqlCommand(SqlInsertWarehouse, con);
                    int i = com2.ExecuteNonQuery();
                    if (i < 3)
                    {
                        label21.Text = "插入库存或添加零售价格失败！";
                        con.Close();
                        return;
                    }
                    else
                    {
                        label21.Text = "新书到库，上架成功！";
                    }
                }
                catch (Exception)
                {
                    label21.Text = "插入库存和添加零售价格失败！";
                    con.Close();
                    return;
                }
            }
            //有这本书的库存，更新库存
            else
            {
                try
                {
                    String SqlUpdateWarehouse = "update warehouse set quantity = quantity + " + number + "where  address in (select address from stock where book_id= '" + str_id + "');";

                    SqlCommand com3 = new SqlCommand(SqlUpdateWarehouse, con);
                    int i = com3.ExecuteNonQuery();
                    if (i == 0)
                    {
                        label21.Text = "库存存在该书籍，但是更新失败！";
                        con.Close();
                        return;
                    }
                    else
                    {
                        label21.Text = "到库成功！";
                    }
                }
                catch (Exception)
                {
                    label21.Text = "库存量更新失败！";
                    con.Close();
                    return;
                }
            }

            //更新状态为已到达、上架
            try
            {
                String SqlUpdateBill = "update buy set _status = 'arrival' where bill_no = '" + textBox20.Text + "';";
                SqlCommand com4 = new SqlCommand(SqlUpdateBill, con);
                com4.ExecuteNonQuery();
            }
            catch (Exception)
            {
                label21.Text = "上架失败！";
            }
            con.Close();

            Show_buy("buy");
            return;
        }

        private void Bt_sell_Click(object sender, EventArgs e)//图书销售
        {
            Bt_close_Click(sender, e);
            label0.Hide();
            label23.Show();
            textBox23.Show();//书籍编号
            label24.Show();
            textBox24.Show();//售出数量
            button7.Show();
            label21.Show();
            label21.Text = "这里显示状态信息";
            bt_close.Show();

            String SqlBook = "select book.book_id, book.isbn, book.title, book.pub_house, written_by.name as author, translate, country, year, price, quantity " +
            "from ((book left outer join written_by on book.book_id = written_by.book_id) left outer join stock on book.book_id = stock.book_id) left outer join warehouse on stock.address = warehouse.address where price > 0 and quantity > 0;";
            Search_book(SqlBook);
        }
        
        private void Button7_Click(object sender, EventArgs e)
        {
            if (textBox23.Text.Length.Equals(0) || textBox24.Text.Length.Equals(0))
            {
                label21.Text = "输入的值不能为空！";
                return;
            }
            string SellNumber = textBox24.Text;
            for (int i = 0; i < SellNumber.Length; ++i)
            {
                if (SellNumber[i] < '0' || SellNumber[i] > '9')
                {
                    label21.Text = "数量必须为正整数！";
                    return;
                }
            }
            if (Convert.ToInt32(textBox24.Text) <= 0)
            {
                label21.Text = "数量必须为正数！";
                return;
            }

            con.Open();
            String SqlSearchBook = "select count(*) from book where book_id = '" + textBox23.Text + "';";
            SqlCommand com0 = new SqlCommand(SqlSearchBook, con);
            //若返回参数等于0，则说明没有这本书
            if (Convert.ToInt32(com0.ExecuteScalar()) == 0)
            {
                label21.Text = "系统里没有该书籍！";
                con.Close();
                return;
            }
            String SqlSearchStock = "select sum(quantity) from warehouse, stock where book_id = '" + textBox23.Text + "';";
            SqlCommand com = new SqlCommand(SqlSearchStock, con);
            //若返回参数等于0，则说明没有这本书的库存，不卖了
            if (Convert.ToInt32(com.ExecuteScalar()) == 0)
            {
                label21.Text = "库存里没有这本书了！";
            }
            //有这本书的库存，更新库存
            else
            {
                //获取图书的零售价格
                String SqlSelect = "select price from book where book_id = '" + textBox23.Text + "';";
                SqlDataAdapter sd = new SqlDataAdapter(SqlSelect, con);
                DataTable dt = new DataTable();//新建表格
                sd.Fill(dt);//填装表格
                String price = dt.Rows[0][0].ToString();
                try
                {
                    String SqlUpdateWarehouse = "update warehouse set quantity = quantity - " + textBox24.Text + "where  address in (select address from stock where book_id= '" + textBox23.Text + "');" +
                    "insert into bill values(Convert(varchar(20),Getdate(),120), 'sell',  " + textBox24.Text + " * " + price + ", GETDATE(), GETDATE());" +
                    "insert into sell values(Convert(varchar(20),Getdate(),120), '" + textBox23.Text + "',  '" + textBox24.Text + "', '" + price + "');";

                    SqlCommand com2 = new SqlCommand(SqlUpdateWarehouse, con);
                    int i = com2.ExecuteNonQuery();
                    if (i < 3)
                    {
                        label21.Text = "售出失败！";
                    }
                    else
                    {
                        label21.Text = "售出成功！";
                    }
                }
                catch (Exception)
                {
                    label21.Text = "操作失败！";
                }
            }
            con.Close();
        }

        private void Bt_bill_Click(object sender, EventArgs e)//查看账单
        {
            Bt_close_Click(sender, e);
            label0.Hide();
            label25.Show();
            dateTimePicker1.Show();
            label26.Show();
            dateTimePicker2.Show();
            label27.Show();
            comboBox1.Show();
            button8.Show();
            label21.Show();
            label21.Text = "这里显示状态信息";
            bt_close.Show();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "进货情况") Show_bill("pend");
            else if (comboBox1.Text == "退货情况") Show_bill("cancel");
            else if (comboBox1.Text == "查看收入") Show_bill("sell");
            else if (comboBox1.Text == "查看支出") Show_bill("buy");
            else { label21.Text = "请不要不选择或者选其他项！"; }
        }

        private void Show_bill(string action)
        {//辅助函数，查看账单
            con.Open();
            string SqlShowBill;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            string _from = dateTimePicker1.Value.ToString();
            string _to = dateTimePicker2.Value.ToString();
            
            if (action.Equals("buy") || action.Equals("sell"))
            {
                SqlShowBill = "select _date, _time, bill_no, total from bill where action = '" + action;
                SqlShowBill += "' and _date >= '" + _from + "' and _date <= '" + _to + "';";
                SqlDataAdapter sd = new SqlDataAdapter(SqlShowBill, con);
                DataTable dt = new DataTable();//新建表格
                sd.Fill(dt);//填装表格
                //修改列名
                dt.Columns[0].ColumnName = "交易日期";
                dt.Columns[1].ColumnName = "交易时间";
                dt.Columns[2].ColumnName = "账单编号";
                dt.Columns[3].ColumnName = "总金额";
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[2].Width = 150;
            }
            else if (action.Equals("pend") || action.Equals("cancel"))
            {
                SqlShowBill = "select _date, _time, bill.bill_no, book_id, number, price from bill, buy where bill.bill_no = buy.bill_no and _status = '" + action;
                SqlShowBill += "' and _date >= '" + _from + "' and _date <= '" + _to + "';";
                SqlDataAdapter sd = new SqlDataAdapter(SqlShowBill, con);
                DataTable dt = new DataTable();//新建表格
                sd.Fill(dt);//填装表格
                //修改列名
                dt.Columns[0].ColumnName = "交易日期";
                dt.Columns[1].ColumnName = "交易时间";
                dt.Columns[2].ColumnName = "账单编号";
                dt.Columns[3].ColumnName = "图书编号";
                dt.Columns[4].ColumnName = "数量";
                dt.Columns[5].ColumnName = "单价";
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[2].Width = 150;
            }
            con.Close();
        }
        
        private void Bt_close_Click(object sender, EventArgs e)//隐藏查询界面
        {
            label0.Show();//欢迎使用

            label1.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
            textBox1.Hide();
            checkBox1.Hide();
            checkBox2.Hide();
            checkBox3.Hide();
            checkBox4.Hide();
            checkBox5.Hide();
            button1.Hide();

            label4.Hide();
            textBox2.Hide();
            label5.Hide();
            textBox3.Hide();
            label6.Hide();
            textBox4.Hide();
            label7.Hide();
            textBox5.Hide();
            label8.Hide();
            textBox6.Hide();
            label9.Hide();
            textBox7.Hide();
            label10.Hide();
            textBox8.Hide();
            label11.Hide();
            textBox9.Hide();
            textBox10.Hide();
            textBox11.Hide();
            checkBox6.Hide();
            checkBox7.Hide();
            textBox12.Hide();
            textBox13.Hide();
            checkBox8.Hide();
            textBox14.Hide();
            textBox15.Hide();
            checkBox9.Hide();
            button2.Hide();
            label12.Hide();
            label13.Hide();
            label14.Hide();

            label15.Hide();
            textBox16.Hide();
            label16.Hide();
            textBox17.Hide();
            label17.Hide();
            textBox18.Hide();
            label18.Hide();
            textBox19.Hide();
            button3.Hide();
            label19.Hide();

            label20.Hide();
            textBox20.Hide();
            button4.Hide();
            button5.Hide();
            label21.Hide();

            textBox21.Hide();
            textBox22.Hide();
            label22.Hide();
            button6.Hide();

            label23.Hide();
            textBox23.Hide();
            label24.Hide();
            textBox24.Hide();
            button7.Hide();

            label25.Hide();
            dateTimePicker1.Hide();
            label26.Hide();
            dateTimePicker2.Hide();
            label27.Hide();
            comboBox1.Hide();
            button8.Hide();

            bt_close.Hide();
        }

        private void Bt_logout_Click(object sender, EventArgs e)//登出
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
            this.Close();
        }

        private void Bt_exit_Click(object sender, EventArgs e)//退出
        {
            this.Dispose();
            this.Close();
        }
    }
}