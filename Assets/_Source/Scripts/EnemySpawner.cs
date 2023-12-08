using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _enemyNumber;
    [SerializeField] private GameObject _enemyPrefab;

    private HashSet<Vector2Int> positions;

    public void Spawn()
    {
        positions = SimpleRandomWalkDungeonGenerator.FloorPositions;
        for(int i = 0; i < _enemyNumber; i++)
        {
            Debug.Log(positions.Count);
            Vector2Int pos = positions.ElementAt(Random.Range(0, positions.Count));
            var enemy = Instantiate(_enemyPrefab, new Vector3(pos.x, pos.y), Quaternion.identity);
        }
    }
}
