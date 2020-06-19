using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using Button = System.Windows.Controls.Button;
using TreeView = System.Windows.Controls.TreeView;

namespace TRIAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : INotifyPropertyChanged
    {
        private string sourceXmlFile;
        public MainWindow()
        {

             InitializeComponent();
            binddatagrid();
            txtblk.Visibility = System.Windows.Visibility.Hidden;
            windowloaded();

        }
     
        private void windowloaded()
        {
          
            StringBuilder result = new StringBuilder();

            foreach (XElement level1Element in XElement.Load(@"C:\Users\cc149\source\MyNew\wpf\Trial\XMLFile.xml").Elements("Project"))
            {
               
                    result.AppendLine(level1Element.Attribute("Name").Value+"/");
                
            }
          
            List<string> addnames = new List<string>();
             string[] stringArray = result.ToString().Split('/').ToArray();
            stringArray = stringArray.Take(stringArray.Count() - 1).ToArray();
           //Array.Resize(ref stringArray, stringArray.Length - 1);

            foreach (var arrayname in stringArray)
            {
                var names = arrayname.Replace("\r\n", "");
                addnames.Add(names);
            }
            List<string> list = addnames.ToList();
            List<Button> buttons = new List<Button>();

            foreach (var buttoname in list)
            {
                var newButton = new Button() { Name= buttoname,Height=39,Foreground=Brushes.Black,Content=buttoname};
               // buttons.Add(newButton);
                 this.mainpanel.Children.Add(newButton);
                newButton.Click += newbutton_click;

            }
            //  ic.ItemsSource = buttons;



        }

        private void newbutton_click(object sender , RoutedEventArgs e)
        {
            
            string content = (sender as Button).Content.ToString();
            var menuButton = new Button() { Name = content, Height = 34,Width=226, Foreground = Brushes.Black, Content = content };
            
            if (txtblk.Children.Count > 0)
            {
                txtblk.Children.RemoveAt(txtblk.Children.Count - 1);
            }
            txtblk.Children.Add(menuButton);
            txtblk.Visibility = System.Windows.Visibility.Visible;



            XDocument xmlData = new XDocument();
            string xmlFilePath = "C:/Users/cc149/source/MyNew/wpf/Trial/XMLFile.xml";
            sourceXmlFile = xmlFilePath;
            xmlData = XDocument.Load(sourceXmlFile, LoadOptions.None);
            if (xmlData == null)
            {
                throw new XmlException("Cannot load Xml document from file : " + sourceXmlFile);
            }
            else
            {
                
                TreeViewItem treeNode = new TreeViewItem
                {
                    Header = content,
                    IsExpanded = true
                };


                BuildNodes(treeNode, xmlData.Root, content);
                treeview.Items.Clear();
                treeview.Items.Add(treeNode);
            }


        }

            private void BuildNodes(TreeViewItem treeNode, XElement element, string content)
            {
            string attributes = "";
                if (element.HasAttributes)
                {
                    foreach (var att in element.Attributes().Where(p => !element.Attributes().Any(q => (p.Name == "ProjectId"))))
                    {
                   
                        attributes += "   " + att.Name + " = " + att.Value;

                   
                    }
                   
                    
                }

            TreeViewItem childTreeNode = new TreeViewItem
            {
                Header = element.Name.LocalName + attributes,
                IsExpanded = true
            };
           
            if (element.HasElements)
                {
                    foreach (XElement childElement in element.Elements())
                    {
                    if (childElement.FirstAttribute.Value.Equals(content))
                    {
                        BuildNodes(childTreeNode, childElement,content);

                    }
                    }
                }
                else
                {
                    TreeViewItem childTreeNodeText = new TreeViewItem
                    {
                        Header = element.Value,
                        IsExpanded = true
                    };
                    childTreeNode.Items.Add(childTreeNodeText);
                }
            
                treeNode.Items.Add(childTreeNode);
            }



        private void StackPanel_MouseDown(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            MessageBox.Show("content");
            StackPanel test = (StackPanel)sender;
            foreach (FrameworkElement element in test.Children)
            {
                var name = element.Name;
            }
           
            MessageBox.Show("ave");
            
        }
        private string _filter;
        private string temp;

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter == value) return;
                _filter = value;
                OnPropertyChanged();
                NotifyPropertyChanged("Filter");

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        public IEnumerable binddatagrid()
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connEmployee"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select Id,BrokerCode,BrokerName,RTTMCode,DTCNo,Notes,TradeType,Name,Phone,Fax,Email from [SMAFI]";
            cmd.Connection = con;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("SMAFI");
            adapter.Fill(dt);
            g1.AutoGenerateColumns = false;           
            return g1.ItemsSource = dt.DefaultView;

        }

        public void Search_Name(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connEmployee"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select BrokerCode,BrokerName,RTTMCode,DTCNo,Notes,TradeType,Name,Phone,Fax,Email from [SMAFI]";
            cmd.Connection = con;
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable("SMAFI");
            var getdata = adapter1.Fill(dt1);
            List<Details> details = new List<Details>();
            details = (from DataRow row in dt1.Rows
                       select new Details()
                       {
                           BrokerCode = row["BrokerCode"].ToString(),
                           BrokerName = row["BrokerName"].ToString(),
                           RTTMCode = row["RTTMCode"].ToString(),
                           DTCNo = row["DTCNo"].ToString(),
                           Notes = row["Notes"].ToString(),
                           TradeType = row["TradeType"].ToString(),
                           Name = row["Name"].ToString(),
                           Phone = row["Phone"].ToString(),
                           Fax = row["Fax"].ToString(),
                           Email = row["Email"].ToString()

                       }).ToList();

            g1.ItemsSource = details;
            details = details.Where(x => x.BrokerName.ToLower().StartsWith(NameBox.Text.ToLower())).ToList();
            g1.ItemsSource = details;
        }

            public void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connEmployee"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select BrokerCode,BrokerName,RTTMCode,DTCNo,Notes,TradeType,Name,Phone,Fax,Email from [SMAFI]";
            cmd.Connection = con;
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable("SMAFI");
            var getdata = adapter1.Fill(dt1);
            List<Details> details = new List<Details>();
            details = (from DataRow row in dt1.Rows
                       select new Details()
                       {
                           BrokerCode = row["BrokerCode"].ToString(),
                           BrokerName = row["BrokerName"].ToString(),
                           RTTMCode = row["RTTMCode"].ToString(),
                           DTCNo = row["DTCNo"].ToString(),
                           Notes = row["Notes"].ToString(),
                           TradeType = row["TradeType"].ToString(),
                           Name = row["Name"].ToString(),
                           Phone = row["Phone"].ToString(),
                           Fax = row["Fax"].ToString(),
                           Email = row["Email"].ToString()

                       }).ToList();

            g1.ItemsSource = details;
            details = details.Where(x => x.BrokerCode.ToLower().StartsWith(Filter.ToLower())).ToList();
            details = details.Where(x => x.BrokerCode.ToLower().StartsWith(NameBox.Text.ToLower())).ToList();            
            g1.ItemsSource = details;
            



        }

        [ValueConversion(typeof(string), typeof(string))]
        public class RatioConverter : MarkupExtension, IValueConverter
        {
            private static RatioConverter _instance;

            public RatioConverter() { }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            { 
                var size = 20;
                return size;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            { // read only converter...
                throw new NotImplementedException();
            }

            public override object ProvideValue(IServiceProvider serviceProvider)
            {
                return _instance ?? (_instance = new RatioConverter());
            }

        }

        public DataGridCell GetCell(int row, int column)

        {

            DataGridRow rowContainer = GetRow(row);

            if (rowContainer != null)

            {

                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                if (presenter == null)

                {

                    g1.ScrollIntoView(rowContainer, g1.Columns[column]);

                    presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);

                return cell;

            }

            return null;

        }

        public DataGridRow GetRow(int index)

        {

            DataGridRow row = (DataGridRow)g1.ItemContainerGenerator.ContainerFromIndex(index);

            if (row == null)

            {

                g1.UpdateLayout();

                g1.ScrollIntoView(g1.Items[index]);

                row = (DataGridRow)g1.ItemContainerGenerator.ContainerFromIndex(index);

            }

            return row;

        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual

        {

            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < numVisuals; i++)

            {

                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);

                child = v as T;

                if (child == null)

                {

                    child = GetVisualChild<T>

                    (v);

                }

                if (child != null)

                {

                    break;

                }

            }

            return child;

        }


        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;

                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null) break;
                }
                else
                    if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;

                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                    else
                    {
                        foundChild = FindChild<T>(child, childName);

                        if (foundChild != null)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
        public void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connEmployee"].ConnectionString;
            con.Open();
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select BrokerCode,BrokerName,RTTMCode,DTCNo,Notes,TradeType,Name,Phone,Fax,Email from [SMAFI]";
            cmd.Connection = con;
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable("SMAFI");
            var getdata = adapter1.Fill(dt1);
            List<Details> details = new List<Details>();
            details = (from DataRow row in dt1.Rows
                       select new Details()
                       {
                           BrokerCode = row["BrokerCode"].ToString(),
                           BrokerName = row["BrokerName"].ToString(),
                           RTTMCode = row["RTTMCode"].ToString(),
                           DTCNo = row["DTCNo"].ToString(),
                           Notes = row["Notes"].ToString(),
                           TradeType = row["TradeType"].ToString(),
                           Name = row["Name"].ToString(),
                           Phone = row["Phone"].ToString(),
                           Fax = row["Fax"].ToString(),
                           Email = row["Email"].ToString()

                       }).ToList();

            g1.ItemsSource = details;
           

        }
        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // drill down from DataGridRow, through row view to our order row
            DataGridRow dgRow = e.Row;
            DataRowView rowView = dgRow.Item as DataRowView;
            Details d = new Details();
            d.BrokerCode = g1.SelectedValue as string;


        }

        
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            
            List<Details> details = new List<Details>();

            details = g1.ItemsSource as List<Details>;

        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties) row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        private void page_click(object sender, MouseButtonEventArgs e)
        {
            g1.Visibility = System.Windows.Visibility.Hidden;
           
            _mainFrame.Navigate(new Page1());
            _mainFrame.NavigationService.Navigate(new Page1());
        }
        public void ChangeView(Page1 view)
        {
            _mainFrame.NavigationService.Navigate(view);
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
         
            string filename = System.AppDomain.CurrentDomain.BaseDirectory;
            filename = "C:/Users/cc149/source//MyNew/wpf/Trial/XMLFile.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlDataProvider provider = new XmlDataProvider();
            provider.Document = doc;
            
            provider.XPath = "./*";
            
         //   trvItems.DataContext = provider;

        }

        public void Search_RTTM(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connEmployee"].ConnectionString;
            con.Open();
           
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select BrokerCode,BrokerName,RTTMCode,DTCNo,Notes,TradeType,Name,Phone,Fax,Email from [SMAFI]";
            cmd.Connection = con;
            SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable("SMAFI");
            var getdata = adapter1.Fill(dt1);
            List<Details> details = new List<Details>();
            details = (from DataRow row in dt1.Rows
                       select new Details()
                       {
                           BrokerCode = row["BrokerCode"].ToString(),
                           BrokerName = row["BrokerName"].ToString(),
                           RTTMCode = row["RTTMCode"].ToString(),
                           DTCNo = row["DTCNo"].ToString(),
                           Notes = row["Notes"].ToString(),
                           TradeType = row["TradeType"].ToString(),
                           Name = row["Name"].ToString(),
                           Phone = row["Phone"].ToString(),
                           Fax = row["Fax"].ToString(),
                           Email = row["Email"].ToString()

                       }).ToList();

            g1.ItemsSource = details;
             details = details.Where(x => x.RTTMCode.ToLower().StartsWith(RTTM.Text.ToLower())).ToList();



            g1.ItemsSource = details;
        }
        public void Search_DTC(object sender, TextChangedEventArgs e)
        {

        }
        public void Search_Notes(object sender, TextChangedEventArgs e)
        {

        }
        public void Search_NameName(object sender, TextChangedEventArgs e)
        {

        }
        public void Search_Phone(object sender, TextChangedEventArgs e)
        {

        }
        public void Search_Fax(object sender, TextChangedEventArgs e)
        {

        }
        public void Search_Email(object sender, TextChangedEventArgs e)
        {

        }
        

        public class MenuItem
        {
            public MenuItem()
            {
                this.Items = new ObservableCollection<MenuItem>();
            }

            public string Title { get; set; }

            public ObservableCollection<MenuItem> Items { get; set; }
        }
        

        private void SelectionChanged(object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            //Perform actions when SelectedItem changes
            //MessageBox.Show(((TreeViewItem)e.NewValue).Tag.;
        }
        
        private void newitem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
           // var item = (e.NewValue as TreeViewItem).IsSelected;
            //if (e.NewValue is Item)
            //{
            //    Item item = e.NewValue as Item;
            //    if (Item != SelectedItem)
            //    {
            //        //keep SelectedItem in sync with Treeview.SelectedItem
            //        SelectedItem = e.NewValue as Item;
            //    }

            //}
            //else
            //{
            //    var item = e.NewValue as HierarchicalGroup;
            //    item.IsExpanded = true;
            //    if (item.Children.Count() > 0)
            //    {
            //        if (item.Children[0] is Item)
            //        {
            //            (item.Children[0] as Item).IsSelected = true;
            //        }
            //    }
            //}


        }
        public class Details
        {
            public int Id { get; set; }
            public string BrokerCode { get; set; }
            public string BrokerName { get; set; }
            public string RTTMCode { get; set; }
            public string DTCNo { get; set; }
            public string Notes { get; set; }
            public string TradeType { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }


        }

        public class EmployeeType
        {
            private string v;

            public EmployeeType(string v)
            {
                this.v = v;
            }
        }

        public class HierachicalGroup : INotifyPropertyChanged
        {
            public virtual string Name { get; set; }
            public virtual HierachicalGroup[] Children { get; set; }
            public virtual HierachicalGroup Parent { get; set; }

            private bool _isSelected;
            public bool IsSelected
            {
                get { return _isSelected; }
                set
                {
                    if (value != _isSelected)
                    {
                        _isSelected = value;
                        this.OnPropertyChanged("IsSelected");
                    }
                }
            }


            private bool _isExpanded;
            public bool IsExpanded
            {
                get { return _isExpanded; }
                set
                {
                    if (value != _isExpanded)
                    {
                        _isExpanded = value;
                        this.OnPropertyChanged("IsExpanded");
                    }

                    // Expand all the way up to the root.
                    if (_isExpanded && Parent != null)
                        Parent.IsExpanded = true;
                }
            }


            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion // INotifyPropertyChanged Members
        }
        public class Item : HierachicalGroup, INotifyPropertyChanged
        {
            
            private bool _isSelected;
            public bool IsSelected
            {
                get { return _isSelected; }
                set
                {
                    if (value != _isSelected)
                    {
                        _isSelected = value;
                        this.OnPropertyChanged("IsSelected");
                    }
                }
            }

            private void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, e);
            }



            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
