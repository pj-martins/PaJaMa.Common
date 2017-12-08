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
		private static string getUserSettingsPath(Type settingsType)
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				Assembly.GetEntryAssembly().AssemblyTitle(),
				settingsType.Name + ".xml");
		}

		public static TUserSettings GetUserSettings<TUserSettings>()
			where TUserSettings : class, new()
		{
			var path = getUserSettingsPath(typeof(TUserSettings));
			if (!File.Exists(path)) return new TUserSettings();
			return XmlSerialize.DeserializeObjectFromFile<TUserSettings>(getUserSettingsPath(typeof(TUserSettings)));
		}

		public static void SaveUserSettings<TUserSettings>(TUserSettings userSettings)
		{
			var finf = new FileInfo(getUserSettingsPath(typeof(TUserSettings)));
			if (!finf.Directory.Exists)
				finf.Directory.Create();
			XmlSerialize.SerializeObjectToFile<TUserSettings>(userSettings, finf.FullName);
		}
	}
}
