using UnityEngine;

public class ButtonLogicDoor : ButtonLogic
{
    [SerializeField] private DoorLogic _door;

    private void OnTriggerEnter(Collider other)
    {
        var interactiveLayer = LayerMask.NameToLayer("InteractiveObjectForGravity"); // Gravity Cube
        if (other.gameObject.layer == interactiveLayer)
        {
            Press();
        }
    }

    public override void Press()
    {
        base.Press();
        _door.Open();
    }
}
