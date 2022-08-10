using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class SlicerRight : MonoBehaviour
    {
        public GameObject slicedPrefab;

        private void Slice(Transform platform)
        {
            var distance = transform.position.x-platform.transform.position.x  ;
            Debug.Log("distance"+distance);
            if (distance > 0)
            {
                var x = platform.transform.localScale.x / 2 + distance;
                platform.transform.localScale = new Vector3(x, platform.localScale.y, platform.localScale.z);

                var posX = transform.position.x - platform.transform.localScale.x / 2;
                platform.transform.position =
                    new Vector3(posX, platform.transform.position.y, transform.position.z);

                var slicedItem = Instantiate(slicedPrefab);
                var slicedX = slicedItem.transform.localScale.x - platform.localScale.x;
                slicedItem.transform.localScale = new Vector3(slicedX, slicedItem.transform.localScale.y,
                    slicedItem.transform.localScale.z);

                var slicedPosX = transform.position.x + slicedX;
                slicedItem.transform.position = new Vector3(slicedPosX, slicedItem.transform.position.y,
                    slicedItem.transform.position.z);

                slicedItem.transform.AddComponent<Rigidbody>().useGravity = true;
            }
            else if (distance < 0)
            {
                var x = platform.transform.localScale.x / 2 + distance;
                platform.transform.localScale = new Vector3(x, platform.localScale.y, platform.localScale.z);

                var posX = transform.position.x - platform.transform.localScale.x / 2;
                platform.transform.position =
                    new Vector3(posX, platform.transform.position.y, transform.position.z);

                var slicedItem = Instantiate(slicedPrefab);
                var slicedX = slicedItem.transform.localScale.x - platform.localScale.x;
                slicedItem.transform.localScale = new Vector3(slicedX, slicedItem.transform.localScale.y,
                    slicedItem.transform.localScale.z);

                var slicedPosX = transform.position.x + slicedX / 2 + 0.3f;
                slicedItem.transform.position = new Vector3(slicedPosX, slicedItem.transform.position.y,
                    slicedItem.transform.position.z);

                slicedItem.transform.AddComponent<Rigidbody>().useGravity = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag($"Platform"))
            {
                //   Cut(other.gameObject.transform);
                Slice(other.transform);
            }
        }
    }
}