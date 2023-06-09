using System;
using System.Collections.Generic;

using UnityEngine;

public class RoadBlocksMover : MonoBehaviour
{
    public event Action<RoadBlock> BlockFinishedMovingEvent;

    [SerializeField] private Transform _initialPostionForFirstRoadBlock;
    [SerializeField] private List<RoadBlock> _blocks;
    [SerializeField] private float _blocksMoveSpeed;
    [SerializeField] private Vector3 _blocksMoveDirection;

    private LinkedList<RoadBlock> _blocksList;

    #region UnityCalls

    private void Awake()
    {
        _blocksList = new LinkedList<RoadBlock>();
    }
    
    private void Update()
    {
        if (_blocksList.Count < 1)
        {
            return;
        }

        foreach (var block in _blocksList)
        {
            block.transform.position += _blocksMoveDirection * _blocksMoveSpeed * Time.deltaTime;
        }

        if(_blocksList.First.Value.transform.position.z <= 0f)
        {
            LinkedListNode<RoadBlock> firstBlockNode = _blocksList.First;
            _blocksList.RemoveFirst();

            BlockFinishedMovingEvent?.Invoke(firstBlockNode.Value);
        }
    }

    #endregion

    #region PublicMethods

    public void Setup()
    {

    }

    public void SetNewRoadBlock(RoadBlock block)
    {
        LinkedListNode<RoadBlock> lastBlockNode = _blocksList.Last;
        _blocksList.AddLast(block);

        if (_blocksList.Count > 1)
        {
            block.transform.position = lastBlockNode.Value.DockingPosition;
        }
        else
        {
            block.transform.position = _initialPostionForFirstRoadBlock.position;
        }
    }

    #endregion
}