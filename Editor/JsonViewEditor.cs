using System;
using JsonViewer.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class JsonViewEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private JsonView jsonView;
    private InspectorView inspectorView;
    
    [MenuItem("Window/UI Toolkit/JsonViewEditor")]
    public static void ShowExample()
    {
        JsonViewEditor wnd = GetWindow<JsonViewEditor>();
        wnd.titleContent = new GUIContent("JsonViewEditor");
    }

    [MenuItem("Window/UI Toolkit/DoIt")]
    public void DoIt()
    {
    }
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/JsonViewer/Editor/JsonViewEditor.uxml");
        visualTree.CloneTree(root);
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/JsonViewer/Editor/JsonViewEditor.uss");
        root.styleSheets.Add(styleSheet);

        jsonView = root.Q<JsonView>();
        inspectorView = root.Q<InspectorView>();
        
        OnSelectionChange();
    }

    private void OnSelectionChange()
    {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (tree)
        {
            jsonView.PopulateView(tree);
        }
    }
}
