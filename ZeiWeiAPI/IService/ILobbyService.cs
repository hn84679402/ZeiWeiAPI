using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeiWeiAPI.IService
{
	interface ILobbyService
	{
		ArrayList selectAllMember(int memberId);
		ArrayList getLife(int fid);
	}
}
