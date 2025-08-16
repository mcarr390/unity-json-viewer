using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
namespace JsonViewer.Editor
{
    public class JsonView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<JsonView, GraphView.UxmlTraits> {}

        private BehaviourTree tree;
        public JsonView() 
        {
            Insert(0, new GridBackground());
            
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/JsonViewer/Editor/JsonViewEditor.uss");
            styleSheets.Add(styleSheet);
        }

        internal void PopulateView(BehaviourTree tree)
        {
            this.tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            
            DeleteElements(graphElements);
            
            graphViewChanged += OnGraphViewChanged;

            
            tree.nodes.ForEach(n => CreateNodeView(n));
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()
                .Where(endPort => endPort.direction != startPort.direction &&
                                  endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange change)
        {
            if (change.elementsToRemove != null)
            {
                foreach (var e in change.elementsToRemove)
                {
                    if (e is NodeView nv)
                    {
                        Undo.RegisterCompleteObjectUndo(tree, "Delete Node");
                        tree.DeleteNode(nv.node);
                        EditorUtility.SetDirty(tree);
                    }
                }
            }
            return change;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            {
                var types = TypeCache.GetTypesDerivedFrom<Node>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }
            
            evt.menu.AppendAction("Delete", _ => DeleteSelection(), a => selection.Count > 0 ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
        }

        void CreateNode(System.Type type)
        {
            Node node = tree.CreateNode(type);
            CreateNodeView(node);
        }

        void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            AddElement(nodeView);
        }
    }
}
