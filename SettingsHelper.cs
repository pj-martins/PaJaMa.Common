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
		private static string getUserSettingsPath(string settingsFileName)
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				Assembly.GetEntryAssembly().AssemblyTitle(),
				settingsFileName + ".xml");
		}

		public static TUserSettings GetUserSettings<TUserSettings>(string settingFileName = null)
			where TUserSettings : class, new()
		{
			var path = getUserSettingsPath(settingFileName ?? typeof(TUserSettings).Name);
			if (!File.Exists(path)) return new TUserSettings();
			var settings = XmlSerialize.DeserializeObjectFromFile<TUserSettings>(getUserSettingsPath(settingFileName ?? typeof(TUserSettings).Name));
			return settings ?? new TUserSettings();
		}

		public static void SaveUserSettings<TUserSettings>(TUserSettings userSettings, string settingFileName = null)
		{
			var finf = new FileInfo(getUserSettingsPath(settingFileName ?? typeof(TUserSettings).Name));
			if (!finf.Directory.Exists)
				finf.Directory.Create();
			XmlSerialize.SerializeObjectToFile<TUserSettings>(userSettings, finf.FullName);
		}
	}
}
