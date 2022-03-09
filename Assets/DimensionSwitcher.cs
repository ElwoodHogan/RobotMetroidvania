using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DimensionSwitcher : SerializedMonoBehaviour
{
    [TableMatrix(HorizontalTitle = "3D RigidBody Contraints", SquareCells = false)]
    public bool[,] Constraints;
    RigidbodyConstraints SavedConstraints;

    [SerializeField] GameObject twoDVariant;

    [SerializeField] ColliderKind Col;
    enum ColliderKind
    {
        Box,
        Sphere,
        Capsule
    }

    private void Start()
    {

        FrontMan.FM.OnDimensionChange += SwitchDimension;
    }
    void SwitchDimension(bool twoDOrThreeD)
    {
        if (twoDOrThreeD)
        {
            DestroyImmediate(GetComponent<Collider>());
            if (TryGetComponent<Rigidbody>(out Rigidbody bruh))
            {
                SavedConstraints = bruh.constraints;
                DestroyImmediate(GetComponent<Rigidbody>());
                gameObject.AddComponent<Rigidbody2D>();
            }

            switch (Col)
            {
                case ColliderKind.Sphere:
                    gameObject.AddComponent<CircleCollider2D>();
                    break;
                case ColliderKind.Box:
                    gameObject.AddComponent<BoxCollider2D>();
                    break;
                case ColliderKind.Capsule:
                    gameObject.AddComponent<CapsuleCollider2D>();
                    break;
                default:
                    break;
            }



        }
        else
        {
            DestroyImmediate(GetComponent<Collider2D>());
            if (TryGetComponent<Rigidbody2D>(out Rigidbody2D bruh))
            {
                DestroyImmediate(GetComponent<Rigidbody2D>());
                gameObject.AddComponent<Rigidbody>();
                GetComponent<Rigidbody>().constraints = SavedConstraints;
            }


            switch (Col)
            {
                case ColliderKind.Sphere:
                    gameObject.AddComponent<SphereCollider>();
                    break;
                case ColliderKind.Box:
                    gameObject.AddComponent<BoxCollider>();
                    break;
                case ColliderKind.Capsule:
                    gameObject.AddComponent<CapsuleCollider>();
                    break;
                default:
                    break;
            }

        }
    }

    private void OnDestroyImmediate()
    {
        FrontMan.FM.OnDimensionChange -= SwitchDimension;
    }

    [OnInspectorInit]
    private void CreateData()
    {
        Constraints = new bool[3, 2]
        {
            {false,false },
            {false,false },
            {false,false },
        };
    }
}