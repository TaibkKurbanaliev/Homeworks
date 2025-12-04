using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _marker;
    [SerializeField] private float _rayDistance;
    [SerializeField] private Transform _playerView;

    private void Update()
    {
        _playerView.rotation = transform.rotation;
        var pos = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(pos, out RaycastHit hit, _rayDistance))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
            _marker.transform.position = hit.point;
        }
    }
}
