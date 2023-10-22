using UnityEngine;

namespace SiphoinUnityHelpers.XNodeExtensions.Varitables.Set
{
    public abstract class SetVaritableNode<T> : BaseNodeInteraction
    {
        [Input(ShowBackingValue.Never), SerializeField] private T _varitable;

        [Input, SerializeField] private T _value;

        public override void Execute()
        {
            var outputVaritable = GetInputPort(nameof(_varitable));

            var inputValue = GetInputPort(nameof(_value));

            var connectedVaritables = GetInputPort(nameof(_varitable)).GetConnections();

            

            foreach (var port in connectedVaritables)
            {
                var connectedValue = inputValue.Connection;

                var value = _value;

                if (connectedValue != null)
                {
                    var valueFromInput = connectedValue.GetOutputValue();

                    value = (T)valueFromInput;
                }
                var connectedVaritable = port.node;

                if (connectedVaritable is VaritableNode<T>)
                {
                    var varitableNode = connectedVaritable as VaritableNode<T>;

                    varitableNode.SetValue(value);
                }

                if (connectedVaritable is VaritableCollectionNode<T>)
                {
                    var varitableNode = connectedVaritable as VaritableCollectionNode<T>;

                    int index = RegexCollectionNode.GetIndex(port);

                    varitableNode.SetValue(index, value);
                }
            }
        }



    }
}
