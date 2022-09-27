using System;
using System.Collections;
using Trell.Flexus_TZ.Core.Pool;
using UnityEngine;

namespace Trell.Flexus_TZ.Visual
{
	public class DustSpawner : MonoBehaviour
	{
        [SerializeField] private Dust _dustPrefab;

        [SerializeField] private int _initSpawningCount = 3;

		private Pool<Dust> _pool;


        private void Awake()
        {
            Func<Dust> factory = () => Instantiate(_dustPrefab);

            _pool = new Pool<Dust>(factory, dust => { }, dust => { }, _initSpawningCount);
        }
 
        public void Spawn(Vector3 position, Quaternion rotation)
        {
            Dust dust = _pool.Give();

            dust.transform.position = position;
            dust.transform.rotation = rotation;
            dust.Play();

            StartCoroutine(ReturnToPoolCorun(dust));
        }

        private IEnumerator ReturnToPoolCorun(Dust dust)
        {
            yield return new WaitForSeconds(dust.Duration);
            _pool.Take(dust);
        }
    }
}