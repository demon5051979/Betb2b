using System;
using System.Collections.Generic;
using System.Text;

namespace B2B.Domain.Dto
{
	public class ErrorResponse: BaseResponse
	{
		public string ErrorMsg { get; set; }

		public ErrorResponse() 
		{
			ErrorId = 1;
			Success = false;
		}
	}
}
