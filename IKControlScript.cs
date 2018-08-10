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

    void Start()
    {
        animator = GetComponent<Animator>();
        rightHandCoordinate = GameObject.Find("lijevaSaka").transform;
        leftHandCoordinate = GameObject.Find("desnaSaka").transform;
        myRightHand = rightHandMiddleFinger.parent;
        myLeftHand = leftHandMiddleFinger.parent;
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
        }
    }
}
