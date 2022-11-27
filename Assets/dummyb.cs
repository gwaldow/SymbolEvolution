using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyb : MonoBehaviour
{
    private List<LineRenderer> _linesRenderer = new List<LineRenderer>();
    private LineRenderer _currentLineRenderer;
    private int _vertexCount = 0;
    [SerializeField] Transform _linePrefab;

    void Start()
    {
        float[] randFloatArr = new float[75];
        for (int i = 0; i < 75; i++)
        {
            randFloatArr[i] = Random.Range(-1f, 1f);

        }
        Debug.Log(string.Join(", ", randFloatArr));
        for (int i = 0; i < randFloatArr.Length; i = i + 3)
        {
            if (i == 0)
            {
                Transform tmpGesture = Instantiate(_linePrefab, transform.position, transform.rotation, transform) as Transform;
                _currentLineRenderer = tmpGesture.GetComponent<LineRenderer>();
                _linesRenderer.Add(_currentLineRenderer);
                _vertexCount = 0;

                _currentLineRenderer.SetVertexCount(++_vertexCount);
                _currentLineRenderer.SetPosition(_vertexCount - 1, transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(randFloatArr[i] * Screen.width + (Screen.width / 2), randFloatArr[i + 1] * Screen.height + (Screen.height / 2), Vector3.Distance(transform.position, transform.parent.Find("FocalPoint").position))));

            }
            else if (randFloatArr[i + 2] > 0)
            {
                if (randFloatArr[i - 1] <= 0)
                {
                    Transform tmpGesture = Instantiate(_linePrefab, transform.position, transform.rotation, transform) as Transform;
                    _currentLineRenderer = tmpGesture.GetComponent<LineRenderer>();
                    _linesRenderer.Add(_currentLineRenderer);
                    _vertexCount = 0;
                }
                _currentLineRenderer.SetVertexCount(++_vertexCount);
                _currentLineRenderer.SetPosition(_vertexCount - 1, transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(randFloatArr[i] * Screen.width + (Screen.width / 2), randFloatArr[i + 1] * Screen.height + (Screen.height / 2), Vector3.Distance(transform.position, transform.parent.Find("FocalPoint").position))));
            }
        }
        Debug.Log("# of lines : " + _linesRenderer.Count);
        Debug.Log("points in line 1 : " + _currentLineRenderer.positionCount);
    }
    public float stretch(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
