using Unit.Portal;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private Transform _placeInHand;
    private IWeaponedView _currentWeaponView;
    private LayerMask _layerMask;

    private void Start()
    {
        _layerMask = LayerMask.NameToLayer("PickUp");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) { 
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));//Center the screen in the crosshairs

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.layer == _layerMask)
                {
                    IWeaponedView weaponedView = hit.collider.gameObject.GetComponent<IWeaponedView>();
                    if (weaponedView != null) {
                        if (_currentWeaponView != null) {
                            _currentWeaponView.Release();
                        }
                        weaponedView.PickUp(_placeInHand);
                        _currentWeaponView = weaponedView;
                    }                                        
                }                 
            }  
        }
    }
}
