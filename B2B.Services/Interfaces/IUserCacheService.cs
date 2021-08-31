using B2B.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace B2B.Services
{
	public interface IUserCacheService
	{
		void AddUser(User user);
		User GetUser(int id);
		User RemoveUser(int id);
		User SetStatus(User user);
		void UpdateUsers(ConcurrentDictionary<int, User> users);
	}
}
