using UnityEngine;

namespace Sources.Presentations.Views
{
    public class FlagView : MonoBehaviour
    {
        public void SetPosition(Vector3 position) => 
            gameObject.transform.position = position;
    }
}