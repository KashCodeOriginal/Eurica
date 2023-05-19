using UnityEngine;

public class ButtonLogicDoor : ButtonLogic
{
    [SerializeField] private DoorLogic _door;

    public override void Press()
    {
        base.Press();
        _door.Open();
    }
}
