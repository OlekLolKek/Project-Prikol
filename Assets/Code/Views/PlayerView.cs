using System;
using UnityEngine;

namespace ProjectPrikol
{
    public class PlayerView : MonoBehaviour
    {
        public event Action<Collision> OnCollisionStayEvent = delegate(Collision collider) {  };

        private void OnCollisionStay(Collision other)
        {
            OnCollisionStayEvent.Invoke(other);
        }
    }
}