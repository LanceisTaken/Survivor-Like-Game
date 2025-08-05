using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
            prop.layer = LayerMask.NameToLayer("Terrain");

            // Get ALL Sprite Renderers in the prop (including children)
            SpriteRenderer[] allSpriteRenderers = prop.GetComponentsInChildren<SpriteRenderer>();

            if (allSpriteRenderers.Length > 0)
            {
                foreach (SpriteRenderer sr in allSpriteRenderers)
                {
                    sr.sortingLayerName = "Props";
                    sr.sortingOrder = 0; // You can also set a specific order
                    Debug.Log($"Set sorting layer for {sr.gameObject.name} to Props");
                }
            }
            else
            {
                Debug.LogWarning($"Spawned prop '{prop.name}' does not have any Sprite Renderers!");
            }
        }
    }
}
