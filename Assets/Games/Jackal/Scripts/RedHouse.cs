using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class RedHouse : MonoBehaviour {
        [Header("Allies")]
        public GameObject AllyStayPrefab;
        public GameObject Allies;
        public Transform AlliesPos;
        public Transform ARH;
        public int AlliesNumber = 2;
        [Header("Sprites")]
        public SpriteRenderer SpriteRenderer;
        public Sprite Sprite1;
        public Sprite Sprite2;
        public ParticleSystem ExplosionParticles;
        private Animator m_ConstructionAnimator;
        private bool m_HasCollided = false;
        private int m_CurrentAlliesCount = 0;
        private GameObject m_AllyStay;
        private GameObject m_AllyMove;
        private void Awake() {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            m_ConstructionAnimator = GetComponent<Animator>();
        }
        private void Update() {
            if (m_CurrentAlliesCount == AlliesNumber) {
                Destroy(m_AllyStay);
            }
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Grenade") && !m_HasCollided) {
                if (SpriteRenderer.sprite == Sprite1) {
                    StartCoroutine(AnimateConstruction());
                }
                ExplosionParticles.Play();
                m_HasCollided = true;
                SpriteRenderer.sprite = Sprite2; 
                AllyRaiseHand();
                StartCoroutine(Rescue());
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Rocket") && !m_HasCollided) {
                if (SpriteRenderer.sprite == Sprite1) {
                    StartCoroutine(AnimateConstruction());
                }
                ExplosionParticles.Play();
                m_HasCollided = true;
                SpriteRenderer.sprite = Sprite2;
                AllyRaiseHand();
                StartCoroutine(Rescue());
            }
        }
        private IEnumerator Rescue() {
            while(m_CurrentAlliesCount != AlliesNumber) {
                yield return null;
                if (m_AllyMove) {
                    continue;
                }
                m_AllyMove = Instantiate(Allies, AlliesPos.position, Quaternion.identity);
                m_CurrentAlliesCount++;
            }
        }
        private IEnumerator AnimateConstruction() {
            m_ConstructionAnimator.SetBool("Explode", true);
            yield return new WaitForSeconds(3f);
            m_ConstructionAnimator.SetBool("Explode", false);
        }
        private void AllyRaiseHand() {
            m_AllyStay = Instantiate(AllyStayPrefab, ARH.position, Quaternion.identity);
        }
    }
}
