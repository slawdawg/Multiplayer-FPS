﻿using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]

public class playercontroller : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float LookSensitivity = 3f;
    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring Settings")]
    [SerializeField]
    private JointDriveMode jointmode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;


    private PlayerMotor motor;
    private ConfigurableJoint joint;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        // Calculate movement velocity as a 3d vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov; 
        Vector3 _movVertical = transform.forward * _zMov;
        

        //Final Movement Vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Apply Movement
        motor.Move(_velocity);

        //Calculate rotation as a 3d vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * LookSensitivity;

        //Apply rotation to player
        motor.rotate(_rotation);

        //Calculate camera rotation as a 3d vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _camerarotationX = _xRot * LookSensitivity;

        //Apply rotation to player
        motor.CameraRotate(-_camerarotationX);

        //Calc the thrusterforce based on player input
        Vector3 _thrusterForce = Vector3.zero;

        
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }
        //Apply thruster force
        motor.ApplyThruster(_thrusterForce);

    }

    private void SetJointSettings (float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            mode = jointmode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
