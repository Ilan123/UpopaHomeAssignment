using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLogic : MonoBehaviour
{
    [SerializeField] GameObject firePoint = null;
    [SerializeField] GameObject bulletPref = null;
    [SerializeField] float shootingCooldown = 2f;
    [SerializeField] float bulletMoveSpeed = 8f;
    [SerializeField] float shipMoveSpeed = 3f;
    [SerializeField] float shipRotationSpeed = 10;
    [SerializeField] float rotationDragFactor = 10;
    private float _rotationCurrentSpeed = 0;
    private float _prevTime;
    Mesh _objectMesh;
    // Start is called before the first frame update
    void Start()
    {
        _objectMesh = GetComponent<MeshFilter>().mesh;
        _prevTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.up * shipMoveSpeed * Time.deltaTime;
        transform.RotateAround(transform.TransformPoint(_objectMesh.bounds.center), Vector3.forward, _rotationCurrentSpeed * Time.deltaTime);
        ShipRotationCtrl();

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void ShipRotationCtrl()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            _rotationCurrentSpeed += shipRotationSpeed * Time.deltaTime;
            if (_rotationCurrentSpeed > shipRotationSpeed)
            {
                _rotationCurrentSpeed = shipRotationSpeed;
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rotationCurrentSpeed -= shipRotationSpeed * Time.deltaTime;
            if (_rotationCurrentSpeed < -shipRotationSpeed)
            {
                _rotationCurrentSpeed = -shipRotationSpeed;
            }
        }
        // Creating the rotation drag effect
        else if (_rotationCurrentSpeed != 0)
        {
            if (_rotationCurrentSpeed > 0)
            {
                _rotationCurrentSpeed -= rotationDragFactor * Time.deltaTime;
                if (_rotationCurrentSpeed < 0)
                {
                    _rotationCurrentSpeed = 0;
                }
            }
            else
            {
                _rotationCurrentSpeed += rotationDragFactor * Time.deltaTime;
                if (_rotationCurrentSpeed > 0)
                {
                    _rotationCurrentSpeed = 0;
                }
            }
        }
    }

    public void Shoot()
    {
        if (Time.time - _prevTime > shootingCooldown)
        {
            GameObject bullet = SpawnsPoolManager.instance.SpawnableObject(bulletPref);
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<Rigidbody>().velocity = bulletMoveSpeed * transform.up;
            _prevTime = Time.time;
        }
    }
}
