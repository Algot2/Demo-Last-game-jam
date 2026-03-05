using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class IKController : MonoBehaviour
{
    public Transform leftFootTarget;   // The IK target of your left Two Bone IK
    public Transform rightFootTarget;  // The IK target of your right Two Bone IK

    public LayerMask groundLayer;
    public float raycastDistance = 1.5f;
    public float footOffset = 0.1f;
    public float smoothing = 10f;
    public Vector3 ofset;

    private void Update()
    {
        SolvFootIK(leftFootTarget);
        SolvFootIK(rightFootTarget);
    }

    void SolvFootIK(Transform footTarget)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z) 
            + Vector3.up * (footTarget.position.y - transform.position.y - 1.2f);
        Ray ray = new Ray(footTarget.position + Vector3.up * 0.5f, Vector3.down);

        if (Physics.Raycast(ray, out var hit, raycastDistance, groundLayer))
        {
            Vector3 targetPos = hit.point + Vector3.up * footOffset;
            footTarget.position = Vector3.Lerp(footTarget.position, targetPos, Time.deltaTime * smoothing);

            Quaternion targetRot = Quaternion.LookRotation(
                Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);

            targetRot *= Quaternion.Euler(ofset);
            footTarget.rotation = Quaternion.Slerp(footTarget.rotation, targetRot, Time.deltaTime * smoothing);
        }
    }
}