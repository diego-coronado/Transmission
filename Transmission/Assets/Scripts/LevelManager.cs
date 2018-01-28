using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject mainCamera;
    public Transform player1;
    public Transform player2;

    private void Awake()
    {
        float level = PlayerPrefs.GetInt("Level", 0);
        Debug.Log(level);
        mainCamera.transform.position = new Vector3(0, 10 * level, -10);
        player1.position = new Vector3(4, 10 * level -4, 0);
        player2.position = new Vector3(-4, 10 * level -4, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
    }
}
