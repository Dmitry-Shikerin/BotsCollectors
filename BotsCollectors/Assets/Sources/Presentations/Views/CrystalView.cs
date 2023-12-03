using Sources.Domain.Constants;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentations.Views
{
    [RequireComponent(typeof(NavMeshObstacle))]
    [RequireComponent(typeof(Rigidbody))]
    public class CrystalView : MonoBehaviour, ICrystalView
    {
        private NavMeshObstacle _navMeshObstacle;

        public Vector3 Position => transform.position;

        private void Awake() => 
            _navMeshObstacle = GetComponent<NavMeshObstacle>();

        public void SetPosition(Vector3 position) =>
            transform.position = position;

        public void Destroy()
        {
            if (TryGetComponent(out PoolableObject poolableObject) == false)
            {
                Destroy(gameObject);

                return;
            }

            poolableObject.ReturnTooPool();
        }

        public void SetUnavailable() => 
            _navMeshObstacle.enabled = false;

        public void SetAvailable()
        {
            if (_navMeshObstacle.enabled == false)
                _navMeshObstacle.enabled = true;
        }

        public void SetParent(Transform parent)
        {
            if (parent == null)
            {
                transform.parent = null;

                return;
            }

            transform.parent = parent.transform;
        }

        public void SetLocalPosition(Vector3 position) => 
            transform.localPosition = position;

        public void RemoveParent()
        {
            SetParent(null);
            SetPosition(transform.position);
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            gameObject.layer = LayerMaskConstants.TouchableLayer;
        }

        public void Hide() => 
            gameObject.SetActive(false);

        public void ChangeLayerMask() =>
            gameObject.layer = LayerMaskConstants.Default;
    }
}