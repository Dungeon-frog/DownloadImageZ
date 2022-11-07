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
using System.Net;
using System.IO;

namespace DownloadImageZ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void Srch(object sender, RoutedEventArgs e)
        {
            try
            {
                img.Source = null;
                byte[] response = new WebClient().DownloadData(txt.Text);
                BitmapImage uriSource = LoadImage(response);
                img.Source = uriSource;
            }
            catch (Exception)
            {
                MessageBox.Show("Введите коректный URL", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
            
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                using (var fs = new FileStream("Picture.png", FileMode.Create))
                {
                    mem.WriteTo(fs);
                }
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;

        }
    }
}
