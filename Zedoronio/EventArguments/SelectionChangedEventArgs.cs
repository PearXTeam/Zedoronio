using System;

namespace Zedoronio.EventArguments
{
    public class SelectionChangedEventArgs : EventArgs
    {
        public SelectionChangedEventArgs(bool sel)
        {
            Selected = sel;
        }
        public bool Selected { get; set; }
    }
}