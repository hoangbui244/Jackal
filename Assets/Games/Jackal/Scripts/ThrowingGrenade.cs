using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class ThrowingGrenade : MonoBehaviour {
        [Header("Grenade")]
        public GameObject GrenadePrefab;
        public float GrenadeSpeed = 1.5f;
        public Movement MovementInput;
        [Header("Rocket")]
        public GameObject RocketPrefab;
        public float RokcetSpeed = 2.5f;
        private bool m_CanThrowGrenade = true;
        private bool m_IsUpgrade = false;
        private void Update() {
            if (Input.GetKeyDown(KeyCode.Z) && m_CanThrowGrenade == true) {
                if (m_IsUpgrade) {
                    ShootRocket();
                }
                else {
                    ShootGrenade();
                }
            }
        }
        private void ShootGrenade() {
            // Tạo một instance mới của đạn từ prefab
            GameObject grenade = Instantiate(GrenadePrefab, transform.position, Quaternion.identity);
            // Tính toán hướng và vận tốc của đạn
            Vector2 throwDirection = GetComponent<Movement>().MovementInput;
            Rigidbody2D grenadeRigidbody = grenade.GetComponent<Rigidbody2D>();
            grenadeRigidbody.velocity = throwDirection * GrenadeSpeed;
            // Không thể ném lựu đạn mới trong 1.4 giây
            m_CanThrowGrenade = false;
            StartCoroutine(ResetThrowGrenade());
        }
        private IEnumerator ResetThrowGrenade() {
            // Chờ 1.4 giây
            yield return new WaitForSeconds(1.4f);
            // Cho phép ném lựu đạn mới
            m_CanThrowGrenade = true;
        }
        private void ShootRocket() {
            // Tạo một instance mới của đạn từ prefab
            GameObject rocket = Instantiate(RocketPrefab, transform.position, Quaternion.identity);
            // Tính toán hướng và vận tốc của đạn
            Vector2 throwDirection = GetComponent<Movement>().MovementInput;
            Rigidbody2D rocketRigidbody = rocket.GetComponent<Rigidbody2D>();
            rocketRigidbody.velocity = throwDirection * RokcetSpeed;
            // Không thể ném lựu đạn mới trong 1 giây
            m_CanThrowGrenade = false;
            StartCoroutine(ResetThrowRocket());
        }
        private IEnumerator ResetThrowRocket() {
            yield return new WaitForSeconds(1f);
            m_CanThrowGrenade = true;
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PowerUp")) {
                m_IsUpgrade = true;
            }
        }
    }
}