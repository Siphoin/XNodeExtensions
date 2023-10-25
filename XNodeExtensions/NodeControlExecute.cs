using Cysharp.Threading.Tasks;
using SiphoinUnityHelpers.XNodeExtensions.AsyncNodes;
using SiphoinUnityHelpers.XNodeExtensions.Debugging;
using SiphoinUnityHelpers.XNodeExtensions.Extensions;
using SNEngine.AsyncNodes;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using XNode;

namespace SiphoinUnityHelpers.XNodeExtensions
{
    [NodeTint("#593d6b")]
    public abstract class NodeControlExecute : BaseNodeInteraction, IIncludeWaitingNode
    {
        private CancellationTokenSource _cancellationTokenSource;

        public bool IsWorking => _cancellationTokenSource != null;

        protected async UniTask ExecuteNodesFromPort(NodePort port)
        {
            var connections = port.GetConnections();

            if (connections != null)
            {
                _cancellationTokenSource = new CancellationTokenSource();

                foreach (var connect in connections)
                {
                    if (connect.Connection != null)
                    {
                        var node = connect.node as BaseNodeInteraction;

                        node.Execute();

                        if (node is IIncludeWaitingNode)
                        {
                            var asyncNode = node as IIncludeWaitingNode;

                            XNodeExtensionsDebug.Log($"Wait node <b>{node.name} GUID: {node.GUID} (in {name} GUID {GUID})</b>");

                            await XNodeExtensionsUniTask.WaitAsyncNode(asyncNode, _cancellationTokenSource);
                        }
                    }
                }
            }

            _cancellationTokenSource?.Cancel();

            _cancellationTokenSource = null;
        }
    }
}
