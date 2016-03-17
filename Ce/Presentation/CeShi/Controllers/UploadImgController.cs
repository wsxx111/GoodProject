using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;

namespace CeShi.Controllers
{
    public class UploadImgController : Controller
    {      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveUploadSource(HttpPostedFileBase Filedata)
        {
            var actionResult = default(ContentResult);
            bool flag = false;
            var message = "";
            var filetype = "";
            var filesize = "";
            var filewidth = "";
            var fileheight = "";
            var fileurl = "";
            var newid = Guid.NewGuid().ToString("N");
            try
            {
                // 如果没有上传文件
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    message = "找不到要上传的文件，请重新上传";
                }
                else
                {
                    // 保存到  指定位置                   
                    string fileExtension = Filedata.FileName.Substring(Filedata.FileName.LastIndexOf('.'));

                    string fullFileName = newid + fileExtension;
                    // 文件系统不能使用虚拟路径
                    string uploadPath = Server.MapPath("~/Files/Upload/" + DateTime.Now.ToString("yyyyMMdd") + "/");
                    Directory.CreateDirectory(uploadPath);
                    Filedata.SaveAs(uploadPath + fullFileName);
                    System.Drawing.Image image = System.Drawing.Image.FromFile(uploadPath + fullFileName);
                    filewidth = image.Width.ToString(); ;
                    fileheight = image.Height.ToString();
                    filetype = Filedata.FileName.Substring(Filedata.FileName.LastIndexOf('.') + 1).ToUpper(); ;
                    filesize = Filedata.ContentLength.ToString();
                    fileurl =ConfigurationManager.AppSettings["WebDomain"]+ "/Files/Upload/" + DateTime.Now.ToString("yyyyMMdd") + "/" + fullFileName;
                    message = "上传成功";
                    flag = true;
                }
                actionResult = new ContentResult
                {
                    Content = new JavaScriptSerializer().Serialize(new
                    {
                        Message = message,
                        Flag = flag ? 1 : 0,
                        ftype = filetype,
                        image_width = filewidth,
                        image_height = fileheight,
                        fsize = filesize,
                        furl = fileurl,
                        fid = newid
                    })
                };
            }
            catch (Exception ex)
            {
                message = "上传遇到问题，请重试，问题描述：" + ex.Message;
                flag = false;
            }
            return actionResult;
        }




        //第二种
        #region 图片上传

        public ActionResult Preview(string baseimg)
        {
            //string url = "http://imgtest.fang.com/Upload/UploadFile";
            //string url = "http://imgu.soufun.com/upload/pic?channel=zhongchou.pic&city=&isflash=y";
            string url = "http://imgu.fang.com/upload/pic?isflash=y&channel=kanli.pic&city=";//低于5M大小的图片 

            string html = PostData3(url, 1000000, "filedata", @"D:\upload\123.jpg", baseimg.Split(',')[1]);//方法3

            return this.Json(new { Response = html });
        }
        #region 方法三
        public string PostData3(string url, int timeOut, string fileKeyName, string filePath, string base64str)
        {
            string responseContent;
            var memStream = new MemoryStream();
            var webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            // 边界符
            var boundary = "------------" + DateTime.Now.Ticks.ToString("x");
            // 编码边界符
            var firstBoundary = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");//拼接第一个form表单值用到
            var encodingBoundary = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            var endBoundary = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            MemoryStream fileStream = new MemoryStream(Convert.FromBase64String(base64str)); //base64编码方式
            //var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);//本地读取方式

            // 设置属性
            webRequest.Method = "POST";
            webRequest.Timeout = timeOut;
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

            #region 第一个form key-value 值  非必须
            //memStream.Write(firstBoundary, 0, firstBoundary.Length);//add
            //string partTop = "Content-Disposition: form-data; name=\"Filename\"\r\n\r\n123.jpg";//add
            //var partTopByte = Encoding.UTF8.GetBytes(partTop);//add
            //memStream.Write(partTopByte, 0, partTopByte.Length);//add   form表单 
            #endregion

            #region file表单值
            const string filePartHeader =
                    "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                     "Content-Type: application/octet-stream\r\n\r\n";
            var header = string.Format(filePartHeader, fileKeyName, "123.jpg");
            var headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

            memStream.Write(encodingBoundary, 0, encodingBoundary.Length);
            memStream.Write(headerbytes, 0, headerbytes.Length);

            var buffer = new byte[1024];
            int bytesRead; // =0
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }
            #endregion

            #region 最后一个form key-value值 非必须
            //memStream.Write(encodingBoundary, 0, encodingBoundary.Length);//add
            //string partBottom = "Content-Disposition: form-data; name=\"Upload\"\r\n\r\nSubmit Query";//add
            //var partBottomByte = Encoding.UTF8.GetBytes(partBottom);//add
            //memStream.Write(partBottomByte, 0, partBottomByte.Length);//add  form表单 
            #endregion

            // 写入最后的结束边界符
            memStream.Write(endBoundary, 0, endBoundary.Length);

            #region 上面代码拼接的 Request Payload格式
            //------------Ij5Ef1GI3cH2GI3gL6Ef1ae0cH2KM7
            //Content-Disposition: form-data; name="Filename"

            //123.jpg
            //------------Ij5Ef1GI3cH2GI3gL6Ef1ae0cH2KM7
            //Content-Disposition: form-data; name="filedata"; filename="timg.jpg"
            //Content-Type: application/octet-stream


            //------------Ij5Ef1GI3cH2GI3gL6Ef1ae0cH2KM7
            //Content-Disposition: form-data; name="Upload"

            //Submit Query
            //------------Ij5Ef1GI3cH2GI3gL6Ef1ae0cH2KM7--
            #endregion


            webRequest.ContentLength = memStream.Length;

            var requestStream = webRequest.GetRequestStream();

            memStream.Position = 0;
            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            var httpWebResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

            using (var httpStreamReader = GetWebContent(httpWebResponse))
            {
                responseContent = httpStreamReader.ReadToEnd();
            }

            fileStream.Close();
            httpWebResponse.Close();
            webRequest.Abort();

            return responseContent;
        }
        #endregion
        public static StreamReader GetWebContent(System.Net.HttpWebResponse response)
        {
            Stream desStream;
            if (response.ContentEncoding == "gzip")
            {
                desStream = new System.IO.Compression.GZipStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);

            }
            else
            {
                desStream = response.GetResponseStream();
            }

            string strcs = response.CharacterSet;
            StreamReader xmlStream;
            if (strcs == "")
            {
                xmlStream = new System.IO.StreamReader(desStream, System.Text.Encoding.Default);
            }
            else
            {
                strcs = strcs.Trim().ToLower();
                switch (strcs)
                {
                    case "gb2312":
                        xmlStream = new System.IO.StreamReader(desStream, System.Text.Encoding.GetEncoding("gb2312"));
                        break;
                    case "utf-8":
                        xmlStream = new System.IO.StreamReader(desStream, System.Text.Encoding.UTF8);
                        break;
                    default:
                        xmlStream = new System.IO.StreamReader(desStream, System.Text.Encoding.Default);
                        break;
                }

            }
            return xmlStream;
        }

        #endregion


    }
}
