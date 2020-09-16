using System;
using System.Collections.Generic;
using System.Text;
//Libraries to Read the Tokens from the appsettings.json
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data;

namespace DataAccessLibrary
{
	public static class Helper
	{
		/// <summary>
		/// Read the appsettings.json and get the connection string
		/// </summary>
		/// <param name="databaseConnection">string</param>
		/// <returns>string</returns>
		public static string GetConnectionString(string databaseConnection)
		{
			try
			{
				var configurationBuilder = new ConfigurationBuilder();
				var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
				configurationBuilder.AddJsonFile(path, false);
				var root = configurationBuilder.Build();
				return root.GetSection("ConnectionString").GetSection(databaseConnection).Value;
			}
			catch
			{
				return string.Empty;
			}
		}

		public static string GetStoredProcedure(string spName)
		{
			try
			{
				var configurationBuilder = new ConfigurationBuilder();
				var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
				configurationBuilder.AddJsonFile(path, false);
				var root = configurationBuilder.Build();
				return root.GetSection("StoredProcedures").GetSection(spName).Value;
			}
			catch
			{
				return string.Empty;
			}
		}
	}
}
