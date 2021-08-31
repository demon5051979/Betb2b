using B2B.Domain;
using B2B.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace B2B.Services
{
	public class UserCacheService: IUserCacheService
	{
		private ConcurrentDictionary<int, User> usersCache = new ConcurrentDictionary<int, User>();
		private readonly ILogger<UserCacheService> _logger;

		public UserCacheService(ILogger<UserCacheService> logger)
		{
			_logger = logger;
		}

		public void AddUser(User user)
		{
			if (usersCache.ContainsKey(user.Id))
			{
				throw new Exception($"User with id = {user.Id} already exists");
			}
			user.UserStatus = new UserStatus() { Id = (int)StatusEnum.New, Name = StatusEnum.New.ToString() };
			usersCache[user.Id] = user;
		}

		public User GetUser(int id)
		{
			if (usersCache.ContainsKey(id))
			{
				return usersCache[id];
			}

			throw new Exception($"User with id = {id} does not exists");
		}

		public User RemoveUser(int id)
		{
			if (usersCache.ContainsKey(id))
			{
				usersCache[id].UserStatusId = (int)StatusEnum.Deleted;
				usersCache[id].UserStatus = new UserStatus() { Id = (int)StatusEnum.Deleted, Name = StatusEnum.Deleted.ToString() };

				return usersCache[id];
			}

			throw new Exception($"User with id = {id} does not exists");
		}

		public User SetStatus(User user)
		{
			if (usersCache.ContainsKey(user.Id))
			{
				var enumStatus = (StatusEnum)user.UserStatusId;
				usersCache[user.Id].UserStatusId = user.UserStatusId;
				usersCache[user.Id].UserStatus = new UserStatus() { Id = user.UserStatusId, Name = enumStatus.ToString() };

				return usersCache[user.Id];
			}

			throw new Exception($"User with id = {user.Id} does not exists");
		}

		public void UpdateUsers(ConcurrentDictionary<int, User> users)
		{
			usersCache = users;
			LogUsers(users.Values);
		}

		private void LogUsers(ICollection<User> users)
		{
			foreach (User user in users)
			{
				_logger.LogInformation($"User: Id = {user.Id}, Name = {user.Name}, Status = {user.UserStatus.Name}");
			}
		}
	}
}
