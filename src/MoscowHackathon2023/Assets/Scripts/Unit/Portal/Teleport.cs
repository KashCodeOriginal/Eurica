﻿using Unit.Player;
using UnityEngine;

namespace Unit.Portal
{
    public class Teleport : MonoBehaviour
    {
        private Teleport _other;        
        private float _offset = 1f;
        private LayerMask _grabbedLayer;

        private void Start()
        {
            _grabbedLayer = LayerMask.NameToLayer("Grabbed");
        }

        public void TurnOn(Teleport other) 
        {
            enabled = true;
            _other = other;
        }

        public void TurnOff() 
        {
            enabled = false;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!collider.TryGetComponent(out Rigidbody rigidbodyObject))
            {
                return;
            }
            
            var rigidBodyTransform =  rigidbodyObject.transform;
            
            if (rigidbodyObject.CompareTag("Player"))
            {
                var turnAngle = Quaternion.Angle(_other.transform.rotation, rigidbodyObject.transform.rotation);
                
                rigidbodyObject.GetComponent<PlayerRotation>().ForceTurnAngle(turnAngle);

                rigidBodyTransform.position = _other.transform.position + (_other.transform.forward * _offset);
                
                rigidbodyObject.velocity = rigidbodyObject.velocity.magnitude * _other.transform.forward;
                
                return;
            }

            rigidBodyTransform.position = _other.transform.position + (_other.transform.forward * _offset);
            rigidBodyTransform.rotation = _other.transform.rotation;

            if (rigidbodyObject.gameObject.layer == _grabbedLayer)
            {
                return;
            }
            
            rigidbodyObject.velocity = rigidbodyObject.velocity.magnitude * _other.transform.forward;
        }
    }
}