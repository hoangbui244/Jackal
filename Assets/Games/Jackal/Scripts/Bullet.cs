using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class Bullet : MonoBehaviour {
        public float Life = 1.5f;
        private void Awake() {
            Destroy(gameObject, Life) ;
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Stones & Trees") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Turret") || collision.gameObject.layer == LayerMask.NameToLayer("Construction") || collision.gameObject.layer == LayerMask.NameToLayer("Stage")) {
                Destroy(gameObject);
            }
        }
    }
}