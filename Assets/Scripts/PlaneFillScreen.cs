using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFillScreen : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3((Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height) / 10, 0.1f, Camera.main.orthographicSize * 2.0f / 10);
    }

}
