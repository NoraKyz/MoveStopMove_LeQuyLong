﻿using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private List<Level> levels;
        
        public Level GetLevel(int id)
        {
            return levels[id];
        }
    }
}