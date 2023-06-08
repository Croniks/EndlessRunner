using System.Collections.Generic;

using UnityEngine;

public class RoadBlocksMover : MonoBehaviour
{
    [SerializeField] private Transform _blocksParent;
    [SerializeField] private List<RoadBlock> _blocks;
    [SerializeField] private float _blocksMoveSpeed;
    [SerializeField] private Vector3 _blocksMoveDirection;

    private LinkedList<RoadBlock> _blocksList;

    private void Start()
    {
        _blocksList = new LinkedList<RoadBlock>();
        _blocks.ForEach(b => _blocksList.AddLast(b));
    }

    private void Update()
    {
        foreach(var block in _blocksList)
        {
            block.transform.position += _blocksMoveDirection * _blocksMoveSpeed * Time.deltaTime;
        }

        if(_blocksList.First.Value.transform.position.z <= 0f)
        {
            LinkedListNode<RoadBlock> firstBlockNode = _blocksList.First;
            _blocksList.RemoveFirst();

            LinkedListNode<RoadBlock> lastBlockNode = _blocksList.Last;
            _blocksList.AddLast(firstBlockNode);

            firstBlockNode.Value.transform.position = lastBlockNode.Value.DockingPosition;
        }
    }
}