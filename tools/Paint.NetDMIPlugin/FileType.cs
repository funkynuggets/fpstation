using System.IO;
using System.Drawing;

// Support plugin so you don't need to rename DMI files to load them.
// Made for the latest build of PDN as of 09/04/2020 (and that's not the American date format)
// There is NO documentation for this shit. The Dark Art of PDN Plugins is quite well laid out in this repo, though:
// https://github.com/0xC0000054/pdn-ddsfiletype-plus

namespace PaintDotNet.DMIPlugin
{
	public sealed class DMIFileType : FileType
	{
		public DMIFileType() :
			base("BYOND Graphic", new FileTypeOptions()
			{
				LoadExtensions = new string[] { ".dmi" },
				SaveExtensions = new string[0]
			})
		{ }

		protected override Document OnLoad(Stream input)
		{
			// Get the PNG-*cough* DMI and load it
			Bitmap b = new Bitmap(input, true);
			return Document.FromImage(b);
		}

		// We do not do any save operations as DMIs are literally PNG files.
		// PDN has us covered with some good options for PNG exporting already.
	}

	public sealed class DMIFileTypeFactory : IFileTypeFactory2
	{
		public FileType[] GetFileTypeInstances(IFileTypeHost host)
		{
			// Tell PDN we exist and are a usable file type
			return new FileType[] { new DMIFileType() };
		}
	}
}
