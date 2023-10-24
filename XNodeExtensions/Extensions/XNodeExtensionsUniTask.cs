using Cysharp.Threading.Tasks;
using SiphoinUnityHelpers.XNodeExtensions.AsyncNodes;
using System.Threading;

namespace SiphoinUnityHelpers.XNodeExtensions.Extensions
{
    public static class XNodeExtensionsUniTask
    {
        public static async UniTask WaitAsyncNode (AsyncNode node, CancellationTokenSource cancellationTokenSource)
        {
           await UniTask.WaitUntil(() => !node.IsWorking, cancellationToken: cancellationTokenSource.Token);
        }
    }
}
