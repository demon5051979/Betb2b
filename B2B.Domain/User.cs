using System;

namespace B2B.Domain
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int UserStatusId { get; set; }
		public virtual UserStatus UserStatus { get; set; }
	}
}
