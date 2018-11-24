using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace OptKit.Wpf.UI.Controls
{
    /// <summary>
    /// The MetroHeaderAutomationPeer class exposes the <see cref="T:OptKit.Wpf.UI.Controls.MetroHeader" /> type to UI Automation.
    /// </summary>
    public class MetroHeaderAutomationPeer : GroupBoxAutomationPeer
    {
        public MetroHeaderAutomationPeer(GroupBox owner)
            : base(owner)
        {
        }

        protected override string GetClassNameCore()
        {
            return "MetroHeader";
        }
    }
}