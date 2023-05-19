using UnityEngine;

public class ButtonLogicDoor : ButtonLogic
{
    [SerializeField] private DoorLogic _door;

    private void OnTriggerEnter(Collider other)
    {
        // Detect Gravity Cube colission
        if (other.gameObject.layer == LayerMask.NameToLayer("InteractiveObjectForGravity")
            || other.gameObject.layer == LayerMask.NameToLayer("Grabbed"))
        {
            if (!_isPressed)
            {
                Press();
            }
        }
    }

    public override void Press()
    {
        base.Press();
        _door.Open();
    }
}
