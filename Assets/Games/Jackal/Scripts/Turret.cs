using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class Turret : MonoBehaviour {
        [Header("Bullet")]
        public GameObject Bullet;
        public Transform BulletPos;
        public int TurretHealth = 3;
        [Header("Sprites")]
        public SpriteRenderer SpriteRenderer;
        public Sprite Sprite1;
        public Sprite Sprite2;
        public ParticleSystem ExplosionParticles;
        private int m_CurrentHealth;
        private float m_Timer;
        private GameObject m_Player;
        private bool m_IsDisappear;
        private Collider2D m_TurretCollider;
        private bool m_IsShooting = false;
        private void Awake() {
            m_CurrentHealth = TurretHealth;
            SpriteRenderer = GetComponent<SpriteRenderer>();
            m_TurretCollider = GetComponent<Collider2D>();
            m_IsDisappear = false;
        }
        void Update() {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            if (m_Player != null) {
                float distance = Vector2.Distance(transform.position, m_Player.transform.position);
                if (distance < 1.4 && !m_IsDisappear) {
                    m_Timer += Time.deltaTime;
                    if (m_Timer > 2 && !m_IsShooting) {
                        m_Timer = 0;
                        StartCoroutine(Shooting());
                    }
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet")) {
                m_CurrentHealth--;
                if (m_CurrentHealth <= 0) {
                    Disappear();
                }
            } else if (collision.gameObject.layer == LayerMask.NameToLayer("Grenade") || collision.gameObject.layer == LayerMask.NameToLayer("Rocket")) {
                m_CurrentHealth = m_CurrentHealth - 3;
                if (m_CurrentHealth <= 0) {
                    Disappear();
                }
            } else if (collision.gameObject.CompareTag("Player")) {
                m_CurrentHealth = m_CurrentHealth - 3;
                if (m_CurrentHealth <= 0) {
                    Disappear();
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
        private void Disappear() {
            if (SpriteRenderer.sprite == Sprite1) {
                SpriteRenderer.sprite = Sprite2;
            }
            ExplosionParticles.Play();
            m_IsDisappear = true;
            m_TurretCollider.enabled = false;
        }
    }
}