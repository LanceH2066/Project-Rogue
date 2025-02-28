using UnityEngine;

public class DiceController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 dragStartPos;
    private Vector2 lastMousePos;
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector2 mouseVelocity;
    private bool diceThrown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        Physics.gravity = new Vector3(0, 0, 9.81f); // Gravity on the Z axis
        rb.useGravity = false;
    }

    void Update()
    {
        
        if(!diceThrown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IsMouseOverDice())
                {
                    isDragging = true;
                    dragStartPos = GetMouseWorldPosition();
                    lastMousePos = dragStartPos;
                    rb.isKinematic = true; // Stop physics while dragging
                }
            }

            if (Input.GetMouseButton(0) && isDragging)
            {
                Vector2 currentMousePos = GetMouseWorldPosition();
                mouseVelocity = (currentMousePos - lastMousePos) / Time.deltaTime;
                lastMousePos = currentMousePos;
                FollowMouse();
            }

            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                isDragging = false;
                rb.isKinematic = false;
                rb.useGravity = true;
                ThrowDice(mouseVelocity);
                diceThrown = true;
            } 
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z); 
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        worldPos.z = transform.position.z; // Lock the Z position
        return worldPos;
    }

    void ThrowDice(Vector3 velocity)
    {
        rb.linearVelocity = new Vector2(velocity.x, velocity.y); 
        rb.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse);
    }

    bool IsMouseOverDice()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform;
        }

        return false;
    }

    void FollowMouse()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            rb.linearVelocity *= 0.9f; // Slightly reduce velocity to simulate energy loss
            rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
        }
    }
}
