using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour
{
    public static string SourceOfContact { get; set; }
    private bool cupHeldWithRightHand = false, cupHeldWithLeftHand = false;

    public Transform leftHand;
    public Transform rightHand;
    // Transform
    public Transform holdingCupLeft;
    public Transform holdingCupRight;

    public Transform flatLeft;
    public Transform flatRight;

    private Transform rightHandCoordinate, leftHandCoordinate;
    protected Animator animator;
    private RealsenseController RSController;

    private Transform cup, cupReferenceLeft, cupReferenceRight;
    // Use this for initialization
    void Start()
    {
        SourceOfContact = "";
        animator = GetComponent<Animator>();
        rightHandCoordinate = GameObject.Find("lijevaSaka").transform;
        leftHandCoordinate = GameObject.Find("desnaSaka").transform;
        cup = GameObject.Find("CupObject").transform;
        cupReferenceLeft = GameObject.Find("SalicaReferenceLijeva").transform;
        cupReferenceRight = GameObject.Find("SalicaReferenceDesna").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (SourceOfContact == "RightHand")
        {
            Debug.Log("Desna ruka");
            // tu pozicija salice u ruci
            cup.parent = rightHand;
            cup.position = cupReferenceRight.position;
            cup.rotation = Quaternion.Euler(cupReferenceRight.rotation.eulerAngles);

            cupHeldWithLeftHand = false;
            cupHeldWithRightHand = true;
        }
        else if (SourceOfContact == "LeftHand")
        {
            Debug.Log("Lijeva ruka");
            cup.parent = leftHand;
            cup.position = cupReferenceLeft.position;
            cup.rotation = Quaternion.Euler(cupReferenceLeft.rotation.eulerAngles);
            cupHeldWithLeftHand = true;
            cupHeldWithRightHand = false;
        }

        if(cupHeldWithRightHand)
        {
            TransformStature(rightHand, flatRight, holdingCupRight);
        }
        else if (cupHeldWithLeftHand)
        {
            TransformStature(leftHand, flatLeft, holdingCupLeft);
        }
        SourceOfContact = "";
    }

    private void TransformGrab(Transform original, Transform reference)
    {
        Vector3 eulerRotation = Vector3.zero;
        for (int i = 0; i < original.childCount; i++)
        {
            Vector3 temp = reference.GetChild(i).localEulerAngles;
            eulerRotation.x = temp.x;
            eulerRotation.y = temp.y;
            eulerRotation.z = temp.z;
            original.GetChild(i).localRotation = Quaternion.Euler(eulerRotation);
            if (original.childCount > 0)
            {
                TransformGrab(original.GetChild(i), reference.GetChild(i));
            }
            else return;
        }
    }

    private void TransformStature(Transform original, Transform jointReference, Transform reference)
    {
        //original.GetChild(0).rotation = Quaternion.Euler(jointReference.GetChild(0).rotation.eulerAngles);
        TransformGrab(original.GetChild(0), reference.GetChild(0));
    }

    void OnAnimatorIK()
    {
        if (animator)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandCoordinate.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandCoordinate.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        }
    }
}
