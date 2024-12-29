using UnityEngine;

public class ColorBook : MonoBehaviour
{
    [SerializeField]
    private Color[] _colors = new Color[]{
        new Color(0, 0, 0),
        new Color(0, 0, 0)
    };

    public Color GetRandomColor()
    {
        return _colors[Random.Range(0, _colors.Length)];
    }
}
