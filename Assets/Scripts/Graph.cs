using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform _pointPrefab;
    [SerializeField, Range(10, 100)] private int _resolution = 10;
    [SerializeField] private FunctionLibrary.FunctionName _function;

    private Transform[] _points;

    private void Awake()
    {
        float step = 2f / _resolution;
        var position = Vector3.zero;
        var scale = Vector3.one * step;

        _points = new Transform[_resolution * _resolution];

        for (int i = 0, x = 0, z = 0; i < _points.Length; i++, x++)
        {
            if (x == _resolution)
            {
                x = 0;
                z += 1;
            }

            Transform point = _points[i] = Instantiate(_pointPrefab);

            position.x = (x + 0.5f) * step - 1f;
            position.z = (z + 0.5f) * step - 1f;
            
            point.localPosition = position;
            point.localScale = scale;

            point.SetParent(transform, false);
        }
    }

    private void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(_function);

        float time = Time.time;

        foreach (var point in _points)
        {
            Vector3 position = point.localPosition;
            position.y = f(position.x, position.z, time);
            point.localPosition = position;
        }
    }
}