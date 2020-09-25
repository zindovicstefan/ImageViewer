using System;
using System.Collections.Generic;
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
    /// Interaction logic for SliderNumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDownSlider : UserControl
    {
        public NumericUpDownSlider()
        {
            InitializeComponent();
        }


        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set 
            {
                if (value < MinValue || value > MaxValue)
                    return;
                SetValue(ValueProperty, value); 
            }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDownSlider), new PropertyMetadata(0));



        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericUpDownSlider), new PropertyMetadata(0));



        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(int), typeof(NumericUpDownSlider), new PropertyMetadata(0));



        public object TrackImage
        {
            get { return (object)GetValue(TrackImageProperty); }
            set { SetValue(TrackImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrackImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackImageProperty =
            DependencyProperty.Register("TrackImage", typeof(object), typeof(NumericUpDownSlider), new PropertyMetadata());



        private void upButton_Click(object sender, EventArgs e)
        {
            Value++;
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            Value--;
        }


    }
}
