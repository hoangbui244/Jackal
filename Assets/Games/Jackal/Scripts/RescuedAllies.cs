using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class RescuedAllies : MonoBehaviour {
        public float MovementSpeed = .6f;
        private Animator m_RAAnimator;
        private void Awake() {
            m_RAAnimator = GetComponent<Animator>();
        }
        void Update() {
            transform.Translate(Vector2.left * MovementSpeed * Time.deltaTime);
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("RescueHeli")) {
                Destroy(gameObject);
            }
        }
    }
}