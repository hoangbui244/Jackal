using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class RescueHelicopter : MonoBehaviour {
        public float HelicopterSpeed = 1f;
        public Transform Target;
        private Animator m_HeliAnimator;
        private void Update() {
            if (GameManager.Instance.Count() == true) {
                // Xác định hướng di chuyển
                Vector3 direction = Target.position - transform.position;
                direction.Normalize();
                // Xác định góc xoay để nhìn vào target
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                // Di chuyển theo hướng nhìn
                transform.Translate(Vector3.right * HelicopterSpeed * Time.deltaTime);
            }
        }
    }
}