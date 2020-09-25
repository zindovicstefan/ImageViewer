using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PraksaProjekat
{
	public class ImageWrapper
	{
		public string ImagePath { get; set; }
		public string Name { get; set; }

		public ImageWrapper(string imagePath,string name)
		{
			ImagePath = imagePath;
			Name = name;
		}
	}
}
