using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class JsonViewEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/JsonViewEditor")]
    public static void ShowExample()
    {
        JsonViewEditor wnd = GetWindow<JsonViewEditor>();
        wnd.titleContent = new GUIContent("JsonViewEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/JsonViewEditor.uxml");
        visualTree.CloneTree(root);
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/JsonViewEditor.uss");
        root.styleSheets.Add(styleSheet);
        
    }
}
