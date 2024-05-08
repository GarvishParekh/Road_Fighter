using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class InGameUiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinCounTxt;
    [SerializeField] private List<GameObject> nearMissCanvasList = new List<GameObject>();

    int canvasSpawnCount;

    private void OnEnable()
    {
        NearMissTrigger.NearMiss += OnNearMiss;
    }

    private void OnDisable()
    {
        NearMissTrigger.NearMiss -= OnNearMiss;
    }

    private void OnNearMiss()
    {
        SpawnNearMissCanvas(nearMissCanvasList[0]);
    }

    private void SpawnNearMissCanvas(GameObject canvasToSpawn)
    {
        canvasToSpawn.transform.SetSiblingIndex(nearMissCanvasList.Count); 
        canvasToSpawn.SetActive(true);

        GameObject spawnedCanvas = canvasToSpawn;

        nearMissCanvasList.Remove(spawnedCanvas);
        nearMissCanvasList.Add(spawnedCanvas);
    }

    public void UpdateCoinsCountTxt(int _coinsToAdd)
    {
        coinCounTxt.text = _coinsToAdd.ToString("#,##0", CultureInfo.InvariantCulture);
    }
}
