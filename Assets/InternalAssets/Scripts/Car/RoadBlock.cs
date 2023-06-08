using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    [SerializeField] private Transform _dockingPosition;
    public Vector3 DockingPosition { get => _dockingPosition.position; }
}