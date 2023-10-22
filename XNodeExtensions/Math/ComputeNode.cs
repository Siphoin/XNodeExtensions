using System.Data;
using UnityEngine;
using XNode;

namespace SiphoinUnityHelpers.XNodeExtensions.Math
{
    [NodeTint("#3d6b6b")]
    public class ComputeNode : BaseNode
    {
        [SerializeField, Input(ShowBackingValue.Never, ConnectionType.Override)] private Object _a;

        [SerializeField, Input(ShowBackingValue.Never, ConnectionType.Override)] private Object _b;

        [SerializeField] private ComputeType _type;

        [SerializeField, Output(ShowBackingValue.Never)] private bool _result;

        public override object GetValue(NodePort port)
        {
            var a = GetDataFromPort<object>(nameof(_a));

            var b = GetDataFromPort<object>(nameof(_b));

            if (a is null || b is null)
            {
                return "a or b on compute is null";
            }

            return ComputeHelper.Compute(a, b, _type);



        }
    }
}
