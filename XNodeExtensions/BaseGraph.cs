using XNode;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace SiphoinUnityHelpers.XNodeExtensions
{
    public abstract class BaseGraph : NodeGraph
    {
        public bool IsPaused { get; private set; }

        private NodeQueue _queue;

        private CancellationTokenSource _cancellationTokenSource;

        public event Action OnEndExecute;

        public event Action<BaseNode> OnNextNode;

        public void Execute ()
        {
            var queue = new List<BaseNodeInteraction>();

            _cancellationTokenSource = new CancellationTokenSource();


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
            _cancellationTokenSource.Cancel();

            _queue = null;

            _cancellationTokenSource = null;

            End();


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

            for (int i = 0; _queue.Count > i; i++)
            {
               await UniTask.WaitUntil(() => !IsPaused, cancellationToken: _cancellationTokenSource.Token);

               var node = await _queue.Next();

                OnNextNode?.Invoke(node);
            }
        }

        private void End()
        {
            _queue.OnEnd -= End;

            OnEndExecute?.Invoke();

            ResetVaritables();

            Debug.Log($"graph {name} end execute");
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
