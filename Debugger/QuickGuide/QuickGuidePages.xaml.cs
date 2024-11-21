using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
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
using System.Xml;

namespace Debugger.QuickGuide
{
    /// <summary>
    /// Interaction logic for QuickGuidePages.xaml
    /// </summary>
    public partial class QuickGuidePages : UserControl, IContent
    {
        public QuickGuidePages()
        {
            InitializeComponent();
        }

        string lastFragment;
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            if (e.Fragment.Equals(lastFragment))
                return;
            
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {

        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            helpList.Links.Clear();
            string filePath = "Debugger.QuickGuide.QG.xml";
            string code = QuickGuide.GetResourceTextFile(filePath, this);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(code);
            XmlElement root = doc.DocumentElement;
            int node = 0;
            foreach (XmlElement elem in root.ChildNodes)
            {
                helpList.Links.Add(new Link
                {
                    DisplayName = elem.Attributes["name"].Value,
                    Source = new Uri(string.Format("QuickGuide/QuickGuide.xaml#QG:{0}", node), UriKind.Relative)
                });
                ++node;
            }
            helpList.SelectedSource = helpList.Links[0].Source;
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {

        }
    }
}
