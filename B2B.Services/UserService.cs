using B2B.Domain;
using B2B.Domain.Dto;
using B2B.Repository;
using B2B.Services.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace B2B.Services
{
	public class UserService: IUserService
	{
		private readonly IUserCacheService _userCacheService;
		private readonly IB2BRepository _repo;

		public UserService(IUserCacheService userCacheService, IB2BRepository repo)
		{
			_userCacheService = userCacheService;
			_repo = repo;
		}

		public User CreateUser(User user)
		{
			_repo.AddUser(user);
			_userCacheService.AddUser(user);

			return user;
		}

		public User RemoveUser(User user)
		{
			_repo.RemoveUser(user.Id);
			user = _userCacheService.RemoveUser(user.Id);
			return user;
		}

		public User GetUserInfo(int id)
		{
			var user = _userCacheService.GetUser(id);
			return user;
		}

		public User SetStatus(User user)
		{
			_repo.SetStatus(user);
			user = _userCacheService.SetStatus(user);
			return user;
		}
	}
}
