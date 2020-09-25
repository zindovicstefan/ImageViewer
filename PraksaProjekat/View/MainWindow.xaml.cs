using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace PraksaProjekat
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : System.Windows.Window
	{
		private MainWindowViewModel viewModel = new MainWindowViewModel();

		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = viewModel;
		}

		private void imageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;
			
			if ( e.AddedItems.Count > 0)
			{
				var valid = e.AddedItems[0];
				foreach(var item in new ArrayList(listBox.SelectedItems))
				{
					if ( item != valid )
					{
						listBox.SelectedItems.Remove(item);
					}
				}
			}

			int selectedIndex = listBox.SelectedIndex;
			if (selectedIndex == -1) return;
			ObservableCollection<ImageWrapper> imageList = viewModel.ImageList;
			viewModel.ImageFormat = Path.GetExtension(imageList[selectedIndex].ImagePath); 
			Uri uri = new Uri(imageList[selectedIndex].ImagePath, UriKind.Absolute);
			BitmapImage bitmapSource = new BitmapImage(uri);
			Image image = new Image() { Source = bitmapSource };

			image.Source = bitmapSource;
			viewModel.Image = image;
		}
	}
}
