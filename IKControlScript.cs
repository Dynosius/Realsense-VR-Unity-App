using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKControlScript : MonoBehaviour
{
    private RealsenseController RSController;
    protected Animator animator;
    private Transform rightHandCoordinate;
    private Transform leftHandCoordinate;
    public Transform myRightHand, myLeftHand;
    private Quaternion defaultLeftRotation, defaultRightRotation;
    private Quaternion rightHandUp = Quaternion.Euler(new Vector3(0, 0, -45));
    private Quaternion leftHandUp = Quaternion.Euler(new Vector3(0, 0, 45));

    private Transform leftHandReference;
    private Transform rightHandReference;

    void Start()
    {
        leftHandReference = GameObject.Find("ispruzenaLijevaReference").transform;
        rightHandReference = GameObject.Find("ispruzenaDesnaReference").transform;
        animator = GetComponent<Animator>();
        rightHandCoordinate = GameObject.Find("lijevaSaka").transform;
        leftHandCoordinate = GameObject.Find("desnaSaka").transform;
        defaultLeftRotation = myLeftHand.rotation;
        defaultRightRotation = myRightHand.rotation;


    }
    private void TransformHand(Transform original, Transform reference)
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
                TransformHand(original.GetChild(i), reference.GetChild(i));
            }
            else return;
        }
    }
    private void LateUpdate()
    {
        TransformHand(myLeftHand, leftHandReference);
        TransformHand(myRightHand, rightHandReference);
        if(myRightHand.transform.position.y > 3.7f)
        {
            myRightHand.localRotation = Quaternion.Euler(rightHandReference.localRotation.eulerAngles);
        }
        else
        {
            myRightHand.localRotation = Quaternion.Euler(Vector3.zero);
        }
        if (myLeftHand.transform.position.y > 3.7f)
        {
            myLeftHand.localRotation = Quaternion.Euler(leftHandReference.localRotation.eulerAngles);
        }
        else
        {
            myLeftHand.localRotation = Quaternion.Euler(Vector3.zero);
        }
        
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandCoordinate.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandCoordinate.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);


            //if (rightHandCoordinate.position.y > 3.5f)
            //{
            //    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandUp);
            //    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            //    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandUp);
            //    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            //}
            //else
            //{
            //    animator.SetIKRotation(AvatarIKGoal.LeftHand, defaultLeftRotation);
            //    animator.SetIKRotation(AvatarIKGoal.RightHand, defaultRightRotation);
            //    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            //    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            //}
        }

    }
}
