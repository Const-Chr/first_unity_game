//create a namespace with your student code/name (will be used in all homeworks)
//change file name to Homework1_nmxxxxxxx

//change this code to create primitives (random or of your choice) on a circle, whose colors
//are defined by a gradient.

using System;
using UnityEngine;
using UnityEngine.UI;

//change this
namespace nm19716 
{
    //change class name to match file name
    public class Homework1_nm19716 : MonoBehaviour
    {
        private class OBJ
        {
            public static int count = 0;
            [SerializeField]
            private string name;
            [SerializeField]
            private PrimitiveType primitiveType;
            [SerializeField]
            private Material material;

            public OBJ(PrimitiveType primitiveType, Material material)
            {
                this.primitiveType = primitiveType;
                this.material = material;
            }

            public float CreateOBJ()
            {
                name = string.Format("object {0}", count);
                GameObject obj = GameObject.CreatePrimitive(primitiveType);
                obj.transform.position = RandomPosition(8);
                //find angle and normalize to be used in gradient.Evaluate()
                float circleAngle = Mathf.Atan2(obj.transform.position.y, obj.transform.position.x);
                float normalizedAngle = (circleAngle + Mathf.PI) / (2 * Mathf.PI);

                obj.name = name;
                obj.GetComponent<MeshRenderer>().material = material;
                count++;
                return normalizedAngle;
            }

            //change this to return points on a circle
            private Vector3 RandomPosition(int radius)
            {
                Vector2 randomCircle = UnityEngine.Random.insideUnitCircle.normalized * radius;
                return new Vector3(randomCircle.x, radius, randomCircle.y);
            }
        }

        private Button button;
        //change this to test 
        public int buttonsToCreate = 25;
        [Tooltip("Dont forget to change this!")]
        public Gradient gradient;
        public Material mat;

        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(CreateObject);
        }

        private void CreateObject()
        {
            if (OBJ.count <= buttonsToCreate)
            {
                Material material = new Material(mat);
                //change this to use colors from gradient
                
                PrimitiveType pType = PrimitiveType.Capsule;
                GetRandomPrimitiveType(out pType);
                OBJ obj = new OBJ(pType, material);
                float normalAngle = obj.CreateOBJ();

                //
                Color color = gradient.Evaluate(normalAngle);
                material.color = color;
            }
        }

        private void GetRandomPrimitiveType(out PrimitiveType pType) //PrimitiveType&
        {
            Array array = Enum.GetValues(typeof(PrimitiveType));
            pType = PrimitiveType.Plane;
            int rnd = UnityEngine.Random.Range(0, array.Length);
            if (rnd > 3)
            {
                GetRandomPrimitiveType(out pType);
                return;
            }
            pType = (PrimitiveType)array.GetValue(rnd);
        }
    }
}
