using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaJaMa.Common
{
    public class JsonSerialize
    {
		public static String SerializeObject<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		public static void SerializeObjectToFile<T>(T obj, string path)
		{
			FileInfo inf = new FileInfo(path);
			if (!inf.Directory.Exists) inf.Directory.Create();
			File.WriteAllText(path, SerializeObject<T>(obj));
		}

		public static T DeserializeObjectFromFile<T>(string path)
		{
			FileInfo inf = new FileInfo(path);
			if (!inf.Exists) return default(T);
			try
			{
				return DeserializeObject<T>(File.ReadAllText(path));
			}
			catch
			{
				return default(T);
			}
		}

		public static T DeserializeObject<T>(String json)
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(json);
			}
			catch (Exception e)
			{
				throw new Exception(e.GetFullExceptionText());
			}
		}
	}
}
