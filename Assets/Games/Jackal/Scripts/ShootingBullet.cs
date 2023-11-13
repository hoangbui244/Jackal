using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace io.lockedroom.Games.Jackal {
    public class ShootingBullet : MonoBehaviour {
        public Transform BulletSpawnPoint;
        public GameObject BulletPrefab;
        public float BulletSpeed = 7;
        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                var bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = BulletSpawnPoint.up * BulletSpeed;
            }
        }
    }
}