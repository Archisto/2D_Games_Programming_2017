using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class ScrollingBackground : MonoBehaviour
    {
        [SerializeField]
        private GameObject backgroundImage1;

        [SerializeField]
        private GameObject backgroundImage2;

        [SerializeField]
        private bool scrolling = true;

        [SerializeField]
        private float scrollSpeed;

        private float bgHeight;
        private float distanceScrolled;

        // Use this for initialization
        void Start()
        {
            bgHeight = backgroundImage1.GetComponent<SpriteRenderer>().bounds.size.y;

            distanceScrolled = 0;

            Vector3 bg1Pos = backgroundImage1.transform.position;
            backgroundImage2.transform.position = 
                new Vector3(bg1Pos.x, bg1Pos.y + bgHeight, bg1Pos.z);
        }

        // Update is called once per frame
        void Update()
        {
            ScrollDown();

            if (distanceScrolled >= bgHeight)
            {
                MoveImageBackUp();
            }
        }

        private void ScrollDown()
        {
            if (scrolling)
            {
                backgroundImage1.transform.position +=
                    GetSingleFrameMovement();

                backgroundImage2.transform.position +=
                    GetSingleFrameMovement();

                distanceScrolled += Mathf.Abs( GetSingleFrameMovement().y );
            }
        }

        private void MoveImageBackUp()
        {
            distanceScrolled = 0;

            GameObject lowerImage = null;
            if (backgroundImage1.transform.position.y <
                backgroundImage2.transform.position.y)
            {
                lowerImage = backgroundImage1;
            }
            else
            {
                lowerImage = backgroundImage2;
            }

            lowerImage.transform.position +=
                Vector3.up * 2 * bgHeight;
        }

        private Vector3 GetSingleFrameMovement()
        {
            return (Vector3.down * scrollSpeed * Time.deltaTime);
        }
    }
}
