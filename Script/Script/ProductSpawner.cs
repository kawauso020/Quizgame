using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // 生成するPrefab
    public float spawnInterval = 2f; // 生成間隔
    public float spawnY = 2.06f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        if (prefabs.Length == 0) return; // 配列が空なら処理しない

        Vector3 spawnPos = new Vector3(-11f, spawnY, 0f); // 左端から出現

        // ランダムにPrefabを選択
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        // 生成
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
