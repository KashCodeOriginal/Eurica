using Unit.GravityCube;
using UnityEngine;

namespace SceneControllers.Scene3
{
    public class Scene3Task1 : MonoBehaviour
    {
        // �� ������� ������ ��������� 1 ���, ����� ������� �� ������ �����. ����� ������ ����� ��� �����-������ � ������ ��� � ������.
        // ����� � ������ ������ ��� ���� ���, � � ����� � ������ �� ����� ���.

        [SerializeField] private GameObject disableAfterColission;
        [SerializeField] private GameObject enableAfterColission;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out GravityCubeLogic cube))
            {
                disableAfterColission.SetActive(false);
                enableAfterColission.SetActive(true);
            }
        }
    }
}