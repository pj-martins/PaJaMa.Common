using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaJaMa.Common
{
	public enum PromptResult
	{
		None,
		Yes,
		YesToAll,
		No,
		NoToAll,
		OK,
		Cancel
	}

	public class PromptEventArgs
	{
		public PromptEventArgs(string message)
		{
			Message = message;
		}

		public string Message { get; private set; }
		public PromptResult Result { get; set; }
	}

	public delegate void PromptEventHandler(object sender, PromptEventArgs e);
}
