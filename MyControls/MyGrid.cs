using System;
using System.Collections;
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

namespace MyControls
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyControls"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MyControls;assembly=MyControls"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:MyGrid/>
    ///
    /// </summary>
    public class MyGrid : Grid
    {
        private MyTabControl _instanceTabControl;

        public MyTabControl InstanceTabControl
        {
            get
            {
                if (_instanceTabControl == null)
                {
                    _instanceTabControl = new MyTabControl();
                    _instanceTabControl.Background = Brushes.Azure;
                    _instanceTabControl.VerticalAlignment = VerticalAlignment.Stretch;
                    _instanceTabControl.HorizontalAlignment = HorizontalAlignment.Stretch;

                    this.Children.Add(_instanceTabControl);
                    Grid.SetRow(_instanceTabControl, 0);
                    Grid.SetColumn(_instanceTabControl, 0);
                }
                return _instanceTabControl;
            }
        }

        static MyGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyGrid), new FrameworkPropertyMetadata(typeof(MyGrid)));
        }

        public MyGrid()
        {
            this.AllowDrop = true;
            this.PreviewMouseMove += MyGrid_PreviewMouseMove;
            this.Drop += MyGrid_Drop;
        }


        private void MyGrid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Source is MyTabItem MyTabItem && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(MyTabItem, MyTabItem, DragDropEffects.All);
        }

        private void MyGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(MyTabItem)) is MyTabItem tabItemSource)
            {
                if (InstanceTabControl.ItemsSource == null && Placement.FuncGetMyTabControlItemsSource != null)
                {
                    // Here I need to create a collection of ViewModels with the type as the TabItemSource.Content.
                    InstanceTabControl.ItemsSource = Placement.GetMyTabControlItemsSource(Placement.FuncGetMyTabControlItemsSource);
                }

                if (InstanceTabControl.ItemsSource is IList list)
                {
                    var source = tabItemSource.Content;

                    list.Remove(source);
                    list.Add(source);

                    InstanceTabControl.SelectedIndex = list.Count - 1;
                }
                else
                {
                    InstanceTabControl.Items.Remove(tabItemSource);
                    InstanceTabControl.Items.Add(tabItemSource);

                    InstanceTabControl.SelectedIndex = InstanceTabControl.Items.Count - 1;
                }

            }
        }
    }
}
