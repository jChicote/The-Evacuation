using UnityEngine;

namespace TheEvacuation.Weapon.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        public Rigidbody2D projectileRB;
        public float projectileSpeed = 5f;
        public float projectileLife = 5f;
        public float projectileDamage = 10f;

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            projectileRB.velocity = transform.up * projectileSpeed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(this.tag)) return;

            //print("Has Collided with something");
        }
    }

}