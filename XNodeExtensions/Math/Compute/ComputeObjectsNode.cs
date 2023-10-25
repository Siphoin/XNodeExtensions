using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SiphoinUnityHelpers.XNodeExtensions.Math.Compute
{
    public class ComputeObjectsNode : ComputeNode
    {
        [SerializeField, Input(ShowBackingValue.Never, ConnectionType.Override)] private Object _a;

        [SerializeField, Input(ShowBackingValue.Never, ConnectionType.Override)] private Object _b;

        [SerializeField] private ComputeObjectsType _type;

        protected override bool Compute()
        {
            var a = GetDataFromPort<object>(nameof(_a));

            var b = GetDataFromPort<object>(nameof(_b));

            if (a is null || b is null)
            {
                throw new NullReferenceException($"any argument on Compute Objects Node (GUID: {GUID}) as null ");
            }

            return ComputeHelper.Compute(a, b, _type);
        }
    }
}
