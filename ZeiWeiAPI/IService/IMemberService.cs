using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeiWeiAPI.IService
{
	interface IMemberService
	{
		ArrayList MemberLogin(string fAccount, string fPassword);
		ArrayList registerZeiWeiMember(string fAccount, string fPassword);
		ArrayList GettCase(string Mid, string MLName, string MLSex, string MLBirYear, string MLBirMonth, string MLBirDate, string MLBirTime);
	}
}