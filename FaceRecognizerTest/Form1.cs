using FaceRecognizer;
using FaceRecognizer.Api;
using FaceRecognizer.SM4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace FaceRecognizerTest
{
    public partial class Form1 : Form
    {
        Config config = new Config();

        public Form1()
        {
            InitializeComponent();
            config.timeOut = 60;
            config.detectFaceScaleVal = 4;
            config.cameraIndex = 0;
            config.apiUrl = "https://smart.hsop.site:18120/tbs/tbs_001";
        }

        private void btnFaceDetect_Click(object sender, EventArgs e)
        {

            FaceRecognizer.Invoke.FaceDetect(config);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.idType = "EDE1D136EBF001D47D3F1B527B9F92CA0";// "01";
            userInfo.idNo = "EF4C6A66F4A13DA5F6DA4CD138C95784D7C023131BF0478E12937D418468064F";// "430111200909081234";
            userInfo.name = "751DDE65ADC44E1B7E6EFB66EDD2FB7E";// "张学友";
            userInfo.mobile = "D4D1B92F4141786FE9F15856690A6CF4";// "13512345678";
            userInfo.cardNo = "EB6E382C99DB8140F92321E5870919E0";// "12345"
            FaceRecognizer.Invoke.FaceRegister(config, userInfo);
        }

        private void btnRecognize_Click(object sender, EventArgs e)
        {
            FaceRecognizer.Invoke.FaceRecognize(config);
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string key = "EF7E26F248CFB37D256E3F0AB40BFFF8";
            string iv = "";
            txtEncryp.Text = SM4Util.EncryptECB(txtPlain.Text, key, iv);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string key = "EF7E26F248CFB37D256E3F0AB40BFFF8";
            string iv = "";
            txtEncryp.Text = SM4Util.DecryptECB(txtPlain.Text, key);
        }
    }
}
