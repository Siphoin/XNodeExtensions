using Cysharp.Threading.Tasks;
using UnityEngine;
using XNode;

namespace SiphoinUnityHelpers.XNodeExtensions.NodesControlExecutes
{
    public class IfNode : NodeControlExecute
    {
        [Input(ShowBackingValue.Never, ConnectionType.Override), SerializeField] private bool _condition;
 
        [Output, SerializeField] private NodePort _true;

        [Output, SerializeField] private NodePort _false;

        public override void Execute()
        {
            var condition = GetDataFromPort<bool>(nameof(_condition));

            string portName = $"_{condition}";

            portName = portName.ToLower();

            NodePort targetPort = GetOutputPort(portName);

            ExecuteNodesFromPort(targetPort).Forget();
        }

        public bool NodeContainsOnBranch (BaseNodeInteraction node)
        {
            var portTrue = GetOutputPort(nameof(_true));

            var portFalse = GetOutputPort(nameof(_false));


            var connectionsOnTruePort = portTrue.GetConnections();

            var connectionsOnFalsePort = portFalse.GetConnections();

            if (connectionsOnTruePort != null && connectionsOnFalsePort is null)
            {
                return connectionsOnTruePort.Contains(node.GetEnterPort());
            }

            else if (connectionsOnFalsePort != null && connectionsOnTruePort is null)
            {
                return connectionsOnFalsePort.Contains(node.GetEnterPort());
            }

            else if (connectionsOnFalsePort != null && connectionsOnTruePort != null)
            {
                return connectionsOnFalsePort.Contains(node.GetEnterPort()) || connectionsOnTruePort.Contains(node.GetEnterPort());
            }

            return false;


        }
        }
    }
