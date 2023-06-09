using Spawners;

using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    
    [SerializeField, Space] private RandomlyFromSeveralObjectPool<RoadBlock> _roadBlocksPool;
    [SerializeField] private RoadBlocksMover _roadBlocksMover;
    [SerializeField] private int _roadBlocsCount = 5;

    public float RoadWidth => Mathf.Abs(_leftBorder.position.x) * 2f;

    private void Start()
    {
        for(int i = 0; i < _roadBlocsCount; i++)
        {
            var block = _roadBlocksPool.Spawn();
            _roadBlocksMover.SetNewRoadBlock(block);
        }
    }

    private void OnEnable()
    {
        _roadBlocksMover.BlockFinishedMovingEvent += BlockFinishedMovingEventHandler;
    }

    private void OnDisable()
    {
        _roadBlocksMover.BlockFinishedMovingEvent -= BlockFinishedMovingEventHandler;
    }

    private void BlockFinishedMovingEventHandler(RoadBlock block)
    {
        block.ReturnToPool();
        block = _roadBlocksPool.Spawn();
        _roadBlocksMover.SetNewRoadBlock(block);
    }
}