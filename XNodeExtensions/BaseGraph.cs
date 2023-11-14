using XNode;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;
using SiphoinUnityHelpers.XNodeExtensions.Debugging;

namespace SiphoinUnityHelpers.XNodeExtensions
{
    public abstract class BaseGraph : NodeGraph
    {

        private NodeQueue _queue;

        private IDictionary<string, VaritableNode> _varitables;

        public event Action OnEndExecute;

        public event Action<BaseNode> OnNextNode;

        public bool IsPaused { get; private set; }

        public bool IsRunning => _queue is null ? false : !_queue.IsEnding;

        protected NodeQueue Queue => _queue;

        public IDictionary<string, VaritableNode> Varitables => _varitables;

        public virtual void Execute ()
        {
            var queue = new List<BaseNodeInteraction>();

            BuidVaritableNodes();

            for (int i = 0; nodes.Count > i; i++)
            {
                var node = nodes[i];

                if (node is BaseNodeInteraction)
                {
                    queue.Add(node as BaseNodeInteraction);
                }
            }

            _queue = new NodeQueue(this, queue);

            ExecuteProcess().Forget();

        }

        protected void BuidVaritableNodes()
        {
            if (_varitables is null)
            {
                Dictionary<string, VaritableNode> nodes = new Dictionary<string, VaritableNode>();

                foreach (var node in this.nodes)
                {
                    if (node is VaritableNode)
                    {
                        VaritableNode varitableNode = node as VaritableNode;
                        nodes.Add(varitableNode.Name, varitableNode);
                    }
                }

                _varitables = nodes;
            }
        }

        public T GetValueFromVaritable<T>(string name)
        {
            var node = _varitables[name];

            if (node is null)
            {
                throw new NullReferenceException($"Varitable Node with name not found");
            }

            var value = node.GetCurrentValue();

            if (value.GetType() != typeof(T))
            {
                throw new InvalidCastException($"varitable node {node.Name} have type {value.GetType()}. Argument type {typeof(T)}");
            }

            return (T)value;
        }

        public void Continue ()
        {
            IsPaused = true;
        }

        public void Pause ()
        {
            IsPaused = false;
        }

        public void Stop ()
        {

            End();

            _queue.Exit();       
        }

        public BaseNode GetNodeByGuid (string guid)
        {
            foreach (var node in from item in nodes
                                 let node = item as BaseNode
                                 where node.GUID == guid
                                 select node)
            {
                return node;
            }

            return null;
        }

        private async UniTask ExecuteProcess ()
        {
            _queue.OnEnd += End;

            for (int i = 0; i < _queue.Count; i++)
            {
                await UniTask.WaitUntil(() => !IsPaused);

                var node = await _queue.Next();

                if (node is null)
                {
                    Debug.Log("666");

                    break;
                }

                OnNextNode?.Invoke(node);

               
            }
        }

        private void End()
        {
            _queue.OnEnd -= End;

            OnEndExecute?.Invoke();

            ResetVaritables();

            XNodeExtensionsDebug.Log($"graph {name} end execute");
        }

        private void ResetVaritables()
        {
            foreach (var node in nodes.Where(node => node is VaritableNode))
            {
                VaritableNode varitableNode = node as VaritableNode;

                varitableNode.ResetValue();
            }
        }
    }
}
