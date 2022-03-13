using UnityEngine;

namespace WeaponScripts 
{
    public class WeaponSway : MonoBehaviour
    {
            [SerializeField] private Vector2 _swayMultiplier; // x = 1.5 , y = 1 
            [SerializeField] private float _smooth = 3f;

            private void Update()
            {
                // Get Mouse Input
                float mouseX = PlayerInput.LookDelta_X * _swayMultiplier.x;
                float mouseY = PlayerInput.LookDelta_Y * _swayMultiplier.y;

                // Calculate Target Rot.
                Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
                Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

                Quaternion targetRotaation = rotationX * rotationY;

                // Rotate
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotaation, _smooth * Time.deltaTime);
            }
    }

}