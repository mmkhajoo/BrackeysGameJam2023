using System;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IPlayer
    {
        Transform Transform { get; }
        void Enable();

        void Disable();
        
        void Die();
    }
}