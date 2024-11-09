using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{

    public float detectionRadius = 5f;    // Distance to detect the player
    public float jumpForce = 5f;          // Force with which the object will jump
    public float timeToJump = 5f;         // Time in seconds player must be in radius
    public float decayRate = 1f;          // How fast the timer decays when the player leaves the radius
    
    private float proximityTimer = 0f;    // Timer to keep track of player's time in radius
    private bool isGrounded = true;       // Check if the object is on the ground

    private Transform player;
    private Rigidbody rb;

    [SerializeField] TextMeshProUGUI countText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player by tag
        rb = GetComponent<Rigidbody>(); 
     }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            // Increase timer while player is within radius
            proximityTimer += Time.deltaTime;

            // Check if timer has reached the required time to jump
            if (proximityTimer >= timeToJump && isGrounded)
            {
                Jump();
            }
        }
        else
        {
            // Decay timer when player is outside the radius
            proximityTimer -= decayRate * Time.deltaTime;
            proximityTimer = Mathf.Max(proximityTimer, 0f); // Clamp to 0
        }

        countText.text = proximityTimer.ToString();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Temporarily disable jumping until grounded again
        proximityTimer = 0f; // Reset timer after jump
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
