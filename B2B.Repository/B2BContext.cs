using B2B.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace B2B.Repository
{
	public class B2BContext : DbContext
	{
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<UserStatus> UserStatuses { get; set; }

		public B2BContext(DbContextOptions<B2BContext> options): base(options)
		{ }
	}
}
