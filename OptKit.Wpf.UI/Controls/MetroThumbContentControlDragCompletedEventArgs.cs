using System.Windows.Controls.Primitives;

namespace OptKit.Wpf.UI.Controls
{
    public class MetroThumbContentControlDragCompletedEventArgs : DragCompletedEventArgs
    {
        public MetroThumbContentControlDragCompletedEventArgs(double horizontalOffset, double verticalOffset, bool canceled)
            : base(horizontalOffset, verticalOffset, canceled)
        {
            this.RoutedEvent = MetroThumbContentControl.DragCompletedEvent;
        }
    }
}