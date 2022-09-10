using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform _pointPrefab;
    [SerializeField, Range(10, 100)] private int _resolution = 10;

    private Transform[] _points;

    private void Awake()
    {
        float step = 2f / _resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;

        _points = new Transform[_resolution];
        
        for (int i = 0; i < _points.Length; i++)
        {
            Transform point = _points[i] = Instantiate(_pointPrefab);

            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            
            point.SetParent(transform, false);
        }
    }

    private void Update()
    {
        float time = 0;
        
        for (int i = 0; i < _points.Length; i++)
        {
            Transform point = _points[i];
            Vector3 position = point.localPosition;
            position.y = FunctionLibrary.MultiWave(position.x, time);
            point.localPosition = position;
        }
    }
}