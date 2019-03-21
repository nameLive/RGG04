using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    GameObject playerCharacter;

    int arraySize;

    [SerializeField]
    [Tooltip("Place GameObjects to act as spawn locations for debris")]
    public Transform[] DebrisSpawnLocation;

    [SerializeField]
    [Tooltip("Place what prefab that should be spawned(Default:Debris)")]
    public GameObject[] DebrisPrefab;

    [SerializeField]
    [Tooltip("No need to assign anything here, used as placeholder for the spawnable prefab")]
    public GameObject[] DebrisPrefabClone;

   
    void Start()
    {
        playerCharacter = GameObject.Find("PlayerCharacter");
        arraySize = DebrisPrefab.Length;
    }

    void SpawnDebris()
    {
        for(int i = 0; i < arraySize; i++)
        {
            DebrisPrefabClone[i] = Instantiate(DebrisPrefab[i], DebrisSpawnLocation[i].transform) as GameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerCharacter)
        {
            SpawnDebris();
        }
    }
}
