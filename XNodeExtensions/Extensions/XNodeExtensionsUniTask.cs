using Cysharp.Threading.Tasks;
using SiphoinUnityHelpers.XNodeExtensions.AsyncNodes;

namespace SiphoinUnityHelpers.XNodeExtensions.Extensions
{
    public static class XNodeExtensionsUniTask
    {
        public static async UniTask WaitAsyncNode (AsyncNode node)
        {
           await UniTask.WaitUntil(() => !node.IsWorking);
        }
    }
}
