﻿using UnityEngine;

namespace Unit.GravityCube
{
    [CreateAssetMenu(menuName = "StaticData/GravityCubeSettings", fileName = "GravityCubeSettings")]
    public class GravityCubeSettings : ScriptableObject
    {
        public Color[] ColorVariants;
    }
}