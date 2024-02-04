using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        InvokeRepeating(nameof(UpdatePFS), 0, 0.5f);
    }

    // Update is called once per frame
    void UpdatePFS()
    {
        text.text = (1 / Time.deltaTime).ToString("0");
    }
}
