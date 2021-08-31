using System;
using System.Collections.Generic;
using System.Text;

namespace B2B.Domain.Dto
{
	public class UserResponse: BaseResponse
	{
		public User User { get; set; }
	}
}
