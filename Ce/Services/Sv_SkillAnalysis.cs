using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helper;

namespace Services
{
    public partial class Sv_SkillAnalysis
    {      
        /// <summary>
        /// 根据projId获取ProjName和IsCheckCode
        /// </summary>
        /// <param name="projId"></param>
        /// <param name="errorMsg"></param>
        /// <param name="ProjName"></param>
        /// <param name="IsCheckCode"></param>
        public static void getisCheckCodeByProjId(string projId, ref string errorMsg,ref string ProjName, ref bool IsCheckCode)
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
            }           
        }

      
    }
}
