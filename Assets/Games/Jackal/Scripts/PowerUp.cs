using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class PowerUp : MonoBehaviour {
        public float MoveSpeed = .3f;
        private Animator m_PowerUpAnimator;
        private bool m_IsMoving = true;
        private bool m_Moving = true;
        private bool m_HasStopped = false;
        private float m_MoveTimer = 3f;
        private float m_RestTimer = 3f;
        private Vector2 m_Direction;
        private Rigidbody2D m_Rb;
        private void Awake() {
            m_Rb = GetComponent<Rigidbody2D>();
            m_PowerUpAnimator = GetComponent<Animator>();
        }
        private void Start() {
            MovementDown();
        }
        void Update() {
            if (m_Moving && !m_IsMoving) {
                Move();
                m_MoveTimer -= Time.deltaTime;
                if (m_MoveTimer <= 0) {
                    m_Moving = false;
                    m_RestTimer = 3f;
                }
            }
            else if (!m_Moving && !m_IsMoving) {
                m_RestTimer -= Time.deltaTime;
                if (!m_HasStopped) {
                    StartCoroutine(RescueAnimation());
                }
                if (m_RestTimer <= 0) {
                    m_Moving = true;
                    m_MoveTimer = 3f;
                    SetRandomMovementDirection();
                    m_HasStopped = false;
                }
            }
        }
        private IEnumerator RescueAnimation() {
            m_HasStopped = true;
            m_PowerUpAnimator.SetBool("IsStop", true);
            yield return new WaitForSeconds(2.5f);
            m_PowerUpAnimator.SetBool("IsStop", false);
        }
        void MovementDown() {
            if (m_IsMoving) {
                Vector3 direction = Vector3.down;
                m_Rb.velocity = direction * MoveSpeed;
                StartCoroutine(StopMovementAfterDelay(1.1f));
            }
        }
        void SetRandomMovementDirection() {
            m_Direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        void Move() {
            transform.Translate(m_Direction * MoveSpeed * Time.deltaTime);
        }
        private IEnumerator StopMovementAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);
            m_Rb.velocity = Vector2.zero;
            m_IsMoving = false;
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
                Destroy(gameObject);
            }
        }
    }
}