using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class GravityController : MonoBehaviour
    {
        [SerializeField] private float _gravityValue = 30f;


        [SerializeField] private ConstantForce2D _constantForce2D;

        private void Awake()
        {
            _constantForce2D = GetComponent<ConstantForce2D>();
        }


        private void Update()
        {
            _constantForce2D.force = new Vector2(0, -_gravityValue);
        }
    }
}