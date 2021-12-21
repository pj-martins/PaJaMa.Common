using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaJaMa.Common
{
	public class SettingsHelper
	{
		private static string getUserSettingsPathForExtension(string settingsFileName, string extension)
        {
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				Assembly.GetEntryAssembly().AssemblyTitle(),
				settingsFileName + "." + extension);
		}
		private static string getUserSettingsPath(string settingsFileName)
		{
			return getUserSettingsPathForExtension(settingsFileName, "json");
		}

		private static string getUserSettingsPathLegacy(string settingsFileName)
		{
			return getUserSettingsPathForExtension(settingsFileName, "xml");
		}

		public static TUserSettings GetUserSettings<TUserSettings>(string settingFileName = null)
			where TUserSettings : class, new()
		{
			var basePath = settingFileName ?? typeof(TUserSettings).Name;
			var path = getUserSettingsPath(basePath);
			if (!File.Exists(path))
			{
				var legacyPath = getUserSettingsPathLegacy(basePath);
				if (File.Exists(legacyPath))
				{
					var legacySettings = XmlSerialize.DeserializeObjectFromFile<TUserSettings>(legacyPath);
					SaveUserSettings<TUserSettings>(legacySettings, basePath);
				}
				else
				{
					return new TUserSettings();
				}
			}
			try
			{
				var settings = JsonConvert.DeserializeObject<TUserSettings>(File.ReadAllText(getUserSettingsPath(settingFileName ?? typeof(TUserSettings).Name)));
				return settings ?? new TUserSettings();
			}
			catch
            {
				return new TUserSettings();
            }
		}

		public static void SaveUserSettings<TUserSettings>(TUserSettings userSettings, string settingFileName = null)
		{
			var finf = new FileInfo(getUserSettingsPath(settingFileName ?? typeof(TUserSettings).Name));
			if (!finf.Directory.Exists)
				finf.Directory.Create();
			File.WriteAllText(finf.FullName, JsonConvert.SerializeObject(userSettings));
		}
	}
}
