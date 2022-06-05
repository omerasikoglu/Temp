using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Old {
    public class Spawner : MonoBehaviour {
        [SerializeField] private CogaltBeni pfCogaltBeni;
        [SerializeField] private int spawnAmount = 20;

        private ObjectPool<CogaltBeni> pool;

        private void Start() {
            pool = new ObjectPool<CogaltBeni>(() =>
            {
                return Instantiate(pfCogaltBeni, transform.position, Quaternion.identity, transform);
            }, pfCogaltBeni =>
            {
                pfCogaltBeni.gameObject.SetActive(true);
                pfCogaltBeni.transform.position = transform.position + UnityEngine.Random.insideUnitSphere * 10;
                pfCogaltBeni.Init(KillShape);
            }, pfCogaltBeni =>
            {
                pfCogaltBeni.gameObject.SetActive(false);
            }, pfCogaltBeni =>
            {
                Destroy(pfCogaltBeni.gameObject);
            }, false, 10, 20


            );

            InvokeRepeating(nameof(Spawn), 0.2f, 0.2f);
        }

        private void Spawn() {
            pool.Get();
        }
        private void KillShape(CogaltBeni cogaltBeni) {
            pool.Release(cogaltBeni);
        }
    }
}

namespace New {
    public class Spawner : MonoBehaviour {
        [SerializeField] private Bullet pfBullet;

        private IObjectPool<Bullet> bulletPool;

        private void Awake() {
            bulletPool = new ObjectPool<Bullet>(CreateBullet);
        }

        private Bullet CreateBullet() {
            return Instantiate(pfBullet);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.C)) {
                bulletPool.Get();
            }
        }
    }
    public class Bullet : MonoBehaviour {
        private IObjectPool<Bullet> bulletPool;

        public void SetPool(IObjectPool<Bullet> bulletPool)
        {
            this.bulletPool = bulletPool;
        }

        private void OnBecameInvisible()
        {
            throw new NotImplementedException();
        }
    }
}