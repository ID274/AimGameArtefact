using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.VisualScripting;

public class RespawnObject : MonoBehaviour
{
    [ReadOnly] public bool applicationQuitting = false;

    [SerializeField] private float delay = 5f;

    private Vector3 originalPosition;
    private Vector3 farAwayPosition = new Vector3(0, -1000, 0);

    private void Awake()
    {
        originalPosition = transform.position;
    }

    private void OnApplicationQuit()
    {
        applicationQuitting = true;
    }

    public void StartRespawn()
    {

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        transform.position = farAwayPosition;
        yield return new WaitForSeconds(delay);
        transform.position = originalPosition;
    }
}
