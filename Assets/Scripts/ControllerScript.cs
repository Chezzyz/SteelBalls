using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerScript : MonoBehaviour
{
    private bool draggingFlag;
    private Vector3 dragOrigin;
    private new Camera camera;
    private Vector3 startRot;
    private Vector3 rotDiff;

    [SerializeField] public new Rigidbody rigidbody;
    [SerializeField] public float sensetive;
    [SerializeField] public float jumpForce;
    [SerializeField] public GameObject cameraGO;
    [SerializeField] public GameObject target;
    [SerializeField] public Animator animator;
    private GameObject JabkaGO => gameObject;
    private Transform CameraTransform => camera.transform;
    private Transform JabkaTransform => JabkaGO.transform;

    void Start()
    {
        camera = cameraGO.GetComponent<Camera>();
        rotDiff = Vector3.zero; 
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            draggingFlag = true;
            dragOrigin = camera.ScreenToViewportPoint(Input.mousePosition);
            startRot = transform.localEulerAngles;
            rigidbody.freezeRotation = false;
        }

        if (Input.GetMouseButton(0) && draggingFlag)
        {
            rotDiff = camera.ScreenToViewportPoint(Input.mousePosition) - dragOrigin;
            transform.localEulerAngles = new Vector3(0,startRot.y - rotDiff.x*sensetive,0);
        }

        if (rotDiff.y < -0.15f)
        {
            animator.SetBool("JumpPrep", true);
            StartCoroutine(UIManager.singleton?.ChangeVignetteState(0, true));
        }
        else
        {
            animator.SetBool("JumpPrep", false);
            StartCoroutine(UIManager.singleton?.ChangeVignetteState(0, false));
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (rotDiff.y < -0.15f)
            {
                rigidbody.AddForce((target.transform.position - transform.position) * jumpForce, ForceMode.Impulse);
                animator.SetBool("JumpPrep", false);
            }
            draggingFlag = false;
            rigidbody.freezeRotation = true;
            rotDiff = Vector3.zero;
        }
    }
}
