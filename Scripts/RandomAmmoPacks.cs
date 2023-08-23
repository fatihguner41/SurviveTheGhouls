using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RandomAmmoPacks : MonoBehaviour
{

    public GameObject ammoPackPrefab;
    public GameObject ghoulPrefab;
    public Terrain terrain;

    public int ghoulSpawnTime;
    public int ghoulSpawnAmount;
    public int waveBonus;

    public int ammoPackSpawnTime;
    public int ammoPackSpawnAmount;
    //PlayerMovement player;
    
    List<GameObject> ammoPackages= new List<GameObject>();

    int waveNumber=0;

    public GameObject waveCanvas;
    TMP_Text waveText;

    // Start is called before the first frame update
    void Start()
    {
        waveText = waveCanvas.GetComponent<TextMeshProUGUI>();

        InvokeRepeating("CreateRandomAmmoPackagesRepeatedly", 0f, ammoPackSpawnTime);
        InvokeRepeating("CreateRandomGhoulRepeatedly", 0f, ghoulSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateRandomAmmoPackagesRepeatedly()
    {
        CreateRandomAmmoPackages(ammoPackSpawnAmount); // Her çaðrýda sadece 100 mermi paketi oluþturulacaksa
    }

    void CreateRandomGhoulRepeatedly()
    {
        CreateRandomGhoul(ghoulSpawnAmount);
        ghoulSpawnAmount += waveBonus;
        waveNumber++;

        waveText.text = waveText.text + waveNumber;
        waveCanvas.SetActive(true);
        Invoke("WaveCanvasDeactivater", 3f);
    }

    void CreateRandomAmmoPackages(int count)
    {
        foreach (GameObject ammoPackage in ammoPackages)
        {
            Destroy(ammoPackage);
        }
        ammoPackages.Clear();

        TerrainData terrainData = terrain.terrainData;
        

        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(0f, terrainData.size.x);
            float randomZ = Random.Range(0f, terrainData.size.z);
            float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0f, randomZ));

            Vector3 randomPosition = new Vector3(randomX, terrainHeight, randomZ);
            GameObject newBulletPackage = Instantiate(ammoPackPrefab, randomPosition, Quaternion.identity);
            ammoPackages.Add(newBulletPackage.gameObject);

            
        }
    }

    void CreateRandomGhoul(int count)
    {
        

        TerrainData terrainData = terrain.terrainData;


        for (int i = 0; i < count; i++)
        {
            float randomX = Random.Range(0f, terrainData.size.x);
            float randomZ = Random.Range(0f, terrainData.size.z);
            float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0f, randomZ));

            Vector3 randomPosition = new Vector3(randomX, terrainHeight, randomZ);
            GameObject ghoul = Instantiate(ghoulPrefab, randomPosition, Quaternion.identity);
            

        }
    }

    void WaveCanvasDeactivater()
    {
        waveCanvas.SetActive(false);
        waveText.text = "wave ";
    }
}
