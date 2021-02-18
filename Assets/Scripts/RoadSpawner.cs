using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoadSpawner : MonoBehaviour
{
    private List<GameObject> roadsGO;
    [SerializeField] GameObject TestRoad;
    private GameObject roadPref;
    [Range(1,5)] public float roadCD = 3f;
    private Transform SpawnPoint;


    void Start()
    {
        SpawnPoint = transform;
        StartCoroutine(Spawn());
    }

    private void OnEnable()
    {
        
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(roadCD);
        var newRoad = Instantiate(TestRoad,SpawnPoint);
        newRoad.transform.localPosition = Vector3.zero;
        newRoad.GetComponent<RoadManager>().Move();
        StartCoroutine(Spawn());
    }
    void Move()
    {
        //testRoad.transform.DOLocalMoveX(testRoad.transform.position.x - 1, 3f);
    }
}
