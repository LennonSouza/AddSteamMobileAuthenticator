namespace AddSteamMobileAuthenticator
{
    partial class Frm_Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Main));
            txt_API = new TextBox();
            txt_UrlPhone = new TextBox();
            txt_UrlMail = new TextBox();
            txt_Country = new TextBox();
            lbl_API = new Label();
            lbl_UrlPhone = new Label();
            lbl_UrlMail = new Label();
            lbl_Country = new Label();
            richtxt_Console = new RichTextBox();
            btn_Start = new Button();
            SuspendLayout();
            // 
            // txt_API
            // 
            txt_API.Location = new Point(66, 12);
            txt_API.Name = "txt_API";
            txt_API.PlaceholderText = "sf4s6df46sdf486s4f16584646";
            txt_API.Size = new Size(136, 23);
            txt_API.TabIndex = 0;
            // 
            // txt_UrlPhone
            // 
            txt_UrlPhone.Location = new Point(283, 12);
            txt_UrlPhone.Name = "txt_UrlPhone";
            txt_UrlPhone.PlaceholderText = "onlinesim.io";
            txt_UrlPhone.Size = new Size(136, 23);
            txt_UrlPhone.TabIndex = 1;
            // 
            // txt_UrlMail
            // 
            txt_UrlMail.Location = new Point(66, 39);
            txt_UrlMail.Name = "txt_UrlMail";
            txt_UrlMail.PlaceholderText = "gmail.com";
            txt_UrlMail.Size = new Size(136, 23);
            txt_UrlMail.TabIndex = 2;
            // 
            // txt_Country
            // 
            txt_Country.Location = new Point(283, 38);
            txt_Country.Name = "txt_Country";
            txt_Country.PlaceholderText = "RU";
            txt_Country.Size = new Size(41, 23);
            txt_Country.TabIndex = 3;
            // 
            // lbl_API
            // 
            lbl_API.AutoSize = true;
            lbl_API.Location = new Point(12, 15);
            lbl_API.Name = "lbl_API";
            lbl_API.Size = new Size(28, 15);
            lbl_API.TabIndex = 4;
            lbl_API.Text = "Api:";
            // 
            // lbl_UrlPhone
            // 
            lbl_UrlPhone.AutoSize = true;
            lbl_UrlPhone.Location = new Point(208, 15);
            lbl_UrlPhone.Name = "lbl_UrlPhone";
            lbl_UrlPhone.Size = new Size(62, 15);
            lbl_UrlPhone.TabIndex = 5;
            lbl_UrlPhone.Text = "Url Phone:";
            // 
            // lbl_UrlMail
            // 
            lbl_UrlMail.AutoSize = true;
            lbl_UrlMail.Location = new Point(12, 41);
            lbl_UrlMail.Name = "lbl_UrlMail";
            lbl_UrlMail.Size = new Size(51, 15);
            lbl_UrlMail.TabIndex = 6;
            lbl_UrlMail.Text = "Url Mail:";
            // 
            // lbl_Country
            // 
            lbl_Country.AutoSize = true;
            lbl_Country.Location = new Point(208, 41);
            lbl_Country.Name = "lbl_Country";
            lbl_Country.Size = new Size(53, 15);
            lbl_Country.TabIndex = 7;
            lbl_Country.Text = "Country:";
            // 
            // richtxt_Console
            // 
            richtxt_Console.Location = new Point(12, 70);
            richtxt_Console.Name = "richtxt_Console";
            richtxt_Console.Size = new Size(407, 160);
            richtxt_Console.TabIndex = 8;
            richtxt_Console.Text = "";
            // 
            // btn_Start
            // 
            btn_Start.FlatStyle = FlatStyle.Flat;
            btn_Start.Location = new Point(330, 39);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(89, 23);
            btn_Start.TabIndex = 9;
            btn_Start.Text = "Start";
            btn_Start.UseVisualStyleBackColor = true;
            btn_Start.Click += btn_Start_Click;
            // 
            // Frm_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(431, 240);
            Controls.Add(btn_Start);
            Controls.Add(richtxt_Console);
            Controls.Add(lbl_Country);
            Controls.Add(lbl_UrlMail);
            Controls.Add(lbl_UrlPhone);
            Controls.Add(lbl_API);
            Controls.Add(txt_Country);
            Controls.Add(txt_UrlMail);
            Controls.Add(txt_UrlPhone);
            Controls.Add(txt_API);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(495, 472);
            Name = "Frm_Main";
            Text = "Automatic Steam Mobile Authenticate";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lbl_API;
        private Label lbl_UrlPhone;
        private Label lbl_UrlMail;
        private Label lbl_Country;
        private Button btn_Start;
        public RichTextBox richtxt_Console;
        public TextBox txt_API;
        public TextBox txt_UrlPhone;
        public TextBox txt_UrlMail;
        public TextBox txt_Country;
    }
}