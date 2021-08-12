namespace library
{
    partial class account
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_staff_id = new System.Windows.Forms.Label();
            this.lb_pwd = new System.Windows.Forms.Label();
            this.lb_pwd2 = new System.Windows.Forms.Label();
            this.lb_usr_name = new System.Windows.Forms.Label();
            this.lb_name = new System.Windows.Forms.Label();
            this.lb_phone = new System.Windows.Forms.Label();
            this.lb_gender = new System.Windows.Forms.Label();
            this.lb_age = new System.Windows.Forms.Label();
            this.tb_staff_id = new System.Windows.Forms.TextBox();
            this.tb_pwd = new System.Windows.Forms.TextBox();
            this.tb_pwd2 = new System.Windows.Forms.TextBox();
            this.tb_usr_name = new System.Windows.Forms.TextBox();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.tb_phone = new System.Windows.Forms.TextBox();
            this.bt_update = new System.Windows.Forms.Button();
            this.bt_exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_gender = new System.Windows.Forms.ComboBox();
            this.tb_age = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lb_staff_id
            // 
            this.lb_staff_id.AutoSize = true;
            this.lb_staff_id.Location = new System.Drawing.Point(152, 52);
            this.lb_staff_id.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_staff_id.Name = "lb_staff_id";
            this.lb_staff_id.Size = new System.Drawing.Size(73, 21);
            this.lb_staff_id.TabIndex = 0;
            this.lb_staff_id.Text = "工号：";
            // 
            // lb_pwd
            // 
            this.lb_pwd.AutoSize = true;
            this.lb_pwd.Location = new System.Drawing.Point(152, 96);
            this.lb_pwd.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_pwd.Name = "lb_pwd";
            this.lb_pwd.Size = new System.Drawing.Size(73, 21);
            this.lb_pwd.TabIndex = 0;
            this.lb_pwd.Text = "密码：";
            // 
            // lb_pwd2
            // 
            this.lb_pwd2.AutoSize = true;
            this.lb_pwd2.Location = new System.Drawing.Point(108, 140);
            this.lb_pwd2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_pwd2.Name = "lb_pwd2";
            this.lb_pwd2.Size = new System.Drawing.Size(115, 21);
            this.lb_pwd2.TabIndex = 0;
            this.lb_pwd2.Text = "确认密码：";
            // 
            // lb_usr_name
            // 
            this.lb_usr_name.AutoSize = true;
            this.lb_usr_name.Location = new System.Drawing.Point(130, 184);
            this.lb_usr_name.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_usr_name.Name = "lb_usr_name";
            this.lb_usr_name.Size = new System.Drawing.Size(94, 21);
            this.lb_usr_name.TabIndex = 0;
            this.lb_usr_name.Text = "用户名：";
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(152, 228);
            this.lb_name.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(73, 21);
            this.lb_name.TabIndex = 0;
            this.lb_name.Text = "姓名：";
            // 
            // lb_phone
            // 
            this.lb_phone.AutoSize = true;
            this.lb_phone.Location = new System.Drawing.Point(108, 271);
            this.lb_phone.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_phone.Name = "lb_phone";
            this.lb_phone.Size = new System.Drawing.Size(115, 21);
            this.lb_phone.TabIndex = 0;
            this.lb_phone.Text = "电话号码：";
            // 
            // lb_gender
            // 
            this.lb_gender.AutoSize = true;
            this.lb_gender.Location = new System.Drawing.Point(152, 315);
            this.lb_gender.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_gender.Name = "lb_gender";
            this.lb_gender.Size = new System.Drawing.Size(73, 21);
            this.lb_gender.TabIndex = 0;
            this.lb_gender.Text = "性别：";
            // 
            // lb_age
            // 
            this.lb_age.AutoSize = true;
            this.lb_age.Location = new System.Drawing.Point(152, 359);
            this.lb_age.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lb_age.Name = "lb_age";
            this.lb_age.Size = new System.Drawing.Size(73, 21);
            this.lb_age.TabIndex = 0;
            this.lb_age.Text = "年龄：";
            // 
            // tb_staff_id
            // 
            this.tb_staff_id.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tb_staff_id.Enabled = false;
            this.tb_staff_id.Location = new System.Drawing.Point(238, 47);
            this.tb_staff_id.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_staff_id.Name = "tb_staff_id";
            this.tb_staff_id.Size = new System.Drawing.Size(180, 31);
            this.tb_staff_id.TabIndex = 1;
            // 
            // tb_pwd
            // 
            this.tb_pwd.Location = new System.Drawing.Point(238, 91);
            this.tb_pwd.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_pwd.Name = "tb_pwd";
            this.tb_pwd.PasswordChar = '*';
            this.tb_pwd.Size = new System.Drawing.Size(180, 31);
            this.tb_pwd.TabIndex = 1;
            // 
            // tb_pwd2
            // 
            this.tb_pwd2.Location = new System.Drawing.Point(238, 135);
            this.tb_pwd2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_pwd2.Name = "tb_pwd2";
            this.tb_pwd2.PasswordChar = '*';
            this.tb_pwd2.Size = new System.Drawing.Size(180, 31);
            this.tb_pwd2.TabIndex = 1;
            // 
            // tb_usr_name
            // 
            this.tb_usr_name.BackColor = System.Drawing.SystemColors.Window;
            this.tb_usr_name.Location = new System.Drawing.Point(238, 178);
            this.tb_usr_name.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_usr_name.Name = "tb_usr_name";
            this.tb_usr_name.Size = new System.Drawing.Size(180, 31);
            this.tb_usr_name.TabIndex = 1;
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(238, 222);
            this.tb_name.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(180, 31);
            this.tb_name.TabIndex = 1;
            // 
            // tb_phone
            // 
            this.tb_phone.Location = new System.Drawing.Point(238, 266);
            this.tb_phone.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_phone.Name = "tb_phone";
            this.tb_phone.Size = new System.Drawing.Size(180, 31);
            this.tb_phone.TabIndex = 1;
            // 
            // bt_update
            // 
            this.bt_update.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_update.Location = new System.Drawing.Point(193, 452);
            this.bt_update.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.bt_update.Name = "bt_update";
            this.bt_update.Size = new System.Drawing.Size(138, 40);
            this.bt_update.TabIndex = 2;
            this.bt_update.Text = "修改";
            this.bt_update.UseVisualStyleBackColor = true;
            this.bt_update.Click += new System.EventHandler(this.Button1_Click);
            // 
            // bt_exit
            // 
            this.bt_exit.BackColor = System.Drawing.SystemColors.ControlLight;
            this.bt_exit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_exit.Location = new System.Drawing.Point(193, 534);
            this.bt_exit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.bt_exit.Name = "bt_exit";
            this.bt_exit.Size = new System.Drawing.Size(138, 40);
            this.bt_exit.TabIndex = 2;
            this.bt_exit.Text = "退出";
            this.bt_exit.UseVisualStyleBackColor = false;
            this.bt_exit.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(235, 410);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "修改失败！";
            this.label1.Visible = false;
            // 
            // tb_gender
            // 
            this.tb_gender.FormattingEnabled = true;
            this.tb_gender.Items.AddRange(new object[] {
            "male",
            "female"});
            this.tb_gender.Location = new System.Drawing.Point(238, 310);
            this.tb_gender.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_gender.Name = "tb_gender";
            this.tb_gender.Size = new System.Drawing.Size(180, 29);
            this.tb_gender.TabIndex = 5;
            // 
            // tb_age
            // 
            this.tb_age.FormattingEnabled = true;
            this.tb_age.Items.AddRange(new object[] {
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60"});
            this.tb_age.Location = new System.Drawing.Point(238, 354);
            this.tb_age.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_age.Name = "tb_age";
            this.tb_age.Size = new System.Drawing.Size(180, 29);
            this.tb_age.TabIndex = 6;
            // 
            // account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 637);
            this.Controls.Add(this.tb_age);
            this.Controls.Add(this.tb_gender);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_exit);
            this.Controls.Add(this.bt_update);
            this.Controls.Add(this.tb_usr_name);
            this.Controls.Add(this.tb_pwd2);
            this.Controls.Add(this.tb_phone);
            this.Controls.Add(this.tb_name);
            this.Controls.Add(this.tb_pwd);
            this.Controls.Add(this.tb_staff_id);
            this.Controls.Add(this.lb_age);
            this.Controls.Add(this.lb_gender);
            this.Controls.Add(this.lb_phone);
            this.Controls.Add(this.lb_name);
            this.Controls.Add(this.lb_usr_name);
            this.Controls.Add(this.lb_pwd2);
            this.Controls.Add(this.lb_pwd);
            this.Controls.Add(this.lb_staff_id);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "account";
            this.Text = "account";
            this.Load += new System.EventHandler(this.account_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_staff_id;
        private System.Windows.Forms.Label lb_pwd;
        private System.Windows.Forms.Label lb_pwd2;
        private System.Windows.Forms.Label lb_usr_name;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Label lb_phone;
        private System.Windows.Forms.Label lb_gender;
        private System.Windows.Forms.Label lb_age;
        private System.Windows.Forms.TextBox tb_staff_id;
        private System.Windows.Forms.TextBox tb_pwd;
        private System.Windows.Forms.TextBox tb_pwd2;
        private System.Windows.Forms.TextBox tb_usr_name;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.TextBox tb_phone;
        private System.Windows.Forms.Button bt_update;
        private System.Windows.Forms.Button bt_exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tb_gender;
        private System.Windows.Forms.ComboBox tb_age;
    }
}