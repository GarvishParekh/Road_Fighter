using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    int myIndex = 0;
    private float sinTheta = 0;
    [SerializeField] private Transform coinTransform;


    [Space]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float degree = 0;
    [SerializeField] private float waveHeight = 0;
    [SerializeField] private float waveSpeed = 1;

    private void Awake()
    {
        myIndex = transform.GetSiblingIndex();
        degree = myIndex;
    }

    // Update is called once per frame
    void Update()
    {
        coinTransform.Rotate(0, rotationSpeed, 0);
        degree += Time.deltaTime * waveSpeed;
        sinTheta = Mathf.Sin(degree) * waveHeight;
        coinTransform.localPosition = new Vector3 (0,sinTheta,0);
    }
}
