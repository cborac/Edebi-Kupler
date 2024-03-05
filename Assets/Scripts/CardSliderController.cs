using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSliderController : MonoBehaviour
{
    new Camera camera;

    bool isSliding = false;
    public bool isDeploying = false;

    public static CardSliderController current;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        camera = Camera.allCameras[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            

            if (touch.phase == TouchPhase.Began)
            {
                isSliding = touch.position.y < Screen.height * .4;
            }

            if (touch.phase == TouchPhase.Moved && isSliding && !isDeploying)
            {
                if (touch.deltaPosition.y > 110)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    Physics.Raycast(ray, out RaycastHit hit);

                    if (GetComponentsInChildren<Collider>().Contains(hit.collider))
                    {
                        isDeploying = true;
                        (hit.collider.gameObject.GetComponent<CardController>() ?? hit.collider.gameObject.GetComponentInParent<CardController>()).Deploy();
                    }
                } else transform.position = new Vector3(touch.deltaPosition.x * .003f, 0, 0) + transform.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                isSliding = false;
            }
        }
    }
}
