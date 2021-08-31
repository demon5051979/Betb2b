using System;
using System.Collections.Generic;
using System.Text;

namespace B2B.Domain.Dto
{
	public class StatusResponse: BaseResponse
	{
		UserStatus Status { get; set; }
	}
}
