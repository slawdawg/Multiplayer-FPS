using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class playercontroller : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float LookSensitivity = 3f;


    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // Calculate movement velocity as a 3d vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov; 
        Vector3 _movVertical = transform.forward * _zMov;
        

        //Final Movement Vector
        Vector3 _volicity = (_movHorizontal + _movVertical).normalized * speed;

        //Apply Movement
        motor.Move(_volicity);

        //Calculate rotation as a 3d vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * LookSensitivity;

        //Apply rotation to player
        motor.rotate(_rotation);

        //Calculate camera rotation as a 3d vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _camerarotation = new Vector3(_xRot, 0f, 0f) * LookSensitivity;

        //Apply rotation to player
        motor.CameraRotate(-_camerarotation);
    }
}
