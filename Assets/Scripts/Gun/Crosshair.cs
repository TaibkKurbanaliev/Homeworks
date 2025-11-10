using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private RectTransform _canvasTransform;
    private PlayerInputActions _inputActions;
    private RectTransform _transform;

    public void Init(PlayerInputActions inputActions)
    {
        _canvasTransform = _canvas.GetComponent<RectTransform>();
        _inputActions = inputActions;
        _transform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        var mousePos = _inputActions.UI.Point.ReadValue<Vector2>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasTransform,mousePos, _canvas.worldCamera, out Vector2 pos);

        Vector2 halfSize = (_canvasTransform.rect.size) / 2f;
        pos.x = Mathf.Clamp(pos.x, -halfSize.x, halfSize.x);
        pos.y = Mathf.Clamp(pos.y, -halfSize.y, halfSize.y);

        _transform.anchoredPosition = pos;
    }

    public Vector3 WorldPosition()
    {
        var ray = Camera.main.ScreenPointToRay(_transform.position);
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, 10));

        if (plane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        return Vector3.zero;
    }
}
