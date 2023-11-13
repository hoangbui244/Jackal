using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class Ship : MonoBehaviour {
        public GameObject PointA;
        public GameObject PointB;
        public GameObject Bullet;
        public Transform BulletPos;
        public int ShipHealth = 3;
        public float ShipSpeed = 1f;
        private int m_CurrentHealth;
        private float m_Timer;
        private GameObject m_Player;
        private bool m_IsMoving = false;
        private Transform m_CurrentPoint;
        private bool m_IsShooting = false;
        private Rigidbody2D m_Rb;
        private void Awake() {
            m_CurrentHealth = ShipHealth;
            m_CurrentPoint = PointA.transform;
            m_Rb = GetComponent<Rigidbody2D>();
        }
        void Update() {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            if (m_Player != null) {
                float distance = Vector2.Distance(transform.position, m_Player.transform.position);
                if (distance < 2.0 && distance > 1.3) {
                    if (!m_IsMoving) {
                        StartCoroutine(MoveToPoint(PointB.transform));
                    }
                }
                else if (distance < 1.3) {
                    m_Timer += Time.deltaTime;
                    if (m_Timer > 2 && !m_IsShooting) {
                        m_Timer = 0;
                        StartCoroutine(Shooting());
                    }
                }
            }
        }
        private IEnumerator MoveToPoint(Transform targetPoint) {
            m_IsMoving = true;
            while (Vector2.Distance(transform.position, targetPoint.position) > 0.1f) {
                Vector2 direction = (targetPoint.position - transform.position).normalized;
                m_Rb.velocity = direction * ShipSpeed;
                yield return null;
            }
            m_Rb.velocity = Vector2.zero;
            m_IsMoving = false;
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet")) {
                m_CurrentHealth--;
                if (m_CurrentHealth <= 0) {
                    Destroy(gameObject);
                }
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Grenade") || collision.gameObject.layer == LayerMask.NameToLayer("Rocket")) {
                m_CurrentHealth = m_CurrentHealth - 3;
                if (m_CurrentHealth <= 0) {
                    Destroy(gameObject);
                }
            }
        }
        IEnumerator Shooting() {
            m_IsShooting = true;
            for (int i = 0; i < 3; i++) {
                Instantiate(Bullet, BulletPos.position, Quaternion.identity);
                // Khoảng thời gian đợi giữa mỗi viên đạn
                yield return new WaitForSeconds(0.2f);
            }
            m_IsShooting = false;
        }
    }
}