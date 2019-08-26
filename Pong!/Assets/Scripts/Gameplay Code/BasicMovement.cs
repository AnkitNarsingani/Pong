using UnityEngine;
using UnityEngine.Advertisements;

public class BasicMovement : MonoBehaviour
{
    public Transform bob;
    private Vector3 targetPos;
    public float speed = 17;

    void Start()
    {
        targetPos = bob.position;
    }

    void Update()
    {
        if (bob.position.x < -1.95f)
            bob.position = new Vector3(-1.95f, bob.position.y, bob.position.z);
        if (bob.position.x > 1.95f)
            bob.position = new Vector3(1.95f, bob.position.y, bob.position.z);

        bob.position = Vector3.Lerp(bob.position, targetPos, speed * Time.deltaTime);
    }

    void OnTouchStay(Vector3 point)
    {

    }

    void OnTouchDown(Vector3 point)
    {
            
    }

    void OnTouchMoved(Vector3 point)
    {
        targetPos = new Vector3(point.x, bob.position.y, 0);
    }

    void OnTouchUp(Vector3 point)
    {

    }
}