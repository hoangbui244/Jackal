using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace io.lockedroom.Games.Jackal {
    public class AlliesRescue : MonoBehaviour {
        /// <summary>
        /// Tốc độ di chuyển của đồng minh
        /// </summary>
        public float force;
        private Rigidbody2D m_Rb;
        /// <summary>
        /// Animator của đồng minh
        /// </summary>
        private Animator m_AlliesAnimator;
        private void Awake() {
            m_Rb = GetComponent<Rigidbody2D>();
            m_AlliesAnimator = GetComponent<Animator>();
        }
        private void Start() {
            StartCoroutine(MoveLeftForDuration(1.8f));
        }
        /// <summary>
        /// Hàm đếm thời gian di chuyển của đồng minh
        /// </summary>
        private IEnumerator MoveLeftForDuration(float duration) {
            // Set biến trong animator sang true
            m_AlliesAnimator.SetBool("ERH", true);
            float elapsedTime = 0f;
            // Di chuyển sang bên phải
            Vector2 initialVelocity = new Vector2(force, m_Rb.velocity.y);

            while (elapsedTime < duration) {
                m_Rb.velocity = initialVelocity;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            m_Rb.velocity = Vector2.zero;
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
                Destroy(gameObject);
            }
        }
    }
}
