using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class MilitaryHeli : MonoBehaviour {
        public Transform HeliSpawnPos;
        private float m_HeliSpeed = .8f;
        private void Awake() {
            StartCoroutine(MoveAndStop());
        }
        private IEnumerator MoveAndStop() {
            Rigidbody2D heliRigidbody = GetComponent<Rigidbody2D>();
            heliRigidbody.velocity = HeliSpawnPos.up * m_HeliSpeed;
            yield return new WaitForSeconds(1.5f);
            heliRigidbody.velocity = Vector2.zero;
            yield return new WaitForSeconds(1f);
            heliRigidbody.velocity = HeliSpawnPos.up * m_HeliSpeed;
        }
        private void Update() {
            Destroy(gameObject, 5.5f);
        }
    }
}