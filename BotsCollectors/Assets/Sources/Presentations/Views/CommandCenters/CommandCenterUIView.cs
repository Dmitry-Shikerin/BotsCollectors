using UnityEngine;

namespace Sources.Presentations.Views.CommandCenters
{
    public class CommandCenterUIView : MonoBehaviour
    {
        [field : SerializeField] public TextUI ExtractedResourcesView { get; private set; }
        [field : SerializeField] public TextUI FoundedResourcesView { get; private set; }
        [field : SerializeField] public TextUI CollectorsCountView { get; private set; }
        [field : SerializeField] public TextUI MessageView { get; private set; }
    }
}