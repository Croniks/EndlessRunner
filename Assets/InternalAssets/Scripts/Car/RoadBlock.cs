using Spawners;

using UnityEngine;

public class RoadBlock : PoolObject
{
    [SerializeField] private Transform _dockingPosition;
    public Vector3 DockingPosition { get => _dockingPosition.position; }
}