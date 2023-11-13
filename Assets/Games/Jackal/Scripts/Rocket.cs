using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class Rocket : MonoBehaviour {
        public float Life = 1.2f;
        private Animator m_RocketAnimator;
        private Rigidbody2D m_Rb;
        private void Awake() {
            m_RocketAnimator = GetComponent<Animator>();
            m_Rb = GetComponent<Rigidbody2D>();
            StartCoroutine(ExplodeAfterDelay());
            m_RocketAnimator.SetBool("IsRExplosion", false);
        }
        private IEnumerator ExplodeAfterDelay() {
            yield return new WaitForSeconds(0.6f);
            m_Rb.velocity = Vector2.zero;
            IsExplosion();
            yield return new WaitForSeconds(Life - 0.6f);
            Destroy(gameObject);
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Construction") || collision.gameObject.layer == LayerMask.NameToLayer("Stage") || collision.gameObject.layer == LayerMask.NameToLayer("Stones & Trees")) {
                m_Rb.velocity = Vector2.zero;
                IsExplosion();
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Turret") || collision.gameObject.layer == LayerMask.NameToLayer("Hill")) {
                m_Rb.velocity = Vector2.zero;
                IsExplosion();
            }
        }
        public void IsExplosion() {
            m_RocketAnimator.SetBool("IsRExplosion", true);
        }
    }
}
