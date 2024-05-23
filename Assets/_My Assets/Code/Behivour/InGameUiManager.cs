using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public enum NearMissSide
{
    LEFT,
    RIGHT
}

public class InGameUiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinCounTxt;
    [SerializeField] private List<GameObject> nearMissCanvasListRight = new List<GameObject>();
    [SerializeField] private List<GameObject> nearMissCanvasListLeft = new List<GameObject>();
    [SerializeField] private List<GameObject> onScreenNearMiss = new List<GameObject>();

    int canvasSpawnCount;

    private void OnEnable()
    {
        NearMissTrigger.NearMiss += OnNearMiss;
    }

    private void OnDisable()
    {
        NearMissTrigger.NearMiss -= OnNearMiss;
    }

    private void OnNearMiss(NearMissSide nearMissSide)
    {
        switch (nearMissSide)
        {
            case NearMissSide.LEFT:
                //SpawnNearMissCanvas(nearMissCanvasListLeft[0], nearMissCanvasListLeft);
                OnScreenNearMiss();
                break;
            case NearMissSide.RIGHT:
                //SpawnNearMissCanvas(nearMissCanvasListRight[0], nearMissCanvasListRight);
                OnScreenNearMiss();
                break;
        }
    }

    private void SpawnNearMissCanvas(GameObject canvasToSpawn, List<GameObject> listToRefleftIn)
    {
        canvasToSpawn.transform.SetSiblingIndex(nearMissCanvasListRight.Count); 
        canvasToSpawn.SetActive(true);

        GameObject spawnedCanvas = canvasToSpawn;

        listToRefleftIn.Remove(spawnedCanvas);
        listToRefleftIn.Add(spawnedCanvas);
    }

    private void OnScreenNearMiss()
    {
        onScreenNearMiss[0].transform.SetSiblingIndex(nearMissCanvasListRight.Count);
        onScreenNearMiss[0].SetActive(true);

        GameObject spawnedCanvas = onScreenNearMiss[0];

        onScreenNearMiss.Remove(spawnedCanvas);
        onScreenNearMiss.Add(spawnedCanvas);
    }

    public void UpdateCoinsCountTxt(int _coinsToAdd)
    {
        coinCounTxt.text = _coinsToAdd.ToString("#,##0", CultureInfo.InvariantCulture);
    }
}
