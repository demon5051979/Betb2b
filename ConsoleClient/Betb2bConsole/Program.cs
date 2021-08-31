using B2B.Domain;
using B2B.Services.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Betb2bConsole
{
	class Program
	{
		const string apiUrl = "https://localhost:5001/";
		private static readonly HttpClient client = new HttpClient();
		static async Task Main(string[] args)
		{
			client.DefaultRequestHeaders.Add("User-Agent", "Betb2b console");
			client.AddAuthHeader();

			bool exit = false;
			while (!exit)
			{
				Console.WriteLine(@"
				Choose your destiny
				1. CreateUser
				2. RemoveUser
				3. User info
				4. Set status
				Esc. Exit");

				var pressed = Console.ReadKey();

				try
				{
					switch (pressed.Key)
					{
						case ConsoleKey.D1: await CreateUser(); break;
						case ConsoleKey.D2: await RemoveUser(); break;
						case ConsoleKey.D3: await UserInfo(); break;
						case ConsoleKey.D4: await SetUserStatus(); break;
						case ConsoleKey.Escape: exit = true; break;
						default: break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}
		}

		private static async Task SetUserStatus()
		{
			int id = ConsoleHelper.inputInt("user id");
			int statusId = ConsoleHelper.inputInt("new user status id");

			List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
			parameters.Add(new KeyValuePair<string, string>("Id", id.ToString()));
			parameters.Add(new KeyValuePair<string, string>("UserStatusId", statusId.ToString()));

			ConsoleHelper.ShowRequestInfo(string.Empty);

			using (var content = new FormUrlEncodedContent(parameters))
			{
				content.Headers.Clear();
				content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

				var response = await client.PostAsync($"{apiUrl}Auth/SetStatus", content);

				ConsoleHelper.ShowResponseInfo(response);
			}
		}

		private static async Task UserInfo()
		{
			int id = ConsoleHelper.inputInt("user id");
			ConsoleHelper.ShowRequestInfo(string.Empty);
			var response = await client.GetAsync($"{apiUrl}Public/UserInfo?id={id}");
			ConsoleHelper.ShowResponseInfo(response);
		}

		private static async Task RemoveUser()
		{
			User user = new User();
			user.Id = ConsoleHelper.inputInt("user id");

			var jsonUser = JsonConvert.SerializeObject(user);
			ConsoleHelper.ShowRequestInfo(jsonUser);

			HttpContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
			var response = await client.PostAsync($"{apiUrl}Auth/RemoveUser", content);

			ConsoleHelper.ShowResponseInfo(response);
		}

		private static async Task CreateUser()		
		{
			User user = new User();
			user.Id = ConsoleHelper.inputInt("user id");
			user.Name = ConsoleHelper.inputString("user name");
			user.UserStatusId = (int)StatusEnum.New;

			var serializer = new XmlSerializer(typeof(User));
			string userXml = serializer.SerializeToString(user);

			ConsoleHelper.ShowRequestInfo(userXml);

			HttpContent content = new StringContent(userXml, Encoding.UTF8, "application/xml");
			var response = await client.PostAsync($"{apiUrl}Auth/CreateUser", content);

			ConsoleHelper.ShowResponseInfo(response);
		}

	}
}
