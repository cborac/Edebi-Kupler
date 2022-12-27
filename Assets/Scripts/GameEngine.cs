using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public TextAsset questions;
    public static List<EdebiCube> cubeList = null;
    public static int cubeIndex = 0;

    public GameObject CubePrefab;
    public GameObject CardPrefab;

    public GameObject CardSlider;

    // Start is called before the first frame update
    void Start()
    {
        cubeList = new();
        List<EdebiCubeSide> sides = JsonConvert.DeserializeObject<List<EdebiCubeSide>>(questions.text);

        while (sides.Select(x => x.Answer).Distinct().Count() > 5) {
            List<EdebiCubeSide> selectedSides = new();
            var random = new System.Random();

            for (int i = 0; i < 6; i++)
            {
                var side = sides[random.Next(sides.Count())];
                if (selectedSides.Where(x => x.Answer == side.Answer).Count() > 0) {
                    i--;
                    continue;
                }

                selectedSides.Add(side);
                sides.Remove(side);
            }

            EdebiCube cube = new ();

            int j = 0;
            foreach (var property in typeof(EdebiCube).GetFields())
            {
                property.SetValue(cube, selectedSides[j++]);
            }

            cubeList.Add(cube);
        }

        CreateCube(0);

        var k = 0;
        foreach (var cube in cubeList.Shuffle())
        {
            GameObject parent = new GameObject("Card Container");
            parent.transform.parent = CardSlider.transform;

            GameObject card = Instantiate(CardPrefab, parent.transform);
            parent.transform.localPosition = new Vector3(k++ * 3.4f, 0, 0);

            card.GetComponent<CardController>().Setup(cube);
        }
    }

    public GameObject CreateCube(int i)
    {
        GameObject cube = Instantiate(CubePrefab);

        cube.GetComponent<EdebiCubeController>().Setup(cubeList[i]);
        cube.transform.position = new Vector3(-18.2f, 14.69f, -69.258f);
        cube.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

        return cube;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

static class ListExtensions
{
    private static readonly System.Random rng = new();

    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        IList<T> copyList = new List<T>(list);
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (copyList[n], copyList[k]) = (copyList[k], copyList[n]);
        }

        return copyList;
    }
} 

