using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Xin : System.Web.UI.Page
    {
        public static StringBuilder CssContainerStr = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {            
            CssContainerStr.Clear();            
            CssContainerStr.Append(@"                         
                   .container {         
                               position: relative;
                               overflow: hidden;
                               background-size: 100% 100%;
                               background-repeat: no-repeat;                               
                               width: {0}px;
                               height: {1}px;
                               background-image:url('{2}')
                               }");  
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.Panel1.BackImageUrl = "";

            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            this.Panel4.Visible = false;
            this.TimeTotal.Visible = false;
            this.Button3.Visible = false;
            this.Button6.Visible = false;     
            this.Inp.Visible = true;            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.Panel1.Width = Convert.ToInt32(this.Wid.Text);
            this.Panel1.Height = Convert.ToInt32(this.Hei.Text);

            CssContainerStr.Replace("{0}", this.Wid.Text);
            CssContainerStr.Replace("{1}", this.Hei.Text);
            this.Panel1.Visible = true;
            this.Button3.Visible = true;          
            this.Panel2.Visible = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            this.TimeTotal.Visible = true;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            //这样循环，可以同时上传多个文件。前台已经有文件格式的判断，有错误提示了。这里只要过滤掉非法文件即可，无需提示了。
            if (FileUpload1.HasFiles)
            {
                string ex = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                if (".jpg.gif.png.bmp".Contains(ex))
                {
                    string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(100, 999).ToString() + ex;
                    string path = Server.MapPath("/UploadImg/" + newFileName);
                    FileUpload1.PostedFile.SaveAs(path);                   
                    this.Panel1.BackImageUrl = "/UploadImg/" + newFileName;
                    CssContainerStr.Replace("{2}","/UploadImg/" + newFileName);
                    this.Button6.Visible=true;                   
                }
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            this.Panel3.Visible = true;
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            this.TextBox3.Text = CssContainerStr.ToString();
            this.Panel4.Visible = true;
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            //这样循环，可以同时上传多个文件。前台已经有文件格式的判断，有错误提示了。这里只要过滤掉非法文件即可，无需提示了。
            if (FileUpload2.HasFiles)
            {
                string ex = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                if (".jpg.gif.png.bmp".Contains(ex))
                {
                    string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + r.Next(100, 999).ToString() + ex;
                    string path = Server.MapPath("/UploadImg/" + newFileName);
                    FileUpload1.PostedFile.SaveAs(path);
                    //this.Panel1.BackImageUrl = "/UploadImg/" + newFileName;                   
                    this.Button6.Visible = true;
                }
            }
        }
    }
}