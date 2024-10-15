using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;

namespace FaceRecognitionTest
{
    public partial class FrmParams : Form
    {
        public object objType = null;

        public FrmParams()
        {
            InitializeComponent();
        }

        private void FrmParams_Load(object sender, EventArgs e)
        {
            this.ShowProperties(objType.GetType());
        }

        private void ShowProperties(Type tp)
        {
            string xmlPath = tp.Assembly.Location.ToLower().Replace(".dll", ".xml");
            XmlDocument doc = new XmlDocument();
            if (File.Exists(xmlPath))
            {
                doc.Load(xmlPath);
            }

            foreach (PropertyInfo item in tp.GetProperties())
            {
                #region 获取注释内容
                string des = string.Empty;
                if (doc != null)
                {
                    try
                    {
                        des = GetNodeValue(doc, "doc/members/member", "name", "P:" + tp.FullName + "." + item.Name, "summary");
                        des = des.Replace("/r/n", "").Trim();
                    }
                    catch { }
                }
                #endregion

                object value = item.GetValue(objType, null);
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    value = "";
                }
                if (item.PropertyType.BaseType == null)
                {
                    //object类型不处理。
                }
                else if (item.PropertyType.BaseType.Name == "Enum")
                {
                    DataGridViewComboBoxCell dcb = new DataGridViewComboBoxCell();

                    foreach (object item2 in Enum.GetValues(item.PropertyType))
                    {
                        dcb.Items.AddRange(item2.ToString());
                    }
                    dcb.Value = dcb.Items[0];

                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewTextBoxCell dtext = new DataGridViewTextBoxCell();
                    dtext.Value = item.Name;
                    newRow.Cells.Add(dtext);
                    dtext = new DataGridViewTextBoxCell();
                    dtext.Value = item.PropertyType.ToString().Substring(item.PropertyType.ToString().LastIndexOf(".") + 1);
                    newRow.Cells.Add(dtext);
                    newRow.Cells.Add(dcb);
                    dtext = new DataGridViewTextBoxCell();
                    dtext.Value = des;
                    newRow.Cells.Add(dtext);
                    dgvProperty.Rows.Add(newRow);
                }
                else if (item.PropertyType.Name == "DateTime")
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewTextBoxCell dtext = new DataGridViewTextBoxCell();
                    dtext.Value = item.Name;
                    newRow.Cells.Add(dtext);
                    dtext = new DataGridViewTextBoxCell();
                    dtext.Value = item.PropertyType.ToString().Substring(item.PropertyType.ToString().LastIndexOf(".") + 1);
                    newRow.Cells.Add(dtext);
                    dtext = new DataGridViewTextBoxCell();
                    dtext.Value = value == "" ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : value;
                    newRow.Cells.Add(dtext);
                    dtext = new DataGridViewTextBoxCell();
                    dtext.Value = des;
                    dgvProperty.Rows.Add(newRow);
                }
                else if (item.PropertyType.Name.Contains("Dictionary"))
                {

                }
                else
                {
                    this.dgvProperty.Rows.Add(new string[] { item.Name, item.PropertyType.ToString().Substring(item.PropertyType.ToString().LastIndexOf(".") + 1), value.ToString(), des });
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in this.dgvProperty.Rows)
                {
                    if (item.Cells[2].Value == null || item.Cells[2].Value.ToString() == "")
                    {
                        continue;
                    }
                    PropertyInfo inf = objType.GetType().GetProperty(item.Cells[0].Value.ToString());
                    if (inf.PropertyType.BaseType.Name == "Enum")
                    {
                        inf.SetValue(objType, Enum.Parse(inf.PropertyType, item.Cells[2].Value.ToString()), null);
                    }
                    else if (inf.PropertyType.Name == "DateTime")
                    {
                        inf.SetValue(objType, Convert.ToDateTime(item.Cells[2].Value.ToString()), null);
                    }
                    else if (inf.PropertyType.Name == "Decimal")
                    {
                        inf.SetValue(objType, Convert.ToDecimal(item.Cells[2].Value.ToString()), null);
                    }
                    else if (inf.PropertyType.Name == "Int32")
                    {
                        inf.SetValue(objType, Convert.ToInt32(item.Cells[2].Value.ToString()), null);
                    }
                    else
                    {
                        inf.SetValue(objType, (object)item.Cells[2].Value, null);
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 获取节点内容
        /// </summary>
        /// <param name="nodePath">节点路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        /// <param name="childPath">子节点路径</param>
        /// <returns>节点内容</returns>
        public string GetNodeValue(XmlDocument xmlDocument, string nodePath, string attributeName, string attributeValue, string childPath)
        {
            XmlNodeList xnl = xmlDocument.SelectNodes(nodePath);
            if (xnl == null || xnl.Count <= 0)
            {
                throw new Exception(nodePath + "节点不存在。");
            }
            XmlNode xmlNode = null;

            foreach (XmlNode node in xnl)
            {
                if (node.Attributes[attributeName] != null && node.Attributes[attributeName].Value == attributeValue)
                {
                    xmlNode = node;
                    break;
                }
            }
            if (xmlNode == null)
            {
                throw new Exception(nodePath + "节点不存在。");
            }
            else if (string.IsNullOrEmpty(childPath))
            {
                return xmlNode.InnerText;
            }
            else if (xmlNode.SelectSingleNode(childPath) != null)
            {
                return xmlNode.SelectSingleNode(childPath).InnerText;
            }
            else
            {
                throw new Exception(childPath + "子节点不存在。");
            }
        }
    }
}
