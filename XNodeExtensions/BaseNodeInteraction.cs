using SiphoinUnityHelpers.Attributes;
using SiphoinUnityHelpers.XNodeExtensions.Attributes;
using UnityEngine;
using XNode;

namespace SiphoinUnityHelpers.XNodeExtensions
{
    public abstract class BaseNodeInteraction : BaseNode
    {
        [SerializeField, ReadOnly(ReadOnlyMode.OnEditor), NodeControlExecuteField] private bool _enabled;

        public bool Enabled => _enabled;

        [Input(ShowBackingValue.Never), SerializeField] private NodePort _enter;

        [Output, SerializeField] private NodePort _exit;

        public NodePort Enter => GetEnterPort();

        public NodePort Exit => GetExitPort();

        public void SetEnable(bool enable)
        {
            if (Application.isPlaying)
            {
                return;
            }

            _enabled = enable;
        }
        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);

            SetEnable(Exit.Connection != null || Enter.Connection != null);
        }

        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);

            SetEnable(Exit.Connection != null || Enter.Connection != null);


        }

        public NodePort GetEnterPort()
        {
            return GetInputPort(nameof(_enter));
        }

        public NodePort GetExitPort()
        {
            return GetOutputPort(nameof(_exit));
        }
        public NodePort GetConnectionFromExitPort ()
        {
            return Exit.Connection;
        }

        public NodePort GetConnectionFromEnterPort()
        {
            return Enter.Connection;
        }

    }
}
