using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject loading;

    public Transform Player;
    public int NumerofZombies;
    public float SpawnDelay =  1.0f;
    public List<Zombie> ZombiePrefabs = new List<Zombie>();
    public SpawnMethod ZombieSpawnMethod = SpawnMethod.RoundRobin;

    private NavMeshTriangulation Triangulation;

    private Dictionary<int, ObjectPool> ZombieObjectPool = new Dictionary<int, ObjectPool>();


    private void Awake() {
       for(int i = 0; i < ZombiePrefabs.Count; i++){
           ZombieObjectPool.Add(i, ObjectPool.CreateInstance(ZombiePrefabs[i], NumerofZombies));
       }
    }
    


    // Start is called before the first frame update
    private void Start()
    {
        
        Triangulation = NavMesh.CalculateTriangulation();
        StartCoroutine(SpawnZombies());

    }

    private IEnumerator SpawnZombies()
    {
        WaitForSeconds Wait = new WaitForSeconds(SpawnDelay);

        int SpawnedZombies = 0;

        while (SpawnedZombies < NumerofZombies)
        {
            if(ZombieSpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnZombiesRoundRobin(SpawnedZombies);
            }
            else if(ZombieSpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomZombie();
            }
            SpawnedZombies++;

            yield return Wait;
        }

        loading.SetActive(false);
    }

    private void SpawnZombiesRoundRobin(int SpawnedZombies)
    {
        int ZombieIndex = SpawnedZombies % ZombiePrefabs.Count;

        DoSpawnZombie(ZombieIndex);
    }

    private void SpawnRandomZombie()
    {
        int ZombieIndex = Random.Range(0, ZombiePrefabs.Count);

    }

    private void DoSpawnZombie(int ZombieIndex)
    {
        PoolableObject ZombieObject = ZombieObjectPool[ZombieIndex].GetObject();

        if(ZombieObject != null){
            Zombie Zombie = ZombieObject.GetComponent<Zombie>();


            int VertexIndex = Random.Range(0, Triangulation.vertices.Length);

            NavMeshHit Hit;

            if(NavMesh.SamplePosition(Triangulation.vertices[VertexIndex], out Hit, 2f, NavMesh.AllAreas)){
                Zombie.Agent.Warp(Hit.position);
                
                Zombie.Agent.enabled = true;
            }
            else{
                Debug.LogError($"Unable to spawn zombie {ZombieIndex}");
            }
        }
        else{
            Debug.LogError($"Unable to spawn zombie {ZombieIndex}");
        }

    }

    public enum SpawnMethod
    {
        RoundRobin,
        Random
    }
}
