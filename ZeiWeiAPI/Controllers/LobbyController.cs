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
	public class LobbyController : ApiController
	{
		LobbyService service = new LobbyService();
		/// <summary>
		/// select member list by memberinfo
		/// </summary>
		/// <param name="memberId">member id</param>
		/// <returns>return opposite sex member info and memberself info to compair matchlevel</returns>
		[HttpGet]
		public ArrayList selectAllMemeber(int memberId)
		{
			ArrayList memberInfo = service.selectAllMember(memberId);
			return memberInfo;
		}
		/// <summary>
		/// select member life infomation
		/// </summary>
		/// <param name="fid"></param>
		/// <returns>member life infomation by arraylist</returns>
		public ArrayList getLife(int fid)
		{
			ArrayList memberLife = service.getLife(fid);
			return memberLife;
		}
    }
}
