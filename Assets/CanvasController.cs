using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] public RuntimeData _runtimeData;
    [SerializeField] public TMPro.TextMeshProUGUI idLabel;
    void Update()
    {
        idLabel.text = "" + _runtimeData.object_id;
    }
}
