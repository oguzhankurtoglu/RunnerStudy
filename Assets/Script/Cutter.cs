using System;
using UnityEngine;

namespace Script
{
    public class Cutter : MonoBehaviour
    {
        public void Cut(Transform cutter)
        {
            if (cutter.transform.position.x < 0)
            {
                float x = transform.localScale.x;
                x -= transform.position.x;
                float dist = x + cutter.position.x;
                Debug.Log("dist : " + dist);
                if (dist / 2 > 0)
                {
                    // 3 -0.5=2.5
                    // 0 + 0.5 = 0.5

                    transform.localScale = new Vector3(transform.localScale.x - dist / 2, transform.localScale.y,
                        transform.localScale.z);
                    transform.position = new Vector3(transform.position.x + dist / 4, transform.position.y,
                        transform.position.z);
                    // gameObject.SetActive(false);
                    GameObject yeni = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    yeni.transform.localScale = new Vector3(dist / 2, transform.localScale.x, transform.localScale.z);
                    yeni.transform.rotation = transform.rotation;
                    yeni.transform.position = new Vector3(-(x - yeni.transform.localScale.y), transform.position.y,
                        transform.position.z);

                    yeni.AddComponent<Rigidbody>();
                }
            }
            else
            {
                // right
                // scale 3 cutter 1 
                float y = transform.localScale.x;
                y += transform.position.x;
                float dist = y - cutter.position.x;
                Debug.Log("dist : " + dist);

                if (dist / 2 > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x- dist / 2, transform.localScale.y ,
                        transform.localScale.z);
                    transform.position = new Vector3(transform.position.x - dist / 3, transform.position.y,
                        transform.position.z);

                    GameObject yeni = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    yeni.transform.localScale = new Vector3(dist / 2,transform.localScale.y,  transform.localScale.z);
                    yeni.transform.rotation = transform.rotation;
                    yeni.transform.position = new Vector3(y - yeni.transform.localScale.y, transform.position.y,
                        transform.position.z);

                    yeni.AddComponent<Rigidbody>();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag($"Platform"))
            {
                Cut(other.gameObject.transform);
            }
        }
    }
}