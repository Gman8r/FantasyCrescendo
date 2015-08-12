﻿using UnityEngine;

namespace Hourai {

    [RequireComponent(typeof (Collider))]
    public class Ground : MonoBehaviour {

        private void OnCollisionEnter(Collision collision) {
            Change(collision.collider, true);
        }

        private void OnCollisionExit(Collision collision) {
            Change(collision.collider, false);
        }

        private void OnTriggerEnter(Collider other) {
            Change(other, true);
        }

        private void OnTriggerExit(Collider other) {
            Change(other, false);
        }

        private void Change(Collider target, bool targetValue) {
            if (target == null || !Game.IsPlayer(target))
                return;

            var character = target.GetComponentInParent<Character>();

            character.IsGrounded = targetValue;
        }

    }

}