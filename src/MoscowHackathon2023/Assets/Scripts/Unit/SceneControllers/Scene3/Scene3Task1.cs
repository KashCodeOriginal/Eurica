using Unit.GravityCube;
using UnityEngine;

namespace SceneControllers.Scene3
{
    public class Scene3Task1 : MonoBehaviour
    {
        // На локации игрока встречает 1 куб, мирно лежащий по центру холла. Игрок должен взять куб грави-пушкой и кинуть его в люстру.
        // Тогда с люстры упадет еще один куб, и в итоге у игрока их будет два.

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