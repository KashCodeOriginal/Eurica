﻿using UnityEngine;

namespace Data.StaticData.LevelData
{
    [CreateAssetMenu(menuName = "StaticData/Level/LevelData", fileName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public string LevelName { get; private set; }
        [field: SerializeField] public bool IsPlayerOnScene { get; private set; }
        [field: SerializeField] public bool IsPlayerInstancingAtStart { get; private set; }
        [field: SerializeField] public bool IsPlayerWeaponInstancingAtStart { get; private set; }
        [field: SerializeField] public bool IsPlayerCameraInstancingAtStart { get; private set; }
        [field: SerializeField] public Vector3 PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public Quaternion PlayerSpawnRotation { get; private set; }
    }
}