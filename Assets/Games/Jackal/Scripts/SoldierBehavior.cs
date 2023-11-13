using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class SoldierBehavior : MonoBehaviour {
        public GameObject Bullet;
        public Transform BulletPos;
        private Animator m_SoldierAnimator;
        [SerializeField] private float m_MoveSpeed = .2f;
        private Vector2 m_MovementDirection;
        private float m_MoveTimer = 3f;
        private float m_RestTimer = 5f;
        private float m_TimeDead = 1f;
        private GameObject m_Player;
        private bool m_IsMoving = true;
        private bool m_HasFired = false;
        private Vector2 m_Direction;
        void Start() {
            SetRandomMovementDirection();
            m_SoldierAnimator = GetComponent<Animator>();
        }
        void Update() {
            m_Player = GameObject.FindGameObjectWithTag("Player");
            if (m_Player != null) {
                float distance = Vector2.Distance(transform.position, m_Player.transform.position);
                if (distance < 1.4) {
                    if (m_IsMoving) {
                        Move();
                        m_MoveTimer -= Time.deltaTime;
                        if (m_MoveTimer <= 0) {
                            m_IsMoving = false;
                            m_RestTimer = 4f;
                        }
                    }
                    else {
                        m_RestTimer -= Time.deltaTime;
                        if (!m_HasFired) {
                            StartCoroutine(ShootAndSetFireAnimation());
                        }
                        if (m_RestTimer <= 0) {
                            m_IsMoving = true;
                            m_MoveTimer = 3f;
                            SetRandomMovementDirection();
                            m_HasFired = false;
                        }
                    }
                }
                Animated();
            }
        }
        private IEnumerator ShootAndSetFireAnimation() {
            ShootPlayer();
            m_HasFired = true;
            m_SoldierAnimator.SetBool("SoldierFire", true);
            yield return new WaitForSeconds(0.8f);
            m_SoldierAnimator.SetBool("SoldierFire", false);
        }
        //private void OnCollisionEnter2D(Collision2D collision) {
        //     if (collision.gameObject.layer == LayerMask.NameToLayer("Grenade") || collision.gameObject.layer == LayerMask.NameToLayer("Bullet") || collision.gameObject.layer == LayerMask.NameToLayer("Rocket")) {
        //        m_SoldierAnimator.SetBool("SoldierDie", true);
        //        Destroy(gameObject, m_TimeDead);
        //    }
        //}
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                m_SoldierAnimator.SetBool("SoldierDie", true);
                m_MoveSpeed = 0;
                Destroy(gameObject, m_TimeDead);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Grenade") || collision.gameObject.layer == LayerMask.NameToLayer("Bullet") || collision.gameObject.layer == LayerMask.NameToLayer("Rocket")) {
                m_SoldierAnimator.SetBool("SoldierDie", true);
                m_MoveSpeed = 0;
                Destroy(gameObject, m_TimeDead);
            }
        }
        void Move() {
            transform.Translate(m_MovementDirection * m_MoveSpeed * Time.deltaTime);
        }
        private void Animated() {
            m_Direction = m_MovementDirection.normalized;
            if (m_Direction.magnitude > 0.1f) {
                m_Direction = new Vector2(Mathf.Round(m_Direction.x), Mathf.Round(m_Direction.y));
            }
            m_SoldierAnimator.SetFloat("X", m_Direction.x);
            m_SoldierAnimator.SetFloat("Y", m_Direction.y);
        }
        void SetRandomMovementDirection() {
            m_MovementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
        private void ShootPlayer() {
            Instantiate(Bullet, BulletPos.position, Quaternion.identity);
        }
    }
}