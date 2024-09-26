using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "EnemyPool", menuName = "Scriptable Object/Enemy Pool"), Serializable]
public class EnemyPool : ScriptableObject
{
    public List<EnemyGroup> enemyGroups = new List<EnemyGroup>();

    [Serializable]
    public class EnemyGroup
    {
        public Enemy enemy;
        public float weight; // chance of spawn 0-1
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyPool))]
public class EnemyPoolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnemyPool pool = (EnemyPool)target;
        if (GUILayout.Button("Normalize Weights"))
        {
            float totalWeight = pool.enemyGroups.Sum(e => e.weight);
            foreach (var e in pool.enemyGroups)
                e.weight /= totalWeight;
        }    
    }
}
#endif
