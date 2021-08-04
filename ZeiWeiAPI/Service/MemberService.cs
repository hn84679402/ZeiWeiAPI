using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ZeiWeiAPI.IService;
using ZeiWeiAPI.Models;

namespace ZeiWeiAPI.Service
{
	public class MemberService : IMemberService
	{
		ZeiWeiEntities db = new ZeiWeiEntities();
		public ArrayList MemberLogin(string fAccount, string fPassword)
		{
			ArrayList memberList = new ArrayList();
			try
			{
				var data = from a in db.Member
						   where a.MAccount == fAccount && a.MPassword == fPassword
						   select a;
				//有找到資料表示會員帳號密碼無誤 回傳正確資訊
				if (data.FirstOrDefault() != null)
				{
					object fid = data.FirstOrDefault().MId;
					object fToken = data.FirstOrDefault().MToken;
					object errorMessages = "登入成功";
					Object memberObject = new { fid, fToken, errorMessages };
					memberList.Add(memberObject);
				}
				//沒找到表示帳號或密碼有誤
				else
				{
					object errorMessages = "登入失敗,請檢查您的帳號密碼";
					Object errorObject = new { errorMessages };
					memberList.Add(errorObject);
				}
			}
			//抓取錯誤
			catch (Exception ex)
			{
				memberList.Clear();
				//資料庫的例外就傳系統維護
				if (ex is SqlException)
				{
					object errorMessages = "登入失敗,系統維護中";
					Object errorObject = new { errorMessages };
					memberList.Add(errorObject);
					return memberList;
				}
				//其他則回傳失敗
				else
				{
					object errorMessages = "登入失敗";
					Object errorObject = new { errorMessages };
					memberList.Add(errorObject);
					return memberList;
				}
			}
			return memberList;
		}
		public ArrayList registerZeiWeiMember(string fAccount, string fPassword)
		{
			ArrayList memberList = new ArrayList();
			try
			{
				//檢查帳號是否有重覆
				if (db.Member.Where(m => m.MAccount == fAccount).FirstOrDefault() == null)
				{
					db.registerZeiWeiMember(fAccount, fPassword);
					var data = db.Member.Where(m => m.MAccount == fAccount).FirstOrDefault();
					object fid = data.MId;
					object fToken = data.MToken;

					Object memberObject = new { fid, fToken };
					memberList.Add(memberObject);
				}
				//重覆註冊回傳
				else
				{
					object errorMessages = "帳號重複";
					Object errorObject = new { errorMessages };
					memberList.Add(errorObject);
					return memberList;
				}
			}
			//抓取例外
			catch (Exception ex)
			{
				//資料庫錯誤回傳伺服器忙碌
				if (ex is SqlException)
				{
					object errorMessages = "伺服器忙碌中";
					Object errorObject = new { errorMessages };
					memberList.Add(errorObject);
					return memberList;
				}
			}
			return memberList;
		}
		public ArrayList GettCase(string Mid, string MLName, string MLSex, string MLBirYear, string MLBirMonth, string MLBirDate, string MLBirTime)
		{
			SqlConnection conn;

			conn = new SqlConnection();
			//把紫微星耀加入陣列
			string[] lifeArr = {"紫微星","天機星","太陽星","武曲星","天同星","廉貞星","天府星","太陰星","貪狼星","巨門星","天相星","天梁星",
										"七殺星","破軍星","左輔星","右弼星","文昌星","文曲星","祿存星","天馬星","擎羊星","陀羅星","火星","鈴星","天魁星",
										"天鉞星","地空星","地劫星"};
			string MLLife = "";
			string MLMove = "";
			string MLMoney = "";
			string MLCompany = "";
			string MLCouple = "";
			string MLLucky = "";
			//排盤
			int[] arr = ZeiWeiLife();
			//寫入星耀宮位
			MLLife = lifeArr[arr[0] - 1] + "." + lifeArr[arr[1] - 1];
			MLMove = lifeArr[arr[2] - 1] + "." + lifeArr[arr[3] - 1];
			MLMoney = lifeArr[arr[4] - 1] + "." + lifeArr[arr[5] - 1];
			MLCompany = lifeArr[arr[6] - 1] + "." + lifeArr[arr[7] - 1];
			MLCouple = lifeArr[arr[8] - 1] + "." + lifeArr[arr[9] - 1];
			MLLucky = lifeArr[arr[10] - 1] + "." + lifeArr[arr[11] - 1];
			try
			{
				conn.ConnectionString = "Data Source=.;Initial Catalog=ZeiWei;Integrated Security=True";
				conn.Open();
				ArrayList personArray = new ArrayList();
				SqlDataReader MySqlReader = null;
				//找到需更新命盤的會員
				String sqlString = string.Format($"select * from MemberLife where MLNumber =" + Mid);
				SqlCommand cmd = new SqlCommand(sqlString, conn);
				MySqlReader = cmd.ExecuteReader();
				//第一次排盤加入用insert
				if (MySqlReader.Read() == false)
				{
					string sqlString1 = string.Format($"insert into MemberLife (MLNumber,MLName,MLSex,MLBirYear,MLBirMonth" +
						$",MLBirDate,MLBirTime,MLLife,MLMove,MLMoney,MLCompany,MLCouple,MLLucky) values ({Mid}, \'{MLName}\'," +
						$"\'{MLSex}\',\'{MLBirYear}\',\'{MLBirMonth}\',\'{MLBirDate}\',\'{MLBirTime}\',\'{MLLife}\',\'{MLMove}\',\'{MLMoney}\'," +
						$"\'{MLCompany}\',\'{MLCouple}\',\'{MLLucky}\');select * from memberLife where MLNumber = " + Mid);
					MySqlReader.Close();
					MySqlReader = null;
					cmd = new SqlCommand(sqlString1, conn);
					MySqlReader = cmd.ExecuteReader();
					if (MySqlReader.Read() == true)
					{
						object status = "新增資料成功";
						Object b = new { status };
						personArray.Add(b);
					}
					else
					{
						object status = "新增資料失敗";
						Object b = new { status };
						personArray.Add(b);
					}
				}
				else
				{
					//第二次以上的排盤用Update
					string sqlString1 = "";
					if (MLSex != null)
					{
						sqlString1 = string.Format($"update MemberLife set MLName = \'{MLName}\',MLSex = \'{MLSex}\'," +
							$"MLBirYear = \'{MLBirYear}\',MLBirMonth = \'{MLBirMonth}\',MLBirDate = \'{MLBirDate}\'," +
							$"MLBirTime = \'{MLBirTime}\',MLLife = \'{MLLife}\',MLMove = \'{MLMove}\',MLMoney = \'{MLMoney}\'," +
							$"MLCompany = \'{MLCompany}\',MLCouple = \'{MLCouple}\',MLLucky = \'{MLLucky}\' where MLNumber = " + Mid);
					}
					else
					{
						sqlString1 = string.Format($"update MemberLife set MLName = \'{MLName}\'," +
							$"MLBirYear = \'{MLBirYear}\',MLBirMonth = \'{MLBirMonth}\',MLBirDate = \'{MLBirDate}\'," +
							$"MLBirTime = \'{MLBirTime}\',MLLife = \'{MLLife}\',MLMove = \'{MLMove}\',MLMoney = \'{MLMoney}\'," +
							$"MLCompany = \'{MLCompany}\',MLCouple = \'{MLCouple}\',MLLucky = \'{MLLucky}\' where MLNumber = " + Mid);
					}
					MySqlReader.Close();
					MySqlReader = null;
					cmd = new SqlCommand(sqlString1, conn);
					MySqlReader = cmd.ExecuteReader();

					object status = "更新資料成功";
					Object b = new { status };
					personArray.Add(b);
				}
				return personArray;
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex);
				return null;
			}
			finally
			{
				conn.Close();
			}
		}
		static int[] ZeiWeiLife()
		{
			Random r = new Random();
			int[] arr = new int[12];
			int count = 0;
			int num = 0;
			while (count < 12)
			{
				num = r.Next(1, 29);
				if (!IsSelect(num, arr))
				{
					arr[count] = num;
					count++;
				}
			}
			return arr;
		}
		static bool IsSelect(int p1, int[] p2)
		{
			foreach (int i in p2)
			{
				if (p1 == i)
					return true;
			}
			return false;
		}
	}
}