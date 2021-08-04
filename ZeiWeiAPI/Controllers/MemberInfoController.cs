using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZeiWeiAPI.Models;
using ZeiWeiAPI.Service;

namespace ZeiWeiAPI.Controllers
{
    public class MemberInfoController : ApiController
    {
		MemberInfoService service = new MemberInfoService();
		/// <summary>
		/// get the member info by fid 
		/// </summary>
		/// <param name="fid">member fid</param>
		/// <returns>all of the memberinfo</returns>
		public ArrayList getMemberInfo(int fid)
		{
			ArrayList memberInfo = service.getMemberInfo(fid);
			return memberInfo;
		}
    }
}
