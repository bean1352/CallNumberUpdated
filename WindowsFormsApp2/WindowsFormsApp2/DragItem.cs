using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    class DragItem
    {
        public ListBox Client;
        public int Index;
        public object Item;

        public DragItem(ListBox client, int index, object item)
        {
            Client = client;
            Index = index;
            Item = item;
        }
    }
}
