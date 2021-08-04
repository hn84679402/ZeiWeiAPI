using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeiWeiAPI.IService;
using ZeiWeiAPI.Models;

namespace ZeiWeiAPI.Service
{
	public class MemberInfoService : IMemberInfoService
	{
		ZeiWeiEntities db = new ZeiWeiEntities();
		public ArrayList getMemberInfo(int fid)
		{
			ArrayList memberInfo = new ArrayList();
			var list = db.MemberLife.Where(d => d.MLNumber == fid).FirstOrDefault();
			//將資料塞入object編成JSON資料
			if (list != null)
			{
				object name = list.MLName;
				object birthYear = list.MLBirYear;
				object birthMonth = list.MLBirMonth;
				object birthDay = list.MLBirDate;
				object birthTime = list.MLBirTime;
				object lifeStar = list.MLLife;
				object moveStar = list.MLMove;
				object moneyStar = list.MLMoney;
				object companyStar = list.MLCompany;
				object coupleStar = list.MLCouple;
				object luckyStar = list.MLLucky;
				Object memberInfoObject = new { name, birthYear, birthMonth, birthDay, birthTime, lifeStar, moveStar, moneyStar, companyStar, coupleStar, luckyStar };
				memberInfo.Add(memberInfoObject);
			}
			return memberInfo;
		}
	}
}