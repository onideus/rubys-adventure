using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;

    public GameObject dialogBox;

    private float _displayTimer;
    
    // Start is called before the first frame update
    private void Start()
    {
        dialogBox.SetActive(false);
        _displayTimer = -1.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_displayTimer < 0) return;
        _displayTimer -= Time.deltaTime;
        if (_displayTimer >= 0) return;
        dialogBox.SetActive(false);
    }

    public void DisplayDialog()
    {
        _displayTimer = displayTime;
        dialogBox.SetActive(true);
    }
}
