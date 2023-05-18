using UnityEngine;

namespace Unit.Portal
{
    public class Teleporter : MonoBehaviour
    {
        private Teleporter _other;
        private LayerMask _layerMaskForTeleport;
        private LayerMask _currentLayerMaskObject;
        private void Awake()
        {
            _layerMaskForTeleport = LayerMask.NameToLayer("Teleporting");
        }
        public void TurnOn(Teleporter other) {
            enabled = true;
            _other = other;
        }

        public void TurnOff() {
            enabled = false;
        }

        private void OnTriggerStay(Collider other)
        {
            float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;
            if (zPos < 0) Teleport(other.transform);
        }

        private void Teleport(Transform obj)
        {
            Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
            localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
            obj.position = _other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

            Quaternion difference = _other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
            obj.rotation = difference * obj.rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            _currentLayerMaskObject = other.gameObject.layer;
            other.gameObject.layer = _layerMaskForTeleport;
        }

        private void OnTriggerExit(Collider other)
        {
            other.gameObject.layer = _currentLayerMaskObject;
        }
    }
}
