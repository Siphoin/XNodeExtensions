using UnityEngine;

namespace SiphoinUnityHelpers.XNodeExtensions
{
    [NodeTint("#2b335c")]
    public class StartNode : BaseNodeInteraction
    {
        public override void Execute()
        {
            Debug.Log($"node queue from graph {graph.name} started");
        }

        
    }
}
