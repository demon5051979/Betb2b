using B2B.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace B2B.Services
{
	public interface IUserService
	{
		User CreateUser(User user);
		User RemoveUser(User user);
		User GetUserInfo(int id);
		User SetStatus(User user);
	}
}
