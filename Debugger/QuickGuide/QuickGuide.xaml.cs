using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Xml;

namespace Debugger.QuickGuide
{
    /// <summary>
    /// Interaction logic for QuickGuide.xaml
    /// </summary>
    public partial class QuickGuide : UserControl, IContent
    {
        string LastFragment { get; set; }

        public QuickGuide()
        {
            InitializeComponent();
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            if (e.Fragment.Equals(LastFragment))
                return;
            contentStack.Children.Clear();
            
            LastFragment = e.Fragment;
            string[] parts = e.Fragment.Split(':');
            int idx = int.Parse(parts[1]);
            string filePath = string.Format("Debugger.QuickGuide.{0}.xml", parts[0]);
            string code = GetResourceTextFile(filePath, this);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(code);
            XmlElement root = doc.DocumentElement;
            int node = 0;
            foreach (XmlElement elem in root.ChildNodes)
            {
                if (node == idx)
                {
                    foreach (XmlElement text in elem.ChildNodes)
                    {
                        if (text.Name.Equals("img"))
                        {
                            Image img = new Image();
                            BitmapImage bmp = new BitmapImage();
                            bmp.BeginInit();
                            bmp.UriSource = new Uri(string.Format("pack://application:,,,/asDevelop;component/Images/{0}", text.InnerText), UriKind.Absolute);
                            bmp.EndInit();
                            img.Source = bmp;
                            img.Stretch = Stretch.Uniform;
                            img.Width = bmp.PixelWidth/2;
                            img.Height = bmp.PixelHeight/2;
                            img.MaxWidth = bmp.PixelWidth/2;
                            img.MaxHeight = bmp.PixelHeight/2;

                            contentStack.Children.Add(img);
                        }
                        else
                        {
                            BBCodeBlock block = new BBCodeBlock();
                            block.Margin = new Thickness(5);
                            block.BBCode = text.InnerText;
                            if (text.Name.ToLower().Equals("h"))
                                contentStack.Children.Add(new Separator());
                            contentStack.Children.Add(block);
                        }
                    }
                    return;
                }
                ++node;
            }
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            
        }

        public static string GetResourceTextFile(string filename, object ctx)
        {
            string result = string.Empty;

            using (Stream stream = ctx.GetType().Assembly.GetManifestResourceStream(filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }
    }
}
