using System;
using System.Collections.Generic;
using System.Text;

namespace B2B.Domain.Dto
{
	public abstract class BaseResponse
	{
		public bool Success = true;

		public int ErrorId = 0;

		public string Msg;
	}
}
