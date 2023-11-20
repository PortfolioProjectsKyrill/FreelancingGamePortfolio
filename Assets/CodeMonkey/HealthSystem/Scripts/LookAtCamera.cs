using UnityEngine;

namespace CodeMonkey.HealthSystemCM {

    /// <summary>
    /// Utility script to make a Transform look straight at the main camera
    /// Useful for HealthBar World Objects, always face camera
    /// </summary>
    public class LookAtCamera : MonoBehaviour {

        [SerializeField] private bool invert;
        private GameObject Player;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update() {
            LookAt();
        }

        private void LookAt() {
            if (invert) {
                Vector3 dir = (transform.position - Player.transform.position).normalized;
                transform.LookAt(transform.position + dir);
            } else {
                transform.LookAt(Player.transform.position);
            }
        }

    }

}