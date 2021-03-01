using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RoadSpawner : MonoBehaviour
{
    private List<GameObject> roadsGO;
    private int currentRoad;
    [SerializeField] GameObject TestRoad;
    private GameObject roadPref;
    [Range(1,40)] public float roadCD;
    private Transform SpawnPoint;


    void Start()
    {
        currentRoad = 0;
        SpawnPoint = transform;
        FillRoadsList(1);
        StartCoroutine(Spawn());
    }

    private void FillRoadsList(int level)
    {
        string levelNum = $"Level_{level}";
        roadsGO = Resources.LoadAll<GameObject>($"{levelNum}/RoadPrefabs").ToList();
        foreach (var item in roadsGO)
            Debug.Log(item.name);
    }

    private void OnEnable()
    {
        
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(roadCD);
        if (currentRoad < roadsGO.Count)
        {
            var newRoad = Instantiate(roadsGO[currentRoad], SpawnPoint);
            newRoad.transform.localPosition = Vector3.zero;
            newRoad.GetComponent<RoadManager>().Move();
            currentRoad++;
            StartCoroutine(Spawn());
            StartCoroutine(Suicide(newRoad));
        }
        else
            Debug.Log("Roads list is empty!");
    }

    private IEnumerator Suicide(GameObject road)
    {
        yield return new WaitForSeconds(3*roadCD);
        Destroy(road);
    }
}
