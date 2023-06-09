using System.Collections.Generic;

using Spawners;

using UnityEngine;

public class RoadBlock : PoolObject
{
    [SerializeField] private Transform _dockingPosition;
    [SerializeField] private Transform[] _obstacleSpawnPositions;
    public Vector3 DockingPosition { get => _dockingPosition.position; }

    private Transform[,] _spawnPostions;
    private LinkedList<int> _freePositionRowIndexes;
    private int _numberPointsInRow = 0;

    private void Awake()
    {
        _freePositionRowIndexes = new LinkedList<int>();

        int rowsCount = _obstacleSpawnPositions.Length;
        _numberPointsInRow = _obstacleSpawnPositions[0].childCount;
        _spawnPostions = new Transform[rowsCount, _numberPointsInRow];

        for(int i = 0; i < rowsCount; i++)
        {
            _freePositionRowIndexes.AddLast(i);

            for (int j = 0; j < _numberPointsInRow; j++)
            {
                _spawnPostions[i, j] = _obstacleSpawnPositions[i].GetChild(j);
            }
        }
    }

    public bool PlaceObstacleAtRandomPosition(Obstacle obstacle)
    {
        int index = GetRandomIndex();

        if(index != -1)
        {
            Transform obstacleTrans = obstacle.transform;
            Transform spawnPosTrans = _spawnPostions[index, Random.Range(0, _numberPointsInRow)];

            obstacleTrans.position = spawnPosTrans.position;
            obstacleTrans.parent = spawnPosTrans;

            obstacle.OccupiedPositionsRowIndex = index;
            obstacle.PositionsRowIsFreeEvent += PositionsRowIsFreeEventHandler;

            return true;
        }
        else
        {
            return false;
        }
    }

    private int GetRandomIndex()
    {
        if (_freePositionRowIndexes.Count > 0)
        {
            int idexesCount = _freePositionRowIndexes.Count;
            int index = Random.Range(0, idexesCount);

            var currentListNode = _freePositionRowIndexes.First;

            for (int i = 1; i < idexesCount; i++)
            {
                currentListNode = currentListNode.Next;

                if (i == index)
                {
                    break;
                }
            }

            _freePositionRowIndexes.Remove(currentListNode);
            return currentListNode.Value;
        }
        
        return -1;
    }

    private void PositionsRowIsFreeEventHandler(Obstacle obstacle, int index)
    {
        obstacle.PositionsRowIsFreeEvent -= PositionsRowIsFreeEventHandler;
        _freePositionRowIndexes.AddLast(index);
    }
}