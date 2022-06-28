using System.Windows.Forms;
using static Distance.CubemapExporter.Constants;

namespace Distance.CubemapExporter
{
	internal static class Dialogs
	{
		public static SaveFileDialog ExportDirectoryDialog()
		{
			return new SaveFileDialog()
			{
				Filter = DIALOG_FILTER,
				Title = DIALOG_TITLE,
				RestoreDirectory = true
			};
		}
	}
}
