using UnityEngine;

namespace Services.Containers
{
    public interface IPlayerContainer
    {
        public GameObject Player { get; }

        public void SetUp(GameObject player);
    }
}