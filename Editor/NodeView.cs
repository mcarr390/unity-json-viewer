using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Node node;
    public Port input;
    public Port output;
    public NodeView(Node node)
    {
        this.node = node;
        this.title = node.name;
        this.viewDataKey = node.guid;
        
        style.left = node.position.x;
        style.top = node.position.y;


        CreateInputPorts();
        CreateOutputPorts();
        
        
        // Make sure deletion is allowed
        capabilities |= Capabilities.Deletable | Capabilities.Selectable | Capabilities.Movable | Capabilities.Ascendable | Capabilities.Collapsible;
    }

    void CreateInputPorts()
    {
        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

        if (input != null)
        {
            input.portName = "";
            inputContainer.Add(input);
        }
    }

    void CreateOutputPorts()
    {
        output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

        if (output != null)
        {
            output.portName = "";
            inputContainer.Add(output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
    }
}
