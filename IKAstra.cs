using UnityEngine;

[RequireComponent(typeof(Animator))]

public class IKAstra : MonoBehaviour
{
    private RealsenseController RSController;
    protected Animator animator;
    private Transform rightHandCoordinate;
    private Transform leftHandCoordinate;
    public Transform myRightHand, myLeftHand, LKnee, RKnee, Spine, LElbow, RElbow, LFoot, RFoot;

    void Start()
    {
        animator = GetComponent<Animator>();
        rightHandCoordinate = GameObject.Find("LeftHandCube").transform;
        leftHandCoordinate = GameObject.Find("RightHandCube").transform;
    }
    private void LateUpdate()
    {
        
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, rightHandCoordinate.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, leftHandCoordinate.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

            animator.SetIKPosition(AvatarIKGoal.RightFoot, RFoot.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, LFoot.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

            animator.bodyPosition = Spine.position;

            animator.SetIKHintPosition(AvatarIKHint.LeftElbow, LElbow.position);
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
            animator.SetIKHintPosition(AvatarIKHint.RightElbow, RElbow.position);
            animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);

            //animator.SetIKHintPosition(AvatarIKHint.RightKnee, RKnee.position);
            //animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 1);
            //animator.SetIKHintPosition(AvatarIKHint.LeftKnee, LKnee.position);
            //animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 1);

        }

    }
}
