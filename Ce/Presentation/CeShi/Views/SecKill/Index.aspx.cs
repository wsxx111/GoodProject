using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.Helper;
using System.Xml;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;

namespace CeShi.Views.SecKill
{
    public partial class Index : System.Web.Mvc.ViewPage
    {
        #region 活动信息相关成员
        //spikeProj表
        protected string ProjId = "";
        protected string ProjName = "";
        //spikeActivity表
        protected string RunningId = "";
        protected string RunningspikeTime = "";
        protected string RunningpicLink = "";
        protected string RunningheadImgUrl = "";
        protected string RunningpicUrl = "";
        protected string RunningselNum = "";
        protected string Runningprize = "";
        protected string RunningwinnerShow = "";
        protected string RunningnowinnerShow = "";
        //其他
        protected string errorMsg = "";
        protected string hidPhone = "";

        #endregion

        #region 活动中判断相关状态成员
        protected string Cmd = "";
        protected bool IsRunning = false; //是否活动正在进行中
        protected bool IsUserlogin = false;//是否用户已报名，处于登录状态
        protected bool IsJustEnd = false;//是否上次活动刚结束，且结束还没超过10分钟
        protected bool IsCheckCode = false;//是否启用验证码
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.OriginalString;                      
            if (url.Contains(".soufun.com"))
            {
                Response.Redirect(url.Replace(".soufun.com", ".fang.com"));               
            }
            //获取传来的命令参数frm_type，根据frame判断当前活动要执行的情况

            Cmd = WebHelper.GetRequestString("frm_type", string.Empty).Trim();

            //如果值为空  代表没命令，第一次加载
            if (Cmd == string.Empty)
            {
                //当前秒杀活动分析
                ProcLoadData();
            }
            else
            {
                if (Cmd == "c")
                {
                    ProcInsertUser();//接收报名
                }
                if (Cmd == "getcheckcode")
                {
                    GetCheckCode();//获取验证码
                }
                if (Cmd == "spike")
                {
                    ProSPike();//秒杀
                }
            }
        }


        /// <summary>
        /// 当前秒杀活动分析
        /// </summary>
        private void ProcLoadData()
        {
            //加载页面
            #region 获取传来的参数 ProjId   读 spikeProj    获得 ProjName 和 IsCheckCode

            string getProjId = WebHelper.GetRequestString("Pid", string.Empty).Trim();
           //测试
            getProjId = SecureHelper.AESEncrypt(getProjId, ConfigurationManager.AppSettings["encryptKey"].Trim());

            if (getProjId == string.Empty)
            {
                errorMsg = "<script type=\"text/javascript\">$('shadow').style.display='block';$('div_errorMsg2').innerHTML = '重要参数不能为空！';$('baseErr').style.display='';autoClose();</script>";
                return;
            }
            else
            {
                //得到  ProjId
                ProjId = SecureHelper.AESDecrypt(getProjId, ConfigurationManager.AppSettings["encryptKey"].Trim());
            }
            if (!Regex.IsMatch(ProjId, @"^[1-9][0-9]*$"))
            {
                errorMsg = "<script type=\"text/javascript\">$('shadow').style.display='block';$('div_errorMsg2').innerHTML = '项目参数错误！';$('baseErr').style.display='';autoClose();</script>";
                return;
            }
            else
            {
                //得到 IsCheckCode 和 ProjName
                getisCheckCodeByProjId(ProjId);
            }
            #endregion
            //根据当前时间 和spikeTime进行判断  
            #region  刚结束 没超过10分钟  刚结束的绑定 RJust控件
            /*             |
            *              |
            *              |
            *              |
            * |——3——|_3_|_______10______| 
            * 
            */
            string sql = String.Format("select * from  spikeActivity where ProjId={0} and  isShow=1 and DateDiff(ss,DATEADD(MI,3,spikeTime),getdate())>0 and  DateDiff(ss,DATEADD(MI,3,spikeTime),getdate())<=600  and id not in(1) order by spikeTime asc", ProjId);
            DataTable dt = DBHelper.GetDataTable(sql);
            // 获得 IsJustEnd
            if (dt != null && dt.Rows.Count > 0)
            {
                IsJustEnd = true;
                this.RJust.DataSource = dt;
                this.RJust.DataBind();
            }
            #endregion

            #region 进行中的活动  根据ProjId和isShow获取秒杀活动的详细信息 取符合情况的最近一条 没有用控件
            //1.(秒杀期)
            /*        |
            *         |
            *         |
            *         |
            * |——3——|_3_|_______10______| 
            * 
            */
            //2.或者(报名期结束后3分钟，即秒杀期前3分钟)
            /*      |
            *       |
            *       |
            *       |
            *     |——3——|_3_|_______10______| 
            * 
            */
            //3.或者(报名期)
            /*  |
            *   |
            *   |
            *   |
            *     |——3——|_3_|_______10______| 
            * 
            */
            if (!IsJustEnd)
            {
                //已启用  并且  开始时间加3分钟的值大于等于当前时间
                sql = String.Format("select top 1 * from  spikeActivity where ProjId={0} and  isShow=1 and DATEADD(MI,3,spikeTime)>=GETDATE() order by spikeTime asc", ProjId);
                dt = DBHelper.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //ProjId,spikeTime,headImgUrl,picLink,picUrl,isShow,selNum,prize,winnerShow,nowinnerShow,CreateTime
                    IsRunning = true;//有进行中的活动
                    RunningId = dt.Rows[0]["Id"].ToString();//进行中活动ID
                    RunningspikeTime = dt.Rows[0]["spikeTime"].ToString();//开始时间
                    RunningheadImgUrl = dt.Rows[0]["headImgUrl"].ToString();//头图
                    RunningpicLink = dt.Rows[0]["picLink"].ToString();//链接
                    RunningpicUrl = dt.Rows[0]["picUrl"].ToString();//图片
                    RunningselNum = dt.Rows[0]["selNum"].ToString();//中签者
                    Runningprize = dt.Rows[0]["prize"].ToString();//奖品设置
                    RunningwinnerShow = dt.Rows[0]["winnerShow"].ToString();//中奖提示
                    RunningnowinnerShow = dt.Rows[0]["nowinnerShow"].ToString();//未中奖提示

                    //获得 IsUserlogin
                    //活动进行中 ，必须用户处于登录状态  读取cookies中的spikeusercheck 和表spikeUser进行匹配
                    //根据proj和phone判断秒杀登录表中是否存在该手机用户
                    if (!String.IsNullOrEmpty(RunningId))
                    {
                        if (System.Web.HttpContext.Current.Request.Cookies["spikeusercheck"] != null)
                        {
                            string tmp = System.Web.HttpContext.Current.Request.Cookies["spikeusercheck"].Value;
                            string key = ConfigurationManager.AppSettings["encryptKey"].Trim();
                            tmp = SecureHelper.AESDecrypt(tmp, key);
                            if (tmp.IndexOf('+') >= 0)
                            {
                                string[] arr = tmp.Split('+');
                                if (arr.Length == 2)
                                {
                                    hidPhone = arr[0];
                                    string sql1 = "select top 1 * from spikeUser where phone ='" + arr[0] + "'  and proj =" + ProjId;
                                    DataTable dt1 = DBHelper.GetDataTable(sql1);
                                    if (dt1 != null && dt1.Rows.Count > 0)
                                    {
                                        if (arr[1] == dt1.Rows[0]["rNum"].ToString())
                                        {
                                            IsUserlogin = true;//校验成功
                                        }
                                        else
                                        {
                                            IsUserlogin = false;
                                        }
                                    }
                                    else
                                    {
                                        IsUserlogin = false;
                                    }
                                }
                                else
                                {
                                    IsUserlogin = false;
                                }
                            }
                            else
                            {
                                IsUserlogin = false;
                            }
                        }
                        else
                        {
                            IsUserlogin = false;
                        }
                    }
                }
            }
            #endregion

            #region 下一场 取最近的一条  下场的绑定 RNext控件
            //如果有正在进行的，RunningId不会为空，所以是取的第二条最近的
            if (!String.IsNullOrEmpty(RunningId))
            {
                sql = String.Format("select * from  spikeActivity where ProjId={0} and  isShow=1 and DATEADD(MI,3,spikeTime)>=GETDATE() and id not in({1}) order by spikeTime asc", ProjId, RunningId);
            }
            else
            {
                sql = String.Format("select * from  spikeActivity where ProjId={0} and  isShow=1 and DATEADD(MI,3,spikeTime)>=GETDATE()  order by spikeTime asc", ProjId);
            }
            dt = DBHelper.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.RNext.DataSource = dt;
                this.RNext.DataBind();
            }
            #endregion

            #region  已结束  结束超过10分钟的活动  REndL控件
            sql = String.Format("select  * from  spikeActivity where ProjId={0} and  isShow=1 and  DateDiff(ss,DATEADD(MI,3,spikeTime),getdate())>600  order by spikeTime asc", ProjId);
            dt = DBHelper.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.REndL.DataSource = dt;
                this.REndL.DataBind();
            }
            #endregion

        }

        private void getisCheckCodeByProjId(string projId)
        {
            DataTable dt = DBHelper.GetDataTable("select top 1 * from spikeProj where isSpecial IS NULL AND id=" + projId);
            if (dt != null && dt.Rows.Count > 0)
            {
                //得到  ProjName
                ProjName = dt.Rows[0]["ProjName"].ToString();
                string ischeckcode = dt.Rows[0]["isCheckCode"].ToString().Trim();
                if (ischeckcode == "1")
                {
                    //得到 IsCheckCode
                    IsCheckCode = true;
                }
                else
                {
                    IsCheckCode = false;
                }
            }
            else
            {
                errorMsg = "<script type=\"text/javascript\">$('shadow').style.display='block';$('div_errorMsg2').innerHTML = '不存在此项目！';$('baseErr').style.display='';autoClose();</script>";
                return;
            }
        }


        /// <summary>
        /// 接收报名
        /// </summary>
        protected void ProcInsertUser()
        {
            //校对报名信息 不再使用验证码
            string hprojid = WebHelper.GetRequestString("hprojid").Trim();//项目ID
            string hactivityId = WebHelper.GetRequestString("hactivityId").Trim();//活动ID
            string userTel = WebHelper.GetRequestString("txt_phone").Trim();//手机号
            string checkCode = WebHelper.GetRequestString("txt_code").Trim();//验证码

            if (CheckBeforeResign(hprojid, hactivityId, userTel, checkCode))
            {
                //查询spikeUser表，报名流程
                DataTable dtUser = DBHelper.GetDataTable("select top 1 * from spikeUser where proj='" + hprojid + "'  and phone='" + userTel.Trim() + "'");
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    //用户已报名  对于已报名的参与者，即表中有这个手机号的，update  rNum
                    int rNum = new Random().Next(1000, 9999);
                    if (DBHelper.ExecuteCommand("update  spikeUser set rNum='" + rNum + "' where id='" + dtUser.Rows[0]["id"].ToString() + "'") > 0)
                    {
                        this.Response.Cookies["spikeusercheck"].Value = SecureHelper.AESEncrypt(userTel.Trim() + "+" + rNum, ConfigurationManager.AppSettings["encryptKey"]);
                        this.Response.Cookies["spikeusercheck"].Expires = DateTime.Now.AddDays(1);
                        this.Response.Cookies["spikeusercheck"].Domain = System.Configuration.ConfigurationManager.AppSettings["cookieDomain"];
                        errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '登录成功！';parent.$('div_errorMsg1').style.display='';parent.location.reload();</script>";
                        IOHelper.WriteLogFile("登录成功！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                        return;
                    }
                    else
                    {
                        errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '登录失败！';parent.$('div_errorMsg1').style.display='';parent.location.reload();</script>";
                        IOHelper.WriteLogFile("登录失败！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                        return;
                    }
                }
                else
                {
                    //对于未报名用户的
                    //如果本项目需要验证验证码 则进行验证
                    if (IsCheckCode)
                    {
                        string mes = String.Empty;
                        //验证码的校验
                        if (!isCheckCode(userTel, checkCode, ref mes))
                        {
                            errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '" + mes + "';parent.$('div_errorMsg1').style.display='block';</script>";
                            IOHelper.WriteLogFile(" +mes+" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                            return;
                        }
                    }

                    //添加报名用户
                    int rNum = new Random().Next(1000, 9999);
                    if (DBHelper.ExecuteCommand("insert into spikeUser(phone,proj,rNum,activityId) values('" + userTel.Trim() + "','" + hprojid + "','" + rNum + "','" + hactivityId + "')") > 0)
                    {
                        this.Response.Cookies["spikeusercheck"].Value = SecureHelper.AESEncrypt(userTel.Trim() + "+" + rNum, ConfigurationManager.AppSettings["encryptKey"]);
                        this.Response.Cookies["spikeusercheck"].Expires = DateTime.Now.AddDays(1);
                        this.Response.Cookies["spikeusercheck"].Domain = System.Configuration.ConfigurationManager.AppSettings["cookieDomain"];

                        errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '报名成功！';parent.$('div_errorMsg1').style.display='';parent.location.reload();</script>";
                        IOHelper.WriteLogFile("报名成功！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                        return;
                    }
                    else
                    {
                        errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '报名失败！';parent.$('div_errorMsg1').style.display='';parent.location.reload();</script>";
                        IOHelper.WriteLogFile("报名失败！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 报名前的检验
        /// </summary>
        /// <param name="hprojid"></param>
        /// <param name="hactivityId"></param>
        /// <param name="userTel"></param>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        private bool CheckBeforeResign(string hprojid, string hactivityId, string userTel, string checkCode)
        {
            IOHelper.WriteLog("--------------------- 手机号码！" + userTel + "报名日志  开始" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "---------------");
            //手机号码不能为空
            if (String.IsNullOrEmpty(userTel))
            {
                errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '手机号码不能为空！';parent.$('div_errorMsg1').style.display=''</script>";
                IOHelper.WriteLogFile(" 手机号码不能为空！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return false;
            }

            //手机格式必须正确
            if (userTel.Length != 11 || !Regex.IsMatch(userTel, @"^[1-9][0-9]*$"))
            {
                errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '手机号码输入错误！';parent.$('div_errorMsg1').style.display=''</script>";
                IOHelper.WriteLogFile(userTel + " 手机号码输入错误！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return false;
            }

            //项目ID 与 活动ID 不能为空
            if (String.IsNullOrEmpty(hprojid) || String.IsNullOrEmpty(hactivityId))
            {
                errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '重要参数为空！';parent.$('div_errorMsg1').style.display=''</script>";
                IOHelper.WriteLogFile(" 重要参数为空！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return false;
            }

            //项目ID 与 活动ID 必须为数字
            if (!Regex.IsMatch(hprojid, @"^[1-9][0-9]*$") || !Regex.IsMatch(hactivityId, @"^[1-9][0-9]*$"))
            {
                errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '项目参数非数字！';parent.$('div_errorMsg1').style.display=''</script>";
                IOHelper.WriteLogFile(" 项目参数非数字！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return false;
            }


            //是否有这个项目
            DataTable spikeProjdt = DBHelper.GetDataTable("select top 1 * from spikeProj where id=" + hprojid);
            if (spikeProjdt != null && spikeProjdt.Rows.Count > 0)
            {
                string ischeckcode = spikeProjdt.Rows[0]["isCheckCode"].ToString().Trim();
                if (ischeckcode == "1")
                {
                    IsCheckCode = true;
                }
                //是否有这个活动   特定的ProjId和id在spikeActivity只应该有一条
                if (DBHelper.GetDataTable("select * from spikeActivity where ProjId=" + hprojid + " and  id=" + hactivityId + "  and  isShow=1").Rows.Count != 1)
                {
                    errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '活动状态验证失败，请重新提交！';parent.$('div_errorMsg1').style.display='block';</script>";
                    IOHelper.WriteLogFile(" 活动状态验证失败，请重新提交！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                    return false;
                }

                //并且这个活动还没有结束
                if (DBHelper.GetDataTable("select * from spikeActivity where ProjId=" + hprojid + " and  id=" + hactivityId + "  and  isShow=1 and DATEADD(MI,3,spikeTime)>=GETDATE()").Rows.Count != 1)
                {
                    errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '活动已结束！';parent.$('div_errorMsg1').style.display='block';</script>";
                    IOHelper.WriteLogFile(" 活动已结束！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                    return false;
                }
                return true;
            }
            else
            {
                errorMsg = "<script type=\"text/javascript\">parent.$('div_errorMsg1').innerHTML = '项目状态验证失败，请重新提交！';parent.$('div_errorMsg1').style.display='block';</script>";
                IOHelper.WriteLogFile(" 项目状态验证失败，请重新提交！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return false;
            }
        }

        /// <summary>
        /// 验证码的校验
        /// </summary>
        /// <param name="userTel"></param>
        /// <param name="checkCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool isCheckCode(string userTel, string checkCode, ref string message)
        {
            bool ret = false;

            if (checkCode.Length != 6 || !Regex.IsMatch(checkCode, @"^[1-9][0-9]*$"))
            {
                message = "验证码格式不正确，请重新填写！";
                return ret;
            }
            string tmp = WebHelper.PostData(System.Configuration.ConfigurationManager.AppSettings["newcardInterface"] + "/CheckPhoneCode.aspx", "type=22&phone=" + userTel + "&code=" + checkCode);
            if (String.IsNullOrEmpty(tmp))
            {
                message = " 验证码校验失败，请重新获取！";
                return ret;
            }

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(tmp);
            if (xd == null)
            {
                message = " 验证码校验失败，请重新获取！";
                return ret;
            }


            if (xd.SelectSingleNode("soufun_card").SelectSingleNode("get_code").SelectSingleNode("return_result").InnerText.Trim() == "100")
            {
                message = " 验证码验证通过！";
                ret = true;
            }
            else
            {
                message = " 验证码错误！请输入正确的验证码！";
                return ret;
            }
            return ret;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        protected void GetCheckCode()
        {
            string userTel = WebHelper.GetRequestString("txt_phone").Trim();
            string hprojid = WebHelper.GetRequestString("hprojid").Trim();
            string hactivityId = WebHelper.GetRequestString("hactivityId").Trim();

            if (String.IsNullOrEmpty(userTel))
            {
                errorMsg = "<script type=\"text/javascript\">parent.show_div_errorMsg1('手机不能为空！');</script>";
                IOHelper.WriteLogFile("手机不能为空！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return;
            }

            if (userTel.Length != 11 || !Regex.IsMatch(userTel, @"^[1-9][0-9]*$"))
            {
                errorMsg = "<script type=\"text/javascript\">parent.show_div_errorMsg1('手机格式错误！');</script>";
                IOHelper.WriteLogFile("手机格式错误！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return;
            }

            string tmp = WebHelper.PostData(System.Configuration.ConfigurationManager.AppSettings["newcardInterface"] + "/GetPhoneCode.aspx", "type=22&phone=" + userTel);
            if (String.IsNullOrEmpty(tmp))
            {
                errorMsg = "<script type=\"text/javascript\">parent.show_div_errorMsg1('验证码获取失败，请重新获取！');</script>";
                IOHelper.WriteLogFile("验证码获取失败，请重新获取b！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return;
            }


            XmlDocument xd = new XmlDocument();
            xd.LoadXml(tmp);
            if (xd == null)
            {
                errorMsg = "<script type=\"text/javascript\">parent.show_div_errorMsg1('验证码获取失败，请重新获取！');</script>";
                IOHelper.WriteLogFile("验证码获取失败，请重新获取a！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return;
            }


            if (xd.SelectSingleNode("soufun_card").SelectSingleNode("get_code").SelectSingleNode("return_result").InnerText.Trim() == "100")
            {
                errorMsg = "<script type=\"text/javascript\">parent.show_div_errorMsg1('验证码发送成功！');</script>";
                IOHelper.WriteLogFile("验证码发送成功！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return;
            }
            else if (xd.SelectSingleNode("soufun_card").SelectSingleNode("get_code").SelectSingleNode("return_result").InnerText.Trim() == "004")
            {
                errorMsg = "<script type=\"text/javascript\">parent.show_div_errorMsg1('验证码获取次数超过当天限制！');</script>";
                IOHelper.WriteLogFile("验证码获取次数超过当天限制！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return;
            }
            else
            {
                errorMsg = "<script type=\"text/javascript\">parent.show_div_errorMsg1('验证码发送失败，请重新获取！');</script>";
                IOHelper.WriteLogFile("验证码发送失败，请重新获取！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "userTel:" + userTel);
                return;
            }
        }


        /// <summary>
        /// 处理秒杀请求
        /// </summary>
        protected void ProSPike()
        {
            #region 变量定义
            string hprojid = WebHelper.GetRequestString("hprojid").Trim();//项目ID
            string hactivityId = WebHelper.GetRequestString("hactivityId").Trim();//活动ID
            string phone = WebHelper.GetRequestString("hidPhone").Trim();//手机号
            string spikeTime = "";//开始时间
            string nowtime = "";//数据库当前时间
            string nowtimeAdd3 = "";//数据库当前时间加3分钟
            string spikeTimeSub3 = "";//开始时间减3分钟
            string spikeTimeAdd3 = "";//开始时间加3分钟
            string winnerShow = "";//中奖提示
            string nowinnerShow = "";//未中奖提示

            IOHelper.WriteLog("--------------------- 秒杀 手机号码！" + phone + "报名日志  开始" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "---------------");
            #endregion

            #region 必要验证
            //手机号码不能为空
            if (String.IsNullOrEmpty(phone))
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '手机号码不能为空！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 手机号码不能为空！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }

            //手机格式必须正确
            if (phone.Length != 11 || !Regex.IsMatch(phone, @"^[1-9][0-9]*$"))
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '手机号码输入错误！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 手机号码输入错误！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }

            //项目ID 与 活动ID 不能为空
            if (String.IsNullOrEmpty(hprojid) || String.IsNullOrEmpty(hactivityId))
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '重要参数为空！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 重要参数为空！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }

            //项目ID 与 活动ID 必须为数字
            if (!Regex.IsMatch(hprojid, @"^[1-9][0-9]*$") || !Regex.IsMatch(hactivityId, @"^[1-9][0-9]*$"))
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '项目参数非数字！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 项目参数非数字！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }

            //是否有这个项目 spikeProj表
            if (DBHelper.GetDataTable("select * from spikeProj where id = '" + hprojid + "'").Rows.Count != 1)
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '项目状态验证失败，请重新提交！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 项目状态验证失败，请重新提交！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }


            //是否有这个活动  spikeActivity表
            string sql = "select top 1 GETDATE() as nowtime,DATEADD(MI,3,GETDATE()) as nowtimeAdd3, DATEADD(MI,-3,spikeTime) as spikeTimeSub3,DATEADD(MI,3,spikeTime) as spikeTimeAdd3, * from  spikeActivity where ProjId=" + hprojid + " and id=" + hactivityId + " and isShow=1";
            DataTable dtProj = DBHelper.GetDataTable(sql);
            if (dtProj == null || dtProj.Rows.Count <= 0)
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '活动错误！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 活动错误！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }

            //是否该用户已报名
            sql = "select top 1 * from spikeUser where phone ='" + phone + "'  and proj =" + hprojid;
            DataTable userDt = DBHelper.GetDataTable(sql);
            if (userDt == null || userDt.Rows.Count <= 0)
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '此手机未报名！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 此手机未报名！" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }

            #endregion

            #region 本次活动参数获取
            spikeTime = dtProj.Rows[0]["spikeTime"].ToString();
            nowtime = dtProj.Rows[0]["nowtime"].ToString();
            nowtimeAdd3 = dtProj.Rows[0]["nowtimeAdd3"].ToString();
            spikeTimeSub3 = dtProj.Rows[0]["spikeTimeSub3"].ToString();
            spikeTimeAdd3 = dtProj.Rows[0]["spikeTimeAdd3"].ToString();
            winnerShow = dtProj.Rows[0]["winnerShow"].ToString();//中奖提示
            nowinnerShow = dtProj.Rows[0]["nowinnerShow"].ToString();//未中奖提示
            if (String.IsNullOrEmpty(winnerShow))
            {
                winnerShow = "搜房小编会及时联系您领取奖品";
            }
            if (String.IsNullOrEmpty(nowinnerShow))
            {
                nowinnerShow = "差一点就中了哦！";
            }
            List<int> listNum = new List<int>();
            string[] arrNum = dtProj.Rows[0]["selNum"].ToString().Split(',');
            if (arrNum != null && arrNum.Length > 0)
            {
                int myT = 0;
                for (int k = 0; k < arrNum.Length; k++)
                {
                    int.TryParse(arrNum[k], out myT);
                    listNum.Add(myT);
                }
            }
            #endregion

            #region 未到时间或已结束
            //未到秒杀时间  当前时间 小于  减3分钟后的时间 
            //(报名期)
            /*  |
            *   |
            *   |
            *   |
            *     |——3——|_3_|_______10______| 
            * 
            */
            if (DateTime.Parse(nowtime) < DateTime.Parse(spikeTimeSub3))
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '未到秒杀时间！';parent.$('baseErr').style.display='block';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 未到秒杀时间！项目号:" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }

            //秒杀已结束 
            /*             |
            *              |
            *              |
            *              |
            * |——3——|_3_|_______10______| 
            * 
            * 
            */

            if (DateTime.Parse(nowtime) > DateTime.Parse(spikeTimeAdd3))
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '秒杀已结束！';parent.$('baseErr').style.display='block';parent.autoClose();parent.window.location.reload();</script>";
                IOHelper.WriteLogFile(" 秒杀已结束！项目号:" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }


            //   -------------------------------从这里开始--------------------------------------

            //如果已经成功秒杀该项目，可能是其他活动
            
            if (isExists(hprojid, hactivityId, phone))
            {
                //errorMsg += "<script type=\"text/javascript\">parent.$('popSuc').style.display='block';parent.autoClose();</script>";
                errorMsg += "<script type=\"text/javascript\">parent.showPops('popSuc','" + winnerShow + "')</script>";
                IOHelper.WriteLogFile(phone + " 已经秒杀过该项目活动了！项目号:" + hprojid + "活动号:" + hactivityId);
                return;
            }


            //当前时间加3分钟大于等于开始时间  并且 当前时间小于等于开始时间加3分钟
            //(秒杀期)
            /*        |
            *         |
            *         |
            *         |
            * |——3——|_3_|_______10______| 
            * 
            */
            bool flag = false;
            if (DateTime.Parse(nowtimeAdd3) >= DateTime.Parse(spikeTime) && DateTime.Parse(nowtime) <= DateTime.Parse(spikeTimeAdd3))
            {
                flag = true;
            }

            if (!flag)
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '现在不是秒杀时间！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 不是秒杀时间！项目号:" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }

            #endregion

            #region 取已经插入的信息 查询本活动奖品是否被抢完  秒杀表spikeInfo中已存的数据的addtime在秒杀时间的1小时之内
            sql = "select count(*) from spikeInfo where proj='" + hprojid + "' and activityId='" + hactivityId + "'  and addtime>='" + spikeTime + "' and addtime< '" + DateTime.Parse(spikeTime).AddHours(1) + "' ";
            int mCount = 0;
            if (!int.TryParse(DBHelper.ReturnStringScalar(sql), out mCount))
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '获取信息错误！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 取已经插入的信息有误！项目号:" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }


            //本轮次已有秒中的用户  奖品被抢完了
            if (mCount > 0 && mCount >= listNum.Max())
            {
                string failTime = DateTime.Parse(nowtime).ToString("HH:mm:ss");
                errorMsg += "<script type=\"text/javascript\">parent.showPop('popFal','" + failTime + "','" + nowinnerShow + "')</script>";
                IOHelper.WriteLogFile(" 本轮次已有秒中的用户！项目号:" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                return;
            }

            //秒杀操作失败，已经秒中过 本项目其他活动了
            //查spikeOnly表 传phone hactivityId 和 时间（比较）  看是否有该数据

            if (isprojExists(hprojid, phone))
            {
                string failTime = DateTime.Parse(nowtime).ToString("HH:mm:ss");
                IOHelper.WriteLogFile("秒杀操作失败，已经秒中过 本项目其他活动" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone + "," + sql);
                errorMsg += "<script type=\"text/javascript\">parent.showPop('popFal','" + failTime + "','" + nowinnerShow + "')</script>";
                return;
            }

            #endregion

            #region 秒杀流程处理

            //插入秒杀并发表
            sql = "insert into spikeonly(phone,activityId) values('" + phone.Trim() + "','" + hactivityId + "');select @@IDENTITY";//插入并发判断表
            object onlyO = DBHelper.GetScalar(sql);
            string numOnly = string.Empty;
            if (onlyO != null && onlyO.ToString() != "")
            {
                if (Convert.ToInt32(onlyO.ToString()) > 0)
                {
                    numOnly = onlyO.ToString().Trim();
                }
            }
            else
            {
                return;
            }
            //插入秒杀记录表
            sql = "insert into spikeInfo(proj,phone,activityId) values('" + hprojid + "','" + phone + "','" + hactivityId + "');select @@IDENTITY";
            int tNum = 0;
            object otNum = DBHelper.ReturnStringScalar(sql).ToString();
            if (otNum == null)
            {
                deleteSpikeOnly(numOnly);
                return;
            }
            if (!int.TryParse(otNum.ToString(), out tNum))
            {
                deleteSpikeOnly(numOnly);
                return;
            }
            IOHelper.WriteLogFile(" 进入数据库！项目号:" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone + "tNum=" + tNum);


            if (listNum.Count <= 0)//秒杀设定的位次
            {
                errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '位次信息有误！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                IOHelper.WriteLogFile(" 位次信息有误！项目号:" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                //更新秒杀并发处理表中状态为1
                deleteSpikeOnly(numOnly);
                return;
            }

            //秒杀失败,未能取到该时段进入库里的手机号
            sql = "select top 1000 Row_Number() OVER(order by id asc) num, * from spikeInfo where proj='" + hprojid + "' and activityId='" + hactivityId + "' and addtime>='" + spikeTime + "' and addtime< '" + DateTime.Parse(spikeTime).AddHours(1) + "' order by addtime asc";
            DataTable dtTotal = DBHelper.GetDataTable(sql);
            if (dtTotal == null || dtTotal.Rows.Count <= 0)
            {
                string failTime = DateTime.Parse(nowtime).ToString("HH:mm:ss");
                errorMsg += "<script type=\"text/javascript\">parent.showPop('popFal','" + failTime + "','" + nowinnerShow + "')</script>";
                IOHelper.WriteLogFile("秒杀失败,未能取到该时段进入库里的手机号.项目:" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                //更新秒杀并发处理表中状态为1
                deleteSpikeOnly(numOnly);
                return;
            }

            //取到已入库数据的信息并分析处理
            DataRow[] drs = dtTotal.Select("id=" + tNum);
            if (drs == null || drs.Length <= 0)
            {
                string failTime = DateTime.Parse(nowtime).ToString("HH:mm:ss");
                errorMsg += "<script type=\"text/javascript\">parent.showPop('popFal','" + failTime + "','" + nowinnerShow + "')</script>";
                IOHelper.WriteLogFile("按id获取手机号出错，项目:" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                //更新秒杀并发处理表中状态为1
                deleteSpikeOnly(numOnly);
                return;
            }


            int tmpOrder = 0;
            for (int yy = 0; yy < drs.Length;yy++)
            {
                if (int.TryParse(drs[yy]["num"].ToString(), out tmpOrder))
                {
                    if (listNum.Contains(tmpOrder))
                    {
                        sql = "update spikeInfo set ischeck='1' where id =" + tNum;
                        if (DBHelper.ExecuteCommand(sql) > 0)
                        {
                            IOHelper.WriteLogFile(" 成功秒杀该项目" + "hprojid:" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone);
                            errorMsg += "<script type=\"text/javascript\">parent.showPops('popSuc','" + winnerShow + "')</script>";
                            //更新秒杀并发处理表中状态为1
                            deleteSpikeOnly(numOnly);
                            return;
                        }
                        else
                        {
                            IOHelper.WriteLogFile(" 秒杀操作失败，项目" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone + "," + sql);
                            errorMsg += "<script type=\"text/javascript\">parent.$('shadow').style.display='block';parent.$('div_errorMsg2').innerHTML = '秒杀失败联系管理员！';parent.$('baseErr').style.display='';parent.autoClose();</script>";
                            //更新秒杀并发处理表中状态为1
                            deleteSpikeOnly(numOnly);
                            return;
                        }
                    }
                    else
                    {
                        string failTime = DateTime.Parse(nowtime).ToString("HH:mm:ss");
                        IOHelper.WriteLogFile("秒杀操作失败，原因:此手机号位次不在设定位。项目" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone + "," + sql);
                        errorMsg += "<script type=\"text/javascript\">parent.showPop('popFal','" + failTime + "','" + nowinnerShow + "')</script>";                        
                        deleteSpikeOnly(numOnly);
                        return;
                    }
                }
                else
                {
                    string failTime = DateTime.Parse(nowtime).ToString("HH:mm:ss");
                    IOHelper.WriteLogFile("转化序号时出错，项目" + hprojid + "hactivityId:" + hactivityId + "phone:" + phone + "," + drs[yy]["num"].ToString());
                    failTime = DateTime.Now.ToString("HH:mm:ss");
                    errorMsg += "<script type=\"text/javascript\">parent.showPop('popFal','" + failTime + "','" + nowinnerShow + "')</script>";
                    //更新秒杀并发处理表中状态为1
                    deleteSpikeOnly(numOnly);
                    return;
                }
            }
            #endregion
        }             


        /// <summary>
        /// 获取改手机号是否已成功秒杀
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        bool isExists(string projid, string activityId, string phone)
        {
            bool flag = false;
            string sql = "select * from spikeInfo where ischeck='1' and activityId='" + activityId + "'  and proj='" + projid + "' and phone ='" + phone + "'";
            DataTable dtTmp = DBHelper.GetDataTable(sql);
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }


        /// <summary>
        /// 更新秒杀流程状态
        /// </summary>
        void deleteSpikeOnly(string numOnly)
        {
            string sql = "delete from  spikeonly  where id=" + numOnly;
            DBHelper.GetDataTable(sql);
        }

        /// <summary>
        /// 获取手机号是否已成功秒中 本项目其他秒杀
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        bool isprojExists(string projid, string phone)
        {
            bool flag = false;
            string sql = "select * from spikeInfo where ischeck='1'  and proj='" + projid + "' and phone ='" + phone + "'";
            DataTable dtTmp = DBHelper.GetDataTable(sql);
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }




        /// <summary>
        /// 秒中位次过长换行显示
        /// </summary>
        /// <returns></returns>
        protected string DealSeatNum(string selNum)
        {
            string resultStr = string.Empty;
            if (!string.IsNullOrEmpty(selNum))
            {
                string[] numStr = selNum.Split(',');
                if (numStr.Length > 14)
                {
                    for (int i = 0; i < numStr.Length; i++)
                    {
                        if (i % 14 == 0 && i != 0)
                        {
                            resultStr += "<br>" + numStr[i].Trim() + ",";
                        }
                        else
                        {
                            resultStr += numStr[i].Trim() + ",";
                        }
                    }
                    resultStr = resultStr.TrimEnd(',');
                }
                else
                {
                    resultStr = selNum.Trim();
                }
            }
            return resultStr;
        }

        /// <summary>
        /// 项目图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        protected string getImage(string url, string link)
        {
            string str = @" <a href='#'><img src='http://img1.soufun.com/message/images/spike/imgold.png' width='100%'></a>";
            if (url != "" && link != "") str = @" <a href='" + link + "'><img src='" + url + "' width='100%'></a>";
            else
            {
                if (url != "") str = @" <a href='#'><img src='" + url + "' width='100%'></a>";
                if (link != "") str = @" <a href='" + link + "'><img src='/images/img.png' width='100%'></a>";
            }
            return str;
        }

        /// <summary>
        /// 得到哪个用户秒中了
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        protected string GetIscheckUser(string activityId)
        {
            string winUsers = string.Empty;
            DataTable dtUsers = DBHelper.GetDataTable("select * from spikeInfo where activityId ='" + activityId + "' and ischeck='1' and addtime >='" + DateTime.Now.ToString("yyyy-MM-dd") + "' order by id desc");
            if (dtUsers != null && dtUsers.Rows.Count > 0)
            {
                winUsers += "<li><strong class='fl'>中奖结果：</strong><div class='fl'>";
                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {
                    winUsers += "<p><span class='c_f90'>" + dtUsers.Rows[i]["phone"].ToString().Substring(0, 3) + "</span>搜房网友<span class='c_f90'>" + dtUsers.Rows[i]["phone"].ToString().Substring(7) + "</span>成为" + DateTime.Parse(dtUsers.Rows[i]["addtime"].ToString()).ToString("HH点") + DateTime.Parse(dtUsers.Rows[i]["addtime"].ToString()).ToString("mm").Substring(0, 1) + "0分秒杀幸运用户</p>";

                }
                winUsers += "</div></li>";
            }

            return winUsers;
        }

    }
}