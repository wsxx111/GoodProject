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
    }
}
