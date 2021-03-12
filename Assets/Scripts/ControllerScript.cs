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
    private Vector3 rotDiff = Vector3.zero;
    private float angle => (15f + ((rotDiff.y - rotRangeMin) / (rotRangeMax - rotRangeMin)) * 65f) * Mathf.PI/180f;
    [Range(-1,0)] public float rotRangeMin = -0.15f; 
    [Range(-1,0)] public float rotRangeMax = -0.45f; 
    [SerializeField] public new Rigidbody rigidbody;
    [SerializeField] public float sensetive;
    [SerializeField] public float jumpForce;
    [SerializeField] public float radius = 4f;
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
        }

        if (Input.GetMouseButton(0) && draggingFlag)
        {
            print(angle);
            rotDiff = camera.ScreenToViewportPoint(Input.mousePosition) - dragOrigin;
            transform.localEulerAngles = new Vector3(0,startRot.y - rotDiff.x*sensetive,0);
        }

        if (rotDiff.y < rotRangeMin)
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
            if (rotDiff.y < rotRangeMin)
            { 
                print(target.transform.localPosition);
                target.transform.localPosition = new Vector3(0, Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
                jumpForce = Mathf.Abs(rotDiff.y * 10 / Mathf.Tan(angle));
                rigidbody.AddForce((target.transform.position - transform.position) * jumpForce, ForceMode.Impulse);
                animator.SetBool("JumpPrep", false);
            }
            draggingFlag = false;
            rotDiff = Vector3.zero;
        }
    }
}
