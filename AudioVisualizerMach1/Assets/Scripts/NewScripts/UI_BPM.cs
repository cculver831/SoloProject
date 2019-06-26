using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BPM : MonoBehaviour
{
    public BPMSequencer _bpm;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "BPM: " + _bpm._BPM.ToString();
    }
}
