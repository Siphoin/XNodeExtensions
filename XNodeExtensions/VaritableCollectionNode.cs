using UnityEngine;
using XNode;
using SiphoinUnityHelpers.XNodeExtensions.Attributes;
using SiphoinUnityHelpers.XNodeExtensions.Extensions;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json.Linq;
#if UNITY_EDITOR
#endif

namespace SiphoinUnityHelpers.XNodeExtensions
{
    public abstract class VaritableCollectionNode<T> : VaritableNode
    {
        private List<T> _startValue;

        [Space(10)]

        [SerializeField, Output(ShowBackingValue.Always, dynamicPortList = true), ReadOnly(ReadOnlyMode.OnEditor)] private T[] _elements;

        [Space(10)]

        [SerializeField, Output(ShowBackingValue.Never), ReadOnly(ReadOnlyMode.OnEditor)] private NodePortEnumerable _enumerable;
        public override object GetStartValue()
        {
            return _startValue;
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName != nameof(_enumerable))
            {
                int index = RegexCollectionNode.GetIndex(port);

                return _elements[index];
            }

            else
            {
                return _elements.AsEnumerable();
            }
        }


        public void SetValue(IEnumerable<T> value)
        {
            _elements = value.ToArray();
        }

        public void SetValue(int index, T value)
        {
            _elements[index] = value;
        }

        public override object GetCurrentValue()
        {
            return _elements.ToArray();
        }



        public override void ResetValue()
        {
            _elements = _startValue.ToArray();
        }
#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            Validate();
        }

        protected override void Validate()
        {
            base.Validate();

            if (!Application.isPlaying)
            {
                _startValue = _elements.ToList();
            }
        }

        protected override void ValidateName()
        {
            name = Color.ToColorTag($"{Name} ({GetDefaultName()}[])");
        }


#endif

    }




}
