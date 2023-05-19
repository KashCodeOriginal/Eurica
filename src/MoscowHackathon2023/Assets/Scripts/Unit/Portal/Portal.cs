using System.Collections;
using UnityEngine;

namespace Unit.Portal 
{ 
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Camera _portalСamera;
        [SerializeField] private MeshRenderer _portalView;
        [SerializeField] private Texture _closeViewTexture;
        [SerializeField] private Teleport teleport;

        private Transform _playerTransform;

        private Coroutine _portalBroadcast;
        public Camera PortalСamera => _portalСamera;
        public Teleport Teleport => teleport;

        public void Construct(Portal oppositePortal, Transform playerTransform)
        {
            _playerTransform = playerTransform;
            
            if (oppositePortal != null) 
            {
                Open(oppositePortal);
                oppositePortal.Open(this);
            } 
            else 
            { 
                Close();
            }
        }

        //Opens only when the second portal appears
        private void Open(Portal oppositePortal)
        {            
            oppositePortal.PortalСamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            _portalView.material.mainTexture = oppositePortal.PortalСamera.targetTexture;
            _portalBroadcast = StartCoroutine(PortalBroadcast(oppositePortal));
            teleport.TurnOn(oppositePortal.Teleport);
            oppositePortal.Teleport.TurnOn(teleport);
            
        }
        //Closes if the second portal is missing
        private void Close()
        {

            if (_portalBroadcast != null) 
            {
                StopCoroutine(_portalBroadcast);
            }
            
            _portalView.sharedMaterial.mainTexture = _closeViewTexture;
            teleport.TurnOff();
        }

        private IEnumerator PortalBroadcast(Portal otherPortal)
        {
            while (true) 
            {
                yield return null;
                if (Camera.main == null)
                {
                    continue;
                }
                
                Vector3 lookerPosition = otherPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
                lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
                _portalСamera.transform.localPosition = lookerPosition;

                Quaternion difference = transform.rotation * Quaternion.Inverse(otherPortal.transform.rotation * Quaternion.Euler(0,180,0));
                
                _portalСamera.transform.rotation = difference * Camera.main.transform.rotation;

                _portalСamera.nearClipPlane = lookerPosition.magnitude;
            }
        }        
    }
}


