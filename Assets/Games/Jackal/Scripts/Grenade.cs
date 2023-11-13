using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class Grenade : MonoBehaviour {
        public float Life = 1.4f;
        private Animator m_GrenadeAnimator;
        private Rigidbody2D m_Rb;
        private void Awake() {
            m_GrenadeAnimator = GetComponent<Animator>();
            m_Rb = GetComponent<Rigidbody2D>();
            StartCoroutine(ExplodeAfterDelay());
            m_GrenadeAnimator.SetBool("Explosion", false);
        }
        private IEnumerator ExplodeAfterDelay() {
            yield return new WaitForSeconds(0.8f); 
            m_Rb.velocity = Vector2.zero;
            IsExplosion();
            yield return new WaitForSeconds(Life - 0.8f);
            Destroy(gameObject);
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Construction") || collision.gameObject.layer == LayerMask.NameToLayer("Stage") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Turret")) {
                m_Rb.velocity = Vector2.zero;
                IsExplosion();
            }
        }
        public void IsExplosion() {
            m_GrenadeAnimator.SetBool("Explosion", true);
        }
    }
}
