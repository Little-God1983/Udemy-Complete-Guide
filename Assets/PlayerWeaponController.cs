using System.Collections;
using UnityEngine;

namespace Assets
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private Player player;

        private void Start()
        {
            player = GetComponent<Player>();
            player.controls.Character.Fire.performed += ctx => Shoot();

        }
        private void Shoot()
        {
            GetComponentInChildren<Animator>().SetTrigger("Fire");
            //animator.SetTrigger("Fire");
        }
    }
}