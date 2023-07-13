using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{   
        private bool isAttached = false;

        public void AttachTo(GameObject target, Vector3 position)
        {
            if (!isAttached)
            {
                isAttached = true;
                transform.parent = target.transform;
                transform.position = position;
            }
        }   
}
