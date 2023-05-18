using System.Collections;
using UnityEngine;

namespace PortalGunMechanics { 
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Camera _portalСamera;
        [SerializeField] private MeshRenderer _portalView;
        [SerializeField] private Texture _closeViewTexture;
        [SerializeField] private Teleporter _teleporter;

        private PortalType _portalType;
        private Coroutine _portalBroadcast;
        public Camera PortalСamera { get => _portalСamera; }
        public Teleporter Teleporter { get => _teleporter; }

        public void Construct(PortalType portalType) {
            _portalType = portalType;
        }

        //Opens only when the second portal appears
        private void Open(Portal otherPortal)
        {            
            otherPortal.PortalСamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            _portalView.sharedMaterial.mainTexture = otherPortal.PortalСamera.targetTexture;
            _portalBroadcast = StartCoroutine(PortalBroadcast(otherPortal));
            _teleporter.TurnOn(otherPortal.Teleporter);
            
        }
        //Closes if the second portal is missing
        private void Close() {
            StopCoroutine(_portalBroadcast);
            _portalView.sharedMaterial.mainTexture = _closeViewTexture;
            _teleporter.TurnOff();
        }

        private IEnumerator PortalBroadcast(Portal otherPortal) {

            while (true) {
                yield return null;
                if (Camera.main != null) { 
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
}


