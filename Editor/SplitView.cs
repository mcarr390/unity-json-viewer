using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace JsonViewer.Editor
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, GraphView.UxmlTraits> {}
    }
}
