namespace library
{
    partial class Login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_staff_id = new System.Windows.Forms.TextBox();
            this.tb_pwd = new System.Windows.Forms.TextBox();
            this.bt_login = new System.Windows.Forms.Button();
            this.label_staff_id = new System.Windows.Forms.Label();
            this.label_pwd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_staff_id
            // 
            this.tb_staff_id.Location = new System.Drawing.Point(277, 133);
            this.tb_staff_id.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_staff_id.Name = "tb_staff_id";
            this.tb_staff_id.Size = new System.Drawing.Size(180, 31);
            this.tb_staff_id.TabIndex = 0;
            // 
            // tb_pwd
            // 
            this.tb_pwd.Location = new System.Drawing.Point(277, 215);
            this.tb_pwd.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.tb_pwd.Name = "tb_pwd";
            this.tb_pwd.PasswordChar = '*';
            this.tb_pwd.Size = new System.Drawing.Size(180, 31);
            this.tb_pwd.TabIndex = 1;
            // 
            // bt_login
            // 
            this.bt_login.Location = new System.Drawing.Point(158, 350);
            this.bt_login.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.bt_login.Name = "bt_login";
            this.bt_login.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bt_login.Size = new System.Drawing.Size(303, 40);
            this.bt_login.TabIndex = 2;
            this.bt_login.Text = "登      录";
            this.bt_login.UseVisualStyleBackColor = true;
            this.bt_login.Click += new System.EventHandler(this.bt_login_Click);
            // 
            // label_staff_id
            // 
            this.label_staff_id.AutoSize = true;
            this.label_staff_id.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_staff_id.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_staff_id.Font = new System.Drawing.Font("宋体", 10F);
            this.label_staff_id.Location = new System.Drawing.Point(158, 136);
            this.label_staff_id.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_staff_id.Name = "label_staff_id";
            this.label_staff_id.Size = new System.Drawing.Size(84, 26);
            this.label_staff_id.TabIndex = 3;
            this.label_staff_id.Text = "账  号";
            // 
            // label_pwd
            // 
            this.label_pwd.AutoSize = true;
            this.label_pwd.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label_pwd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_pwd.Font = new System.Drawing.Font("宋体", 10F);
            this.label_pwd.Location = new System.Drawing.Point(158, 219);
            this.label_pwd.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_pwd.Name = "label_pwd";
            this.label_pwd.Size = new System.Drawing.Size(84, 26);
            this.label_pwd.TabIndex = 4;
            this.label_pwd.Text = "密  码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(273, 273);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "账号或密码错误！";
            this.label1.Visible = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(631, 562);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_pwd);
            this.Controls.Add(this.label_staff_id);
            this.Controls.Add(this.bt_login);
            this.Controls.Add(this.tb_pwd);
            this.Controls.Add(this.tb_staff_id);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Login";
            this.ShowInTaskbar = false;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_staff_id;
        private System.Windows.Forms.TextBox tb_pwd;
        private System.Windows.Forms.Button bt_login;
        private System.Windows.Forms.Label label_staff_id;
        private System.Windows.Forms.Label label_pwd;
        public string Login_id;
        private System.Windows.Forms.Label label1;
    }
}

