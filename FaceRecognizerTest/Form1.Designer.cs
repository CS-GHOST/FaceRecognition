namespace FaceRecognizerTest
{
    partial class Form1
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
            this.btnFaceDetect = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.face = new System.Windows.Forms.TabPage();
            this.txtFaceResult = new System.Windows.Forms.TextBox();
            this.sm4 = new System.Windows.Forms.TabPage();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.txtEncryp = new System.Windows.Forms.TextBox();
            this.txtPlain = new System.Windows.Forms.TextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnRecognize = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.face.SuspendLayout();
            this.sm4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFaceDetect
            // 
            this.btnFaceDetect.Location = new System.Drawing.Point(83, 24);
            this.btnFaceDetect.Name = "btnFaceDetect";
            this.btnFaceDetect.Size = new System.Drawing.Size(75, 23);
            this.btnFaceDetect.TabIndex = 0;
            this.btnFaceDetect.Text = "人脸检测";
            this.btnFaceDetect.UseVisualStyleBackColor = true;
            this.btnFaceDetect.Click += new System.EventHandler(this.btnFaceDetect_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.face);
            this.tabControl1.Controls.Add(this.sm4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(668, 385);
            this.tabControl1.TabIndex = 1;
            // 
            // face
            // 
            this.face.Controls.Add(this.btnRecognize);
            this.face.Controls.Add(this.btnRegister);
            this.face.Controls.Add(this.txtFaceResult);
            this.face.Controls.Add(this.btnFaceDetect);
            this.face.Location = new System.Drawing.Point(4, 22);
            this.face.Name = "face";
            this.face.Padding = new System.Windows.Forms.Padding(3);
            this.face.Size = new System.Drawing.Size(660, 359);
            this.face.TabIndex = 0;
            this.face.Text = "人脸识别";
            this.face.UseVisualStyleBackColor = true;
            // 
            // txtFaceResult
            // 
            this.txtFaceResult.Location = new System.Drawing.Point(39, 76);
            this.txtFaceResult.Multiline = true;
            this.txtFaceResult.Name = "txtFaceResult";
            this.txtFaceResult.Size = new System.Drawing.Size(583, 249);
            this.txtFaceResult.TabIndex = 1;
            // 
            // sm4
            // 
            this.sm4.Controls.Add(this.btnDecrypt);
            this.sm4.Controls.Add(this.txtEncryp);
            this.sm4.Controls.Add(this.txtPlain);
            this.sm4.Controls.Add(this.btnEncrypt);
            this.sm4.Location = new System.Drawing.Point(4, 22);
            this.sm4.Name = "sm4";
            this.sm4.Padding = new System.Windows.Forms.Padding(3);
            this.sm4.Size = new System.Drawing.Size(660, 359);
            this.sm4.TabIndex = 1;
            this.sm4.Text = "SM4";
            this.sm4.UseVisualStyleBackColor = true;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(269, 155);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 3;
            this.btnDecrypt.Text = "解密";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // txtEncryp
            // 
            this.txtEncryp.Location = new System.Drawing.Point(17, 217);
            this.txtEncryp.Multiline = true;
            this.txtEncryp.Name = "txtEncryp";
            this.txtEncryp.Size = new System.Drawing.Size(622, 108);
            this.txtEncryp.TabIndex = 2;
            // 
            // txtPlain
            // 
            this.txtPlain.Location = new System.Drawing.Point(17, 7);
            this.txtPlain.Multiline = true;
            this.txtPlain.Name = "txtPlain";
            this.txtPlain.Size = new System.Drawing.Size(622, 108);
            this.txtPlain.TabIndex = 1;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(87, 155);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 0;
            this.btnEncrypt.Text = "加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(208, 24);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 2;
            this.btnRegister.Text = "注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnRecognize
            // 
            this.btnRecognize.Location = new System.Drawing.Point(347, 24);
            this.btnRecognize.Name = "btnRecognize";
            this.btnRecognize.Size = new System.Drawing.Size(75, 23);
            this.btnRecognize.TabIndex = 3;
            this.btnRecognize.Text = "识别";
            this.btnRecognize.UseVisualStyleBackColor = true;
            this.btnRecognize.Click += new System.EventHandler(this.btnRecognize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 409);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.face.ResumeLayout(false);
            this.face.PerformLayout();
            this.sm4.ResumeLayout(false);
            this.sm4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFaceDetect;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage face;
        private System.Windows.Forms.TabPage sm4;
        private System.Windows.Forms.TextBox txtEncryp;
        private System.Windows.Forms.TextBox txtPlain;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.TextBox txtFaceResult;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnRecognize;
    }
}

