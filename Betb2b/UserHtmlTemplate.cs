using B2B.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Betb2b
{
	public static class UserHtmlTemplate
	{
		public const string userTemplateHtml = @"
			<html>
				<body>
					<div><strong>User</strong></div>
					<hr>	
					<div>Id: {0}</div>
					<div>Name: {1}</div>
					<div>Status: {2}</div>
				</body>
			</html>";

		public static string GetUserHtmlResponse(User user)
		{
			return string.Format(userTemplateHtml, user.Id, user.Name, user.UserStatus?.Name);
		}
	}
}
