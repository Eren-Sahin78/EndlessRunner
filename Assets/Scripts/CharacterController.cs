using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] float speed = 0.4f;
    [SerializeField] private float lateralSmoothSpeed = 10f;

    private float[] xPosition = { 0f, 0.368f, 0, 736f };

    private int currentXpositionIndex = 0;
    Vector3 targetPosition;

    public bool isAlive = true;

    private void Start()
    {
        targetPosition = transform.position;

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && currentXpositionIndex > 0)
        {
            currentXpositionIndex--;
            UpdateLateralPosition();
            
        }
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 forwardMove = Vector3.forward *speed *Time.fixedDeltaTime;

            Vector3 currentPosition = rb.position;
            Vector3 lateralMove = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * lateralSmoothSpeed);

            Vector3 combineMove = new Vector3(lateralMove.x, transform.position.y, rb.position.z) + forwardMove;
            rb.MovePosition(combineMove);
        }
    }
    void UpdateLateralPosition()
    {
        targetPosition = new Vector3(xPosition[currentXpositionIndex],transform.position.y,transform.position.z);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            isAlive = false;

        }
    }
}