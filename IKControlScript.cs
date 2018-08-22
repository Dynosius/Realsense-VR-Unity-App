using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKControlScript : MonoBehaviour
{
    private RealsenseController RSController;
    protected Animator animator;
    public Transform rightHandMiddleFinger;
    public Transform leftHandMiddleFinger;
    private Transform rightHandCoordinate;
    private Transform leftHandCoordinate;
    private Transform myRightHand, myLeftHand;
    private Quaternion defaultLeftRotation, defaultRightRotation;
    private Quaternion rightHandUp = Quaternion.Euler(new Vector3(0, 0, -45));
    private Quaternion leftHandUp = Quaternion.Euler(new Vector3(0, 0, 45));

    void Start()
    {
        animator = GetComponent<Animator>();
        rightHandCoordinate = GameObject.Find("lijevaSaka").transform;
        leftHandCoordinate = GameObject.Find("desnaSaka").transform;
        myRightHand = rightHandMiddleFinger.parent;
        myLeftHand = leftHandMiddleFinger.parent;
        defaultLeftRotation = myLeftHand.rotation;
        defaultRightRotation = myRightHand.rotation;
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
