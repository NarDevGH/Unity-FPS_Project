using UnityEngine;

public class DebugFireEnemy : Firearm
{
    private const float MAX_RAY_DIST = 100f;

    [SerializeField] private Transform _camera;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _damage;

    private void Update()
    {
        FireRate = _fireRate;

        if (Input.GetMouseButtonDown(0))
        {
            if (OnFireCooldown) return;
            Debug.DrawRay(_camera.position, _camera.forward * MAX_RAY_DIST, Color.blue, 2f);
            FireRaycast(_camera.position, _camera.forward, MAX_RAY_DIST, _damage);
        }
    }
}
