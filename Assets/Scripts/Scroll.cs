using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {
    RectTransform rect;
    // Use this for initialization
    void Start() {
        rect = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update() {
        rect.transform.position += rect.transform.up * 0.5f;


        if (Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel(SceneName.prototype01_test);
        }
    }
    
}
