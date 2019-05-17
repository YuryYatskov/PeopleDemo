using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControls
{
    public static class Placement
    {
        public static void PlaceMyTabItem(MyTabItem TabItemSource, MyTabItem TabItemTarget = null,  MyTabControl TabControlTarget = null)
        {
            if (TabItemTarget == null && TabControlTarget == null)
                return;

            if (TabItemSource != null && TabItemTarget != null)
            {

            }
            else if (TabItemSource != null && TabControlTarget != null)
            {
                if (TabControlTarget.ItemsSource == null)
                {
                    // Here I need to create a collection of ViewModels with the type as the TabItemSource.Content.
                }

                if (TabControlTarget.ItemsSource is IList list)
                {
                    var source = TabItemSource.Content;

                    list.Remove(source);
                    list.Add(source);

                    TabControlTarget.SelectedIndex = TabControlTarget.Items.Count - 1;
                }
            }

        }
    }
}
