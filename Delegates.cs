using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaJaMa.Common
{
	public enum DialogResult
	{
		None,
		Yes,
		YesToAll,
		No,
		NoToAll,
		OK,
		Cancel
	}

	public class DialogEventArgs
	{
		public DialogEventArgs(string message)
		{
			Message = message;
		}

		public string Message { get; private set; }
		public DialogResult Result { get; set; }
	}

	public delegate void DialogEventHandler(object sender, DialogEventArgs e);
}
