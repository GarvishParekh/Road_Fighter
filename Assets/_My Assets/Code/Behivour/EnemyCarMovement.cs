using UnityEngine;

public class EnemyCarMovement : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;
    [SerializeField] private Transform EndPoint;
    [SerializeField] private float movementSpeed = 1;

    bool isMoving = false;

    float complitionRate = 0;
    float degree = 0;

    private void Awake()
    {
        movementSpeed = Random.Range(0.1f, 0.2f);
    }

    private void OnEnable()
    {
        GameStatus.GameOverAction += OnGameOver;
    }

    private void OnDisable()
    {
        GameStatus.GameOverAction -= OnGameOver;
    }

    private void Update()
    {
        Animation();
    }

    private void Animation()
    {
        if (!isMoving)
            return;

        degree += Time.deltaTime * movementSpeed;
        complitionRate = Mathf.Sin(degree);
        complitionRate = Mathf.Abs(complitionRate);
        
        transform.localPosition = Vector3.Lerp(startingPoint.localPosition, EndPoint.localPosition, complitionRate);
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    
    public void StopMoving()
    {
        isMoving = false;
        transform.localPosition = Vector3.zero;
    }

    public void OnGameOver()
    {
        isMoving = false;
    }
}
