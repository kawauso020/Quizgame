using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̃N���X�̐����������āI�I�I
/// </summary>
public class ProductSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // ��������Prefab
    public float spawnInterval = 2f; // �����Ԋu
    public float spawnY = 2.06f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        if (prefabs.Length == 0) return; // �z�񂪋�Ȃ珈�����Ȃ�

        Vector3 spawnPos = new Vector3(-11f, spawnY, 0f); // ���[����o�� //-11�̓}�W�b�N�i���o�[�ł��B�ϐ������Ă��������B

        // �����_����Prefab��I��
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        // ����
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
