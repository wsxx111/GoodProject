using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Token.Models;
using Mo = Token.Models;

namespace Token.Controllers
{
    public class TokenCheckController : Controller
    {
        public ActionResult Index()
        {
            //获得传入数据
            var inModel = new Mo.LoginDataIn();
            try
            {
                this.UpdateModel(inModel);

                inModel.ClientTime = DateTime.Now.ToString();
                inModel.Username = "wangkai";
                ViewBag.Username = inModel.Username;
                ViewBag.ClientTime = inModel.ClientTime;
            }
            catch
            {
                throw;
            }
            return View();
        }



        /// <summary>
        /// 检查令
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckToken()
        {
            var actionResult = default(ContentResult);
            //获得传入数据
            var inModel = new Mo.LoginDataIn();
            try
            {
                this.UpdateModel(inModel);          
                TokenEntity token = new TokenEntity();
                inModel.ClientTime = DateTime.Now.ToString();
                token = GenerateToken(Convert.ToDateTime(inModel.ClientTime), inModel.Username);
                LoginData data = new LoginData();
                if (token.TokenCode == inModel.Token)
                {
                    data = new LoginData { Code = 0, Msg = token.LeftTime.ToString() };
                }
                else
                {
                    data = new LoginData { Code = 1, Msg = "--" };
                }
                actionResult = new ContentResult
                {
                    Content = HttpUtility.UrlEncode(new JavaScriptSerializer().Serialize(data))
                };
            }
            catch (Exception exception)
            {
                actionResult = new ContentResult
                {
                    Content = new JavaScriptSerializer().Serialize(exception)
                };
            }
            return actionResult;
        }


        public ActionResult UpdateToken()
        {
            var actionResult = new ContentResult();
            actionResult.ContentEncoding = Encoding.UTF8;
            actionResult.ContentType = "application/json";
            //获得传入数据
            var inModel = new Mo.LoginDataIn();
            try
            {
                this.UpdateModel(inModel);             
                TokenEntity token = new TokenEntity();
                token = GenerateToken(Convert.ToDateTime(inModel.ClientTime), inModel.Username);                
                actionResult = new ContentResult
                {
                    Content = new JavaScriptSerializer().Serialize(
                    new{
                        Message = "Success",
                        Token = token,
                        Time=DateTime.Now.ToString()
                    })
                };
            }
            catch
            {
                actionResult = new ContentResult
                {
                    Content = new JavaScriptSerializer().Serialize(
                    new
                    {
                        Message = "Error"
                    }
                    )
                };
            }
            return actionResult;
        }

        public static TokenEntity GenerateToken(DateTime ClientTime, string ClientName)
        {
            string TokenCode = string.Empty;           
            //设置15秒变一次    
            Random random = new Random();                        
            TokenEntity token = new TokenEntity();

            int tmppre =10000;
            int tmplast = 10000;
            int tmpelem=ClientTime.Second/15+2;
            byte[] result = System.Text.Encoding.Default.GetBytes(ClientName);
            for(int i=0;i<result.Length;i++)
            {
              tmppre += result[i];             
            }
            tmppre += (result[0] * tmpelem * 1000 + tmpelem * tmpelem%7 + 100 * tmpelem * tmpelem * tmpelem) % 10000;
            string partone=tmppre.ToString().Substring(0,4);
            tmplast += (Convert.ToInt32(partone)*result[0]*20 + tmpelem*tmpelem*tmpelem) % 10000;
            string parttwo=tmplast.ToString().Substring(0,4);
            token.TokenCode = (parttwo + parttwo).Substring(1,6);
            token.LeftTime =15-(DateTime.Now.Second+59)%15;
            return token;
        }

    }
}
