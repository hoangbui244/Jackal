using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class TurretBullet : MonoBehaviour {
        public float Life = 1f;
        public float Force = 1f;
        private GameObject m_Player;
        private Rigidbody2D m_Rb;
        void Awake() {
            m_Rb = GetComponent<Rigidbody2D>();
            m_Player = GameObject.FindGameObjectWithTag("Player");
            Vector3 direction = m_Player.transform.position - transform.position;
            m_Rb.velocity = new Vector2 (direction.x, direction.y).normalized * Force;
            Destroy(gameObject, Life);
        }
        private void Update() {
            if (m_Player == null) {
                Destroy(gameObject, Life);
            }
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Stones & Trees") || collision.gameObject.layer == LayerMask.NameToLayer("Construction") || collision.gameObject.layer == LayerMask.NameToLayer("Stage")) {
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Player")) {
                Destroy(gameObject);
            }
        }
    }
}