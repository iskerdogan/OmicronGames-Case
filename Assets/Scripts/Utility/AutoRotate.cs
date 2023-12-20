using UnityEngine;

namespace Utility
{
	public class AutoRotate : MonoBehaviour
	{
		public Vector3 rotation;

		public Space space = Space.Self;
		void Update()
		{
			transform.Rotate(rotation * Time.deltaTime, space);
		}
	}
}