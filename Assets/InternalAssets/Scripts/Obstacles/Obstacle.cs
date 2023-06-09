using System;

using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public event Action<Obstacle, int> PositionsRowIsFreeEvent;
    public int OccupiedPositionsRowIndex { get; set; } = -1;

    private void OnDisable()
    {
        PositionsRowIsFreeEvent?.Invoke(this, OccupiedPositionsRowIndex);
        OccupiedPositionsRowIndex = -1;
    }
}