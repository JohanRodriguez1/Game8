using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header ("Velocity Control")]
    [SerializeField] private float _turnSpeed = 25.0f;
    [SerializeField] private float _horsePower = 5000.0f;
    [SerializeField] private GameObject _massCenter;
    [SerializeField] private TextMeshProUGUI _speedometer;
    [SerializeField] private TextMeshProUGUI _gearCounter;
    [SerializeField] private List<WheelCollider> _wheelsList;

    private Rigidbody RbPlayer;

    private float horizontalInput;
    private float forwardInput;
    private int _speed;
    private int Gear;
    private int WheelsOnGround;

    // Start is called before the first frame update
    void Start()
    {
        RbPlayer = GetComponent<Rigidbody>();
        RbPlayer.centerOfMass = _massCenter.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Inicializar Ejes
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Mover la furgo hacia delante
        //transform.Translate(Vector3.forward * Time.deltaTime * _speed * forwardInput);

        if (IsInGround())
        {
            RbPlayer.AddRelativeForce(Vector3.forward * forwardInput * _horsePower);

            _speed = (int)(RbPlayer.velocity.magnitude * 3600 / 1000);
            _speedometer.SetText("Speed: " + _speed + "Km/h");

            if (_speed % 30 == 0)
            {
                _gearCounter.SetText("Gear: " + Gear);
                Gear = _speed / 30 + 1;
            }

            // Rotar la furgo hacia los lados
            transform.Rotate(Vector3.up * Time.deltaTime * _turnSpeed * horizontalInput);
        }       
    }

    private bool IsInGround()
    {
        WheelsOnGround = 0;

        foreach (WheelCollider wheel in _wheelsList)
        {
            if (wheel.isGrounded)
            {
                WheelsOnGround++;
            }
        }
        return (WheelsOnGround == 4);
    }
}
