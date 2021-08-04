using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using ZeiWeiAPI.Models;
using ZeiWeiAPI.Service;

namespace ZeiWeiAPI.Controllers
{
    public class MemberController : ApiController
    {
		MemberService service = new MemberService();
		/// <summary>
		/// check login and retrun the login status
		/// </summary>
		/// <param name="fAccount">member account</param>
		/// <param name="fPassword">member password</param>
		/// <returns>fid,ftoken,errorMessage</returns>
		[HttpGet]
		public ArrayList MemberLogin(string fAccount, string fPassword)
		{
			ArrayList memberList = service.MemberLogin(fAccount, fPassword);
			return memberList;
		}
		/// <summary>
		/// register member and retrun the register status
		/// </summary>
		/// <param name="fAccount">member account</param>
		/// <param name="fPassword">member password</param>
		/// <returns>mid, mtoken, error</returns>
		[HttpGet]
		public ArrayList registerZeiWeiMember(string fAccount, string fPassword)
		{
			ArrayList memberList = service.registerZeiWeiMember(fAccount, fPassword);
			return memberList;
		}
		/// <summary>
		/// 排紫微斗數命盤
		/// </summary>
		/// <param name="Mid">member id</param>
		/// <param name="MLName">member name</param>
		/// <param name="MLSex">member sex</param>
		/// <param name="MLBirYear">member birthyear</param>
		/// <param name="MLBirMonth">member birthMonth</param>
		/// <param name="MLBirDate">member birthDay</param>
		/// <param name="MLBirTime">member birthTime</param>
		/// <returns>回傳命盤更新狀態</returns>
		public ArrayList GettCase(string Mid, string MLName, string MLSex, string MLBirYear, string MLBirMonth, string MLBirDate, string MLBirTime)
		{
			ArrayList personArray = service.GettCase(Mid, MLName, MLSex, MLBirYear, MLBirMonth, MLBirDate, MLBirTime);
			return personArray;
		}
	}

}

