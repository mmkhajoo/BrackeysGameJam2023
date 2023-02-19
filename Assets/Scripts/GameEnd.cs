using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(UIManager.instance.ShowGameEnd());
            col.gameObject.SetActive(false);
        }
    }
}
