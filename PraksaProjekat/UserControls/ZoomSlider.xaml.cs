using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PraksaProjekat
{
    /// <summary>
    /// Interaction logic for ZoomSlider.xaml
    /// </summary>
    public partial class ZoomSlider : UserControl, INotifyPropertyChanged
    {

        private const int MinValue = 500, MaxValue = 2000, InitialValue = 750;

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set 
            {
                if (value < MinValue || value > MaxValue)
                    return;
                SetValue(ValueProperty, value);
                int percentage = (int)((double)value / InitialValue * 100);
                PercentageValue = percentage + "%";
            }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(ZoomSlider), new PropertyMetadata(0, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if ((int)e.NewValue <= MaxValue && (int)e.NewValue >= 0)
                ((ZoomSlider)obj).Value = (int)e.NewValue;
        }

        private string percentageValue;
        public string PercentageValue
        {
            get { return percentageValue; }
            set
            {
                percentageValue = value;
                OnPropertyChanged("PercentageValue");
            }
        }

        public ZoomSlider()
        {
            Value = InitialValue;
            InitializeComponent();
        }

        private void zoomIn_Click(object sender, EventArgs e)
        {
            Value+=5;
        }

        private void zoomOut_Click(object sender, EventArgs e)
        {
            Value-=5;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
