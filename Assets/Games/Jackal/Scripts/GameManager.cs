using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance;
        public Transform SpawnPoint;
        public Movement PlayerPrefab;
        public GameObject ExplosionPrefab;
        public CinemachineVirtualCamera CmVcam1;
        public int NumberAllies = 0;
        public bool CanFly = false;
        public Movement m_Player;
        private void Awake() {
            Instance = this;
        }
        void Start() {
            Respawn(SpawnPoint.position);
        }
        public void Respawn(Vector3 SpawnPos) {
            if (m_Player != null) {
                Destroy(m_Player.gameObject);
            }
            StartCoroutine(Wait(SpawnPos));
        }
        private IEnumerator Wait(Vector3 SpawnPos) {
            yield return new WaitForSeconds(2f);
            m_Player = Instantiate(PlayerPrefab, SpawnPos, Quaternion.identity);
            m_Player.GetComponent<Movement>().CountTime();
            CmVcam1.Follow = m_Player.transform;
            CmVcam1.LookAt = m_Player.transform;
        }
        public bool Count() {
            if (NumberAllies == 0 && CanFly == true) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}