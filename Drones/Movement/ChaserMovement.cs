﻿using UnityEngine;

namespace Assets.Scripts.Drones
{
    public class ChaserMovement : MonoBehaviour
    {
        public float Speed;
        public GameObject Player;

        private float _rotationSpeed;
        private Vector3 _targetPos;
        private Vector3 _direction;
        private Rigidbody _rb;
        private float _acceleration;
        private float _maxSpeed;


        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rotationSpeed = 15f;
            _acceleration = 50f;
            _maxSpeed = Speed;
        }


        private void FixedUpdate()
        {
            if (Player == null || _rb == null) { return; }
            _targetPos = Player.transform.position;
            _targetPos.y += 0.6f;
            var currentSpeed = _rb.velocity.magnitude;
            _direction = (_targetPos - transform.position).normalized;

            if ((_targetPos - transform.position).magnitude > 0.1f)
            {
                _rb.velocity = _direction * currentSpeed;

                if (currentSpeed < _maxSpeed)
                {
                    _rb.AddForce(_direction * _acceleration);
                }

                // Don't accelerate over maxSpeed
                else
                {
                    currentSpeed = _maxSpeed;
                    _rb.velocity = _direction * currentSpeed;
                }

                if (Mathf.Abs(currentSpeed) > 0.00001)
                {
                    Rotate(); 
                }
            }
            else
            {
                _rb.velocity = Vector3.zero;
            }
        }

        // Rotate Player
        private void Rotate()
        {
            var lookrotation = _targetPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), _rotationSpeed * Time.deltaTime);
        }

    }
}