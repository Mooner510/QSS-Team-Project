using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public enum EnemyType
    {
        NormalBacteria, Bacteria, Cancer, Virus, CoronaBoss, Unknown
    }

    public static class EnemyTypeExtension {
        public static Color GetRandomColor(this EnemyType enemyType)
        {
            return enemyType switch
            {
                EnemyType.NormalBacteria =>
                    new Color(Random.Range(0f, 0.05f), Random.Range(0f, 0.05f), Random.Range(0.3f, 0.5f)),
                EnemyType.Bacteria =>
                    new Color(Random.Range(0.3f, 0.5f), Random.Range(0.3f, 0.5f), Random.Range(0.6f, 1f)),
                EnemyType.Cancer =>
                    new Color(Random.Range(0.6f, 1f), Random.Range(0f, 0.1f), Random.Range(0f, 0.1f)),
                EnemyType.Virus =>
                    new Color(Random.Range(0f, 0.1f), Random.Range(0.6f, 1f), Random.Range(0f, 0.1f)),
                EnemyType.CoronaBoss =>
                    new Color(Random.Range(0.6f, 1f), Random.Range(0f, 0.1f), Random.Range(0f, 0.1f)),
                EnemyType.Unknown =>
                    new Color(Random.Range(0.6f, 1f), Random.Range(0.6f, 1f), Random.Range(0.6f, 1f)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}