using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Betb2bConsole
{
	public static class ConsoleHelper
	{
		public static void AddAuthHeader(this HttpClient httpClient)
		{
			string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes("admin:admin"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
		}

		public static int inputInt(string paramName)
		{
			while (true)
			{
				Console.WriteLine();
				Console.Write($"Input {paramName}:");
				string value = Console.ReadLine();
				int result;
				if (int.TryParse(value, out result))
				{
					return result;
				}
				else
				{
					Console.WriteLine("Incorrect value");
				}
			}
		}

		public static string inputString(string paramName)
		{
			while (true)
			{
				Console.WriteLine();
				Console.Write($"Input {paramName}:");
				string value = Console.ReadLine();

				if (!string.IsNullOrEmpty(value))
				{
					return value;
				}
				else
				{
					Console.WriteLine("Incorrect value");
				}
			}
		}

		public static void ShowRequestInfo(string info)
		{
			Console.WriteLine();
			Console.WriteLine("**************REQUEST******************");
			Console.WriteLine(info);
		}

		public static async void ShowResponseInfo(HttpResponseMessage response)
		{
			var result = await response.Content.ReadAsStringAsync();

			Console.WriteLine();
			Console.WriteLine("**************RESPONSE******************");
			Console.Write(result);
			Console.WriteLine();
		}
	}
}
