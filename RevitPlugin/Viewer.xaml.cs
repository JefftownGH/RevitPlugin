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
using Autodesk.Revit.DB;

namespace RevitPlugin
{
    public partial class Viewer : Window
    {
        
        private readonly Document _document;

        public class RvtElement
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }

        public Viewer(Document doc)
        {

            _document = doc;
            InitializeComponent();
            DisplayView();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(RvtListView.ItemsSource);
            view.Filter = Filter;
        }

        
        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(IdFilter.Text) && String.IsNullOrEmpty(NameFilter.Text))
                return true;
            
            return (
                    (item as RvtElement).Name.IndexOf(NameFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0 &&
                    (item as RvtElement).Id.IndexOf(IdFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(RvtListView.ItemsSource).Refresh();
            if (RvtListView.Items.Count == 0)
            {
                string message = "Can't find the elements with" + Environment.NewLine;
                if (!string.IsNullOrEmpty(IdFilter.Text))
                    message = message + $" Id: {IdFilter.Text}" + Environment.NewLine;
                if (!string.IsNullOrEmpty(NameFilter.Text))
                    message = message + $" Name: {NameFilter.Text}";

                MessageBox.Show(message);
            }

        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
            IdFilter.Text = string.Empty;
            NameFilter.Text = string.Empty;
            CollectionViewSource.GetDefaultView(RvtListView.ItemsSource).Refresh();
        }

        public void DisplayView()
        {
            List<Element> elements = new FilteredElementCollector(_document).OfClass(typeof(ElementType)).ToList();

            List<RvtElement> items = new List<RvtElement>();

            foreach (Element element in elements)
            {
                items.Add(new RvtElement() { Id = element.UniqueId, Name = element.Name });
            }

            RvtListView.ItemsSource = items;
        }

    }
}
