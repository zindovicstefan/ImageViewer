using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace PraksaProjekat.Model
{

    public class FileService
    {

		private string initialDirectory;

		public string[] Load(string[] fileFormats)
        {
			using (var fbd = new FolderBrowserDialog())
			{
				DialogResult result = fbd.ShowDialog();
				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
				{
					initialDirectory = fbd.SelectedPath;
					return GetFilesFrom(initialDirectory, fileFormats, true);
				}
				return new string[] { };
			}
		}
		private string[] GetFilesFrom(string searchFolder, string[] filters, bool isRecursive)
		{
			List<string> filesFound = new List<string>();
			var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
			foreach (var filter in filters)
			{
				filesFound.AddRange(Directory.GetFiles(searchFolder, string.Format("*.{0}", filter), searchOption));
			}
			return filesFound.ToArray();
		}

		public void Save(Image image, string imageFormat)
        {
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.InitialDirectory = initialDirectory;
			saveFileDialog.Filter = "Gif Image (.gif)|*.gif|Jpeg Image (.jpeg)|*.jpeg|Png Image (.png)|*.png";
			saveFileDialog.Title = "Save an Image File";
            switch (imageFormat)
            {
				case ".gif":
					saveFileDialog.FilterIndex = 1;
					break;
				case ".jpeg":
					saveFileDialog.FilterIndex = 2;
					break;
				case ".jpg":
					saveFileDialog.FilterIndex = 2;
					break;
				case ".png":
					saveFileDialog.FilterIndex = 3;
					break;
            }
			DialogResult result = saveFileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				BitmapEncoder encoder = new JpegBitmapEncoder();
				switch (saveFileDialog.Filter)
				{
					case ".gif":
						encoder = new GifBitmapEncoder();
						break;
					case ".jpeg":
						encoder = new JpegBitmapEncoder();
						break;
					case ".jpg":
						encoder = new JpegBitmapEncoder();
						break;
					case ".png":
						encoder = new PngBitmapEncoder();
						break;
				}
				encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image.Source));
				using (var stream = (FileStream)saveFileDialog.OpenFile())
				{
					encoder.Save(stream);
				}
			}
		}
	}
}
