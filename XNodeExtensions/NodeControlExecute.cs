using XNode;

namespace SiphoinUnityHelpers.XNodeExtensions
{
    [NodeTint("#593d6b")]
    public abstract class NodeControlExecute : BaseNodeInteraction
    {

        protected void ExecuteNodesFromPort(NodePort port)
        {
            if (port.Connection != null)
            {
                var node = port.node as BaseNode;

                node.Execute();
            }
        }
    }
}
