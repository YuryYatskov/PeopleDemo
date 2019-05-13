using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
    ///     <MyNamespace:MyTabItem/>
    ///
    /// </summary>
    public class MyTabItem : TabItem
    {
        static MyTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyTabItem), new FrameworkPropertyMetadata(typeof(MyTabItem)));
        }

        public MyTabItem()
        {
            this.AllowDrop = true;
            this.PreviewMouseMove += MyTabItem_PreviewMouseMove;
            this.Drop += MyTabItem_Drop;
        }

        private void MyTabItem_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.Source is MyTabItem MyTabItem && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(MyTabItem, MyTabItem, DragDropEffects.All);
        }

        private void MyTabItem_Drop(object sender, DragEventArgs e)
        {
            if (GetTargetTabItem(e.OriginalSource) is MyTabItem tabItemTarget
                && e.Data.GetData(typeof(MyTabItem)) is MyTabItem tabItemSource
                && VisualTreeHelper.GetParent(tabItemSource) is System.Windows.Controls.Primitives.TabPanel tabPanel
                && VisualTreeHelper.GetParent(tabPanel) is Grid grid
                && VisualTreeHelper.GetParent(grid) is MyTabControl tabControl)
            {
                int sourceIndex = tabControl.Items.IndexOf(tabItemSource);
                int targetIndex = tabControl.Items.IndexOf(tabItemTarget);

                tabControl.Items.Remove(tabItemSource);
                tabControl.Items.Insert(targetIndex, tabItemSource);

                tabControl.Items.Remove(tabItemTarget);
                tabControl.Items.Insert(sourceIndex, tabItemTarget);

                tabControl.SelectedIndex = targetIndex;
            }
        }

        private MyTabItem GetTargetTabItem(object originalSource)
        {
            var dependencyObject = originalSource as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is MyTabItem MyTabItem)
                    return MyTabItem;

                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            return null;
        }
    }
}
