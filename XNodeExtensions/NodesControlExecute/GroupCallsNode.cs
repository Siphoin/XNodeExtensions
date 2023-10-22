using SiphoinUnityHelpers.XNodeExtensions.Attributes;
using UnityEngine;

namespace SiphoinUnityHelpers.XNodeExtensions.NodesControlExecutes
{
    [NodeTint("#4b3359")]
    public class GroupCallsNode : NodeControlExecute
    {
        [SerializeField, ReadOnly(ReadOnlyMode.OnEditor)] private string _name;
        public override void Execute()
        {
                foreach (var item in GetExitPort().GetConnections())
                {
                    ExecuteNodesFromPort(item);
                }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
                name = string.IsNullOrEmpty(_name) ? GetDefaultName() : $"{_name} ({GetDefaultName()})";
        }
#endif
    }
}
