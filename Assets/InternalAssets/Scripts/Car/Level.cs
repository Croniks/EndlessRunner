using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;

    public float RoadWidth => Mathf.Abs(_leftBorder.position.x) * 2f;
}