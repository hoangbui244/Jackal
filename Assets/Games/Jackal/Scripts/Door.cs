using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class Door : MonoBehaviour {
        public SpriteRenderer SpriteRenderer;
        public Sprite Sprite1;
        public Sprite Sprite2;
        public ParticleSystem ExplosionParticles;
        private Collider2D m_DoorCollider;
        private void Awake() {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            m_DoorCollider = GetComponent<Collider2D>();
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Grenade") || collision.gameObject.layer == LayerMask.NameToLayer("Rocket")) {
                if (SpriteRenderer.sprite == Sprite1) {
                    SpriteRenderer.sprite = Sprite2;
                }
                ExplosionParticles.Play();
                m_DoorCollider.enabled = false;
            }
        }
    }
}
