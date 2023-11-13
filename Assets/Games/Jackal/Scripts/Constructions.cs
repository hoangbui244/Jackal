using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class Constructions : MonoBehaviour {
        [Header("PowerUp")]
        public GameObject PowerUp;
        public Transform PowerUpPos;
        [Header("Sprites")]
        public SpriteRenderer SpriteRenderer;
        public Sprite Sprite1;
        public Sprite Sprite2;
        public ParticleSystem ExplosionParticles;
        private void Awake() {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Grenade") || collision.gameObject.layer == LayerMask.NameToLayer("Rocket")) {
                if (SpriteRenderer.sprite == Sprite1) {
                    SpriteRenderer.sprite = Sprite2;
                    StartCoroutine(PowerUP());
                }
                ExplosionParticles.Play();
            }
        }
        IEnumerator PowerUP() {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PowerUp"), LayerMask.NameToLayer("Construction"), true);
            Instantiate(PowerUp, PowerUpPos.position, Quaternion.identity);
            Physics2D.IgnoreCollision(PowerUp.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            yield return new WaitForSeconds(3f);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PowerUp"), LayerMask.NameToLayer("Construction"), false);
        }
    }
}
