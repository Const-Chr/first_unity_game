//create a namespace with your student code/name (will be used in all homeworks)
//change file name to Homework1_nmxxxxxxx

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

//Create a script that uses a button to instantiate maxCount prefabs placed on a cirlce.
//Prefabs should sample their color from a gradient.
//The button should create the prefabs with a single click and be disabled while the objects 
//are being created. There should also be a time delay between each object being created.
//change this
namespace nm19716
{
    //change class name to match file name
    public class Homework2_nm19716 : MonoBehaviour
    {
        //create a prefab to instantiate
        public GameObject prefab; //reference to prefab
        //used to sample colors
        public Gradient gradient;
        //amount of gameobjects to create
        public int maxCount = 20;
        //button to press
        public Button button;

        private void Start()
        {
            //either assign prefab from the editor or load from resources

            button.onClick.AddListener(() => StartCoroutine(CreatePrefabs()));

        }

        private IEnumerator CreatePrefabs()
        {
            //complete this
            button.interactable = false;

            for (int ii = 0; ii < maxCount; ii++)
            {
                GameObject obj = GameObject.Instantiate(prefab, RandomPosition(10), Quaternion.identity);

                //find angle and normalize to be used in gradient.Evaluate()
                float circleAngle = Mathf.Atan2(obj.transform.position.z, obj.transform.position.x);
                float normalizedAngle = (circleAngle + Mathf.PI) / (2 * Mathf.PI);
                Color color = gradient.Evaluate(normalizedAngle);
                
                // change color of parent
                //obj.GetComponentInParent<MeshRenderer>().material.color = color;

                // change color of 1st child
                obj.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
                // change color of 2nd child
                obj.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = color;

                //wait 0.5s for every prefab spawn
                yield return new WaitForSeconds(0.5f);
            }
            button.interactable = true;
        }

        

        //change this to return points on a circle
        private Vector3 RandomPosition(int radius)
        {
            Vector2 randomCircle = UnityEngine.Random.insideUnitCircle.normalized * radius;
            return new Vector3(randomCircle.x, 0, randomCircle.y);
        }
    }
}

    
