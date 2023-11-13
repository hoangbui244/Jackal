using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class Player : MonoBehaviour {
        public GameObject RescuedAlliesPrefab;
        public Transform PlayerPos;
        private Coroutine state;
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ally")) {
                GameManager.Instance.NumberAllies++;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("RescuePoint")) {
                state = StartCoroutine(SpawnAllies());
            }
        }
        private void OnTriggerExit2D(Collider2D collision) {
            if (state != null) {
                StopCoroutine(state);
            }
        }
        private IEnumerator SpawnAllies() {
            yield return new WaitForSeconds(1.5f);
            while (GameManager.Instance.NumberAllies > 0) {
                Instantiate(RescuedAlliesPrefab, PlayerPos.position, Quaternion.identity);
                GameManager.Instance.NumberAllies--;
                yield return new WaitForSeconds(1.2f);
            }
            StartCoroutine(Wait());
        }
        private IEnumerator Wait() {
            {
                yield return new WaitForSeconds(3f);
                GameManager.Instance.CanFly = true;
            }
        }
    }
}