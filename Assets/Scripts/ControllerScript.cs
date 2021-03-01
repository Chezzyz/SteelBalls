using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerScript : MonoBehaviour
{
    private int Xdirection;
    private int Ydirection;
    private bool draggingFlag;
    private Vector3 dragOrigin;
    private new Camera camera;
    private new Rigidbody rigidbody;

    [SerializeField] public float jumpForce;
    [SerializeField] public GameObject cameraGO;
    [SerializeField] public GameObject target;
    private GameObject JabkaGO => gameObject;
    private Transform CameraTransform => camera.transform;
    private Transform JabkaTransform => JabkaGO.transform;

    void Start()
    {
        camera = cameraGO.GetComponent<Camera>();
        rigidbody = JabkaGO.GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            draggingFlag = true;
            dragOrigin = camera.ScreenToViewportPoint(Input.mousePosition);
            rigidbody.AddForce(target.transform.localPosition * jumpForce, ForceMode.Impulse);
            rigidbody.freezeRotation = false;
        }

        if (Input.GetMouseButton(0) && draggingFlag)
        {
            Vector3 rotDiff = camera.ScreenToViewportPoint(Input.mousePosition) - dragOrigin;
            JabkaTransform.rotation = new Quaternion(JabkaTransform.rotation.x, JabkaTransform.rotation.y - rotDiff.x, JabkaTransform.rotation.z, 5);
            //if (Math.Abs(rotDiff.x) > 0.45f)
           // {
             //   JabkaTransform.rotation.SetLookRotation(Vector3.forward + new Vector3(;
                //if (CameraTransform.position.x > rightLimit) CameraTransform.position = new Vector3(rightLimit, CameraTransform.position.y, CameraTransform.position.z);
                //if (CameraTransform.position.x < leftLimit) CameraTransform.position = new Vector3(leftLimit, CameraTransform.position.y, CameraTransform.position.z);
           // }
        }
        if (Input.GetMouseButtonUp(0))
        {
           // rigidbody.freezeRotation = true;
            draggingFlag = false;
        }
    }
}
