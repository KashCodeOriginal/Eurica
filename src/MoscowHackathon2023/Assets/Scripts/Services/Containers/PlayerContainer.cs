using UnityEngine;

namespace Services.Containers
{
    public class PlayerContainer : IPlayerContainer
    {
        public GameObject Player { get; private set; }

        public void SetUp(GameObject player)
        {
            Player = player;
        }
    }
}