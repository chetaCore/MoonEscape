using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera: MonoBehaviour
{
    [SerializeField] private float mouseSensitiviti = 300f;

    [SerializeField] private float xRotation = 0f;

    [SerializeField] private GameObject _player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitiviti * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitiviti * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        _player.transform.Rotate(Vector3.up * mouseX);
    }



}
