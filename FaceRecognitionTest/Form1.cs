using FaceRecognition.Entity;
using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace FaceRecognitionTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFaceDetect_Click(object sender, EventArgs e)
        {
            FaceResult result = FaceRecognition.Invoke.FaceDetect();
            this.txtFaceResult.Text = new JavaScriptSerializer().Serialize(result);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = new UserInfo();
            FrmParams frmPa = new FrmParams();
            frmPa.objType = userInfo;
            DialogResult re = frmPa.ShowDialog();
            if (re == DialogResult.OK)
            {
                FaceResult result = FaceRecognition.Invoke.FaceRegister(userInfo);
                this.txtFaceResult.Text = new JavaScriptSerializer().Serialize(result);
            }
        }

        private void btnRecognition_Click(object sender, EventArgs e)
        {
            FaceResult result = FaceRecognition.Invoke.FaceRecognize();
            this.txtFaceResult.Text = new JavaScriptSerializer().Serialize(result);
        }
    }
}
