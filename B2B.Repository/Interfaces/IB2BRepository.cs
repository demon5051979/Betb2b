using B2B.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace B2B.Repository
{
	public interface IB2BRepository
	{
		void AddUser(User user);
		User RemoveUser(int id);
		User SetStatus(User user);
		public IEnumerable<User> GetUsers();
	}
}
