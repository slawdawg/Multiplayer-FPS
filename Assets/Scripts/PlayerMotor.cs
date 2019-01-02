using UnityEngine;

[RequireComponent(typeof(playercontroller))]

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 CameraRotation = Vector3.zero;
    private Vector3 thrusterforce = Vector3.zero;

    private Rigidbody rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Gets a movement vector
    public void Move (Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Gets a rotational Vector
    public void rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // Gets a rotational Vector for the camera
    public void CameraRotate(Vector3 _camerarotation)
    {
        CameraRotation = _camerarotation;
    }

    // Get a force vector for our thrusters
    public void ApplyThruster (Vector3 _thrusterForce)
    {
        thrusterforce = _thrusterForce;
    }

    // Run every physics iteration
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
      
    }

    // Perform movement based on velocity variable
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if (thrusterforce != Vector3.zero)
        {
            rb.AddForce(thrusterforce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

   
    //Perform rotation
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
        if (cam != null)
        {
            cam.transform.Rotate(CameraRotation);
        }
    }

}
