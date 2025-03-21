using UnityEngine;

public class NoteSpawnerScript : MonoBehaviour
{
    [Header("Note Spawn Settings")]
    [SerializeField, Tooltip("Set time until the next Note spawns.")]
    private float timeUntilNextSpawn;
    [SerializeField, Tooltip("Set the Notes that could be spawned.")]
    private GameObject[] PossibleNotesToSpawn;
    
    private float _timer = 0;
    
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= timeUntilNextSpawn)
        {
            _timer = 0;
            Instantiate(PossibleNotesToSpawn[Random.Range(0, PossibleNotesToSpawn.Length - 1)], transform.position, Quaternion.identity);
        }
    }
}
