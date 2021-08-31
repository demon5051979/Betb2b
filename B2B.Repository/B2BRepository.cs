using B2B.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B2B.Repository
{
	public class B2BRepository: IB2BRepository
	{
		private B2BContext _context;

		public B2BRepository(B2BContext context)
		{
			_context = context;
		}

		public User GetUser(int id)
		{
			var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
			if (user == null)
			{
				throw new Exception($"User with id = {user.Id} does not exists");
			}

			return user;
		}

		public void AddUser(User user)
		{
			_context.Add(user);
			_context.SaveChanges();

		}

		public User RemoveUser(int id)
		{
			var user = GetUser(id);
			user.UserStatusId = (int)StatusEnum.Deleted;
			_context.Update(user);
			_context.SaveChanges();
			return user;
		}

		public User SetStatus(User userNewStatus)
		{
			var user = GetUser(userNewStatus.Id);
			user.UserStatusId = userNewStatus.UserStatusId;
			_context.Update(user);
			_context.SaveChanges();
			return user;
		}

		public IEnumerable<User> GetUsers()
		{
			return _context.Users.Include(u => u.UserStatus).AsNoTracking();
		}
	}
}
