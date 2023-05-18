using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    [SerializeField] private Transform _headTransform;

    public Transform HeadTransform => _headTransform;
}
