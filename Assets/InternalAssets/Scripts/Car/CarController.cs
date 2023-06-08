using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private float _sidewaysSpeed = 2f;
    [SerializeField] private float _sideMovePadding = 2f;

    private Vector3 _initialPosition = Vector3.zero;
    private Vector2 _movementLateralLimit = Vector2.zero;


    private void Awake()
    {
        _initialPosition = transform.position;
        _movementLateralLimit = new Vector2(((_level.RoadWidth / 2f) - _sideMovePadding) * -1, (_level.RoadWidth / 2f) - _sideMovePadding);
    }
    
    void Update()
    {
        Vector3 currentPostion = transform.position;
        float newXposition = currentPostion.x;

        bool leftInput = Input.GetKey(KeyCode.A);
        bool rightInput = Input.GetKey(KeyCode.D);

        if((leftInput == true && rightInput == true) == false)
        {
            if (leftInput == true)
            {
                newXposition = currentPostion.x - _sidewaysSpeed * Time.deltaTime;
            }
            else if (rightInput == true)
            {
                newXposition = currentPostion.x + _sidewaysSpeed * Time.deltaTime;
            }
            else
            {
                Vector3 newPosition = Vector3.Lerp(currentPostion, _initialPosition, 0.005f);
                newXposition = newPosition.x;
            }
        }
        
        newXposition = CheckSideBorder(newXposition);
        transform.position = new Vector3(newXposition, currentPostion.y, currentPostion.z);
    }

    private float CheckSideBorder(float xPos)
    {
        if(xPos < _movementLateralLimit.x)
        {
            xPos = _movementLateralLimit.x;
        }
        else if(xPos > _movementLateralLimit.y)
        {
            xPos = _movementLateralLimit.y;
        }
        
        return xPos;
    }
}