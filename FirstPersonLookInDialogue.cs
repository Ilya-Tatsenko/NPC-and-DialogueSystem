using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLookInDialogue : MonoBehaviour
{
    [SerializeField]
    Transform character;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.AngleAxis(0, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(0, Vector3.up);
    }
}
