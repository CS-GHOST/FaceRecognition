namespace FaceRecognitionTest
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
            this.txtFaceResult = new System.Windows.Forms.TextBox();
            this.btnFaceDetect = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnRecognition = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFaceResult
            // 
            this.txtFaceResult.Location = new System.Drawing.Point(6, 20);
            this.txtFaceResult.Multiline = true;
            this.txtFaceResult.Name = "txtFaceResult";
            this.txtFaceResult.Size = new System.Drawing.Size(634, 291);
            this.txtFaceResult.TabIndex = 3;
            // 
            // btnFaceDetect
            // 
            this.btnFaceDetect.Location = new System.Drawing.Point(45, 40);
            this.btnFaceDetect.Name = "btnFaceDetect";
            this.btnFaceDetect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnFaceDetect.Size = new System.Drawing.Size(91, 36);
            this.btnFaceDetect.TabIndex = 2;
            this.btnFaceDetect.Text = "获取人脸图像";
            this.btnFaceDetect.UseVisualStyleBackColor = true;
            this.btnFaceDetect.Click += new System.EventHandler(this.btnFaceDetect_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(221, 40);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(90, 36);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Text = "人脸注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnRecognition
            // 
            this.btnRecognition.Location = new System.Drawing.Point(397, 40);
            this.btnRecognition.Name = "btnRecognition";
            this.btnRecognition.Size = new System.Drawing.Size(93, 36);
            this.btnRecognition.TabIndex = 5;
            this.btnRecognition.Text = "人脸识别";
            this.btnRecognition.UseVisualStyleBackColor = true;
            this.btnRecognition.Click += new System.EventHandler(this.btnRecognition_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFaceDetect);
            this.groupBox1.Controls.Add(this.btnRecognition);
            this.groupBox1.Controls.Add(this.btnRegister);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(646, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFaceResult);
            this.groupBox2.Location = new System.Drawing.Point(12, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(646, 317);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 460);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "人脸识别模块测试工具";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFaceResult;
        private System.Windows.Forms.Button btnFaceDetect;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnRecognition;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

