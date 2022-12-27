using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour
{
    public EdebiCube Cube;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Setup(EdebiCube cube)
    {
        Cube = cube;
        foreach (var field in typeof(EdebiCube).GetFields())
        {
            GameObject textObject = transform.Find($"{field.Name.ToUpper()}/Canvas/Text (TMP)").gameObject;

            textObject.GetComponent<TextMeshProUGUI>().text = ((EdebiCubeSide)field.GetValue(cube)).Answer.Replace("-", "<nobr>-</nobr>");
        }
    }

    public void Deploy()
    {
        if (GameEngine.cubeList[GameEngine.cubeIndex] == Cube)
        {
            animator.Play("CardDecayAnimation");
            EdebiCubeController.current.GetComponent<Animator>().Play("EdebiCubeDecayAnimation");
        }
        else
            animator.Play("CardMissAnimation");
    }
}
