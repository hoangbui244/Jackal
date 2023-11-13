using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class Movement : MonoBehaviour {
        public float MovementSpeed;
        public GameObject ExplosionPrefab;
        public GameObject JeepPrefab;
        public Vector2 MovementInput;
        private Rigidbody2D m_Rb;
        private Animator m_MovementAnimator;
        private GameObject m_Boom;
        private GameObject m_Sprite;
        private void Awake() {
            m_Rb = GetComponent<Rigidbody2D>();
            m_MovementAnimator = GetComponent<Animator>();
            m_Sprite = this.transform.Find("Sprite").gameObject;
        }
        private void Update() {
            Move();
            Animated();
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("TurretBullet") || collision.gameObject.layer == LayerMask.NameToLayer("Tank") || collision.gameObject.layer == LayerMask.NameToLayer("Turret")) {
                m_Boom = Instantiate(ExplosionPrefab, gameObject.transform.position, Quaternion.identity);
                Destroy(m_Boom, 0.6f);
                GameManager.Instance.Respawn(JeepPrefab.transform.position);
            }
        }
        private IEnumerator InvicibleTime() {
            m_MovementAnimator.SetBool("IsRespawn", true);
            m_Sprite.SetActive(false);
            yield return new WaitForSeconds(3f);
            m_Sprite.SetActive(true);
            m_MovementAnimator.SetBool("IsRespawn", false);
        }
        public void CountTime() {
            StartCoroutine(InvicibleTime());
        }
        private void Move() {
            float Horizontal = Input.GetAxisRaw("Horizontal");
            float Vertical = Input.GetAxisRaw("Vertical");
            if (Horizontal == 0 && Vertical == 0) {
                m_Rb.velocity = new Vector2(0, 0);
                return;
            }
            MovementInput = new Vector2(Horizontal, Vertical).normalized;
            m_Rb.velocity = MovementInput * MovementSpeed * Time.fixedDeltaTime;
        }
        private void Animated() {
            m_MovementAnimator.SetFloat("X", MovementInput.x);
            m_MovementAnimator.SetFloat("Y", MovementInput.y);
        }
    }
}