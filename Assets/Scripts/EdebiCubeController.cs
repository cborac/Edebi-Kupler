using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EdebiCubeController : MonoBehaviour
{
    bool isRotating = false;
    new Camera camera;
    new Collider collider;
    public float MobileRotationSpeed = 0.4f;

    public EdebiCube Cube = null;

    public static GameObject current = null;

    void Start()
    {
        camera = Camera.allCameras[0];
        collider = GetComponent<Collider>();
        current = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = camera.ScreenPointToRay(touch.position);

                Physics.Raycast(ray, out RaycastHit hit);

                if (hit.collider == collider)
                {
                    isRotating = true;
                }
            }

            if (touch.phase == TouchPhase.Moved && isRotating)
            {
                transform.Rotate(touch.deltaPosition.y * MobileRotationSpeed,
                                        -touch.deltaPosition.x * MobileRotationSpeed, 0, Space.World);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                isRotating = false;
            }
        }
    }

    internal void Setup(EdebiCube cube)
    {
        Cube = cube;
        foreach (var field in typeof(EdebiCube).GetFields())
        {
            GameObject textObject = transform.Find($"{field.Name.ToUpper()}/Text (TMP)").gameObject;

            textObject.GetComponent<TextMeshProUGUI>().text = ((EdebiCubeSide)field.GetValue(cube)).Question.Replace("-", "<nobr>-</nobr>");
        }
    }
}
