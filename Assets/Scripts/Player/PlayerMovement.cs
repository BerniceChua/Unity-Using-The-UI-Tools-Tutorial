using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6.0f;

    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private int floorMask;
    private float cameraRayLength = 100.0f;

    private void Awake() {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vertic = Input.GetAxisRaw("Vertical");

        Move(horiz, vertic);
        Turning();
        Animating(horiz, vertic);
    }

    private void Move(float hori, float verti) {
        movement.Set(hori, 0.0f, verti);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turning() {
        // does the Raycast
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // RaycastHit gets the information (aka the OUTput) back from the Raycast of cameraRay; the RaycastHit Variable
        RaycastHit floorHit;

        // This does the actual casting of the Raycast
        if (Physics.Raycast(cameraRay, out floorHit, cameraRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0.0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    private void Animating(float hori, float verti) {
        bool walking = hori != 0.0f || verti != 0.0f;
        anim.SetBool("IsWalking", walking);
    }

}
