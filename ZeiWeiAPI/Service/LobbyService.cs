using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeiWeiAPI.IService;
using ZeiWeiAPI.Models;

namespace ZeiWeiAPI.Service
{
	public class LobbyService : ILobbyService
	{
		ZeiWeiEntities db = new ZeiWeiEntities();
		public ArrayList selectAllMember(int memberId)
		{
			ArrayList memberInfo = new ArrayList();
			//search memberinfo by memberId
			var info = db.MemberLife.Where(m => m.MLNumber == memberId);
			if (info.FirstOrDefault() == null)
			{
				object id = memberId;
				object errorMessage = "未輸入命盤資料";
				Object memberInfoObject = new { id, errorMessage };
				memberInfo.Add(memberInfoObject);
			}
			else
			{
				string memberSex = info.FirstOrDefault().MLSex;
				//排除同性的使用者資料
				var data = from a in db.MemberLife
						   where a.MLSex != memberSex
						   select a;
				//因為比對需要用到自己的命盤資料 所以跟異性的資料一起回傳
				var join = data.Union(info);
				//加入object 組成JSON
				foreach (var item in join)
				{
					object id = item.MLNumber + ".jpg";
					object name = item.MLName;
					object sex = item.MLSex == "F" ? "女" : "男";
					object age = DateTime.Now.Year - Convert.ToInt32(item.MLBirYear);
					object pair1 = item.MLLife;
					object pair2 = item.MLMove;
					object pair3 = item.MLCompany;
					object pair4 = item.MLMoney;
					Object memberInfoObject = new { id, name, sex, age, pair1, pair2, pair3, pair4 };
					memberInfo.Add(memberInfoObject);
				}
			}
			return memberInfo;
		}
		public ArrayList getLife(int fid)
		{
			ArrayList memberLife = new ArrayList();

			var info = db.MemberLife.Where(m => m.MLNumber == fid).FirstOrDefault();

			object life = info.MLLife;
			object move = info.MLMove;
			object company = info.MLCompany;
			object money = info.MLMoney;
			object love = info.MLCouple;
			object friend = info.MLLucky;
			Object memberInfoObject = new { life, move, company, money, love, friend };
			memberLife.Add(memberInfoObject);

			return memberLife;
		}
	}
}