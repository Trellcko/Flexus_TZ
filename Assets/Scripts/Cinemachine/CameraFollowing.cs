using UnityEngine;

namespace Trell.Flexus_TZ.Cinemachine
{
	[RequireComponent(typeof(Camera))]
	public class CameraFollowing : MonoBehaviour
	{
		[SerializeField] private Transform _target;

        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _rotation;

        private void OnValidate()
        {
            TryConfigureCamera();
        }

        private void LateUpdate()
        {
            TryConfigureCamera();
        }

        private bool TryConfigureCamera()
        {
            if (_target)
            {
                transform.position = _target.position + _offset;
                transform.rotation = Quaternion.Euler(_rotation);
                return true;
            }
            return false;
        }


    }
}