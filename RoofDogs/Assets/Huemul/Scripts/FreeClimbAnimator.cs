using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class FreeClimbAnimator : MonoBehaviour
    {
        Animator anim;

        IKSnapshot ikBase;
        IKSnapshot curret = new IKSnapshot();
        IKSnapshot next = new IKSnapshot();

        public float w_rh;
        public float w_lh;
        public float w_rf;
        public float w_lf;

        Vector3 rh, lh, rf, lf;
        Transform h;

        public void OnServerInitialized(FreeClimb c, Transform helper)
        {
            anim = c.anim;
            ikBase = c.baseIKsnapshot;
            h = helper;
        }

        public void CreatePosition(Vector3 origin)
        {
            IKSnapshot ik = CreateSnapshot(origin);
            CopySnapShot(ref curret, ik);

            UpdateIKPosition(AvatarIKGoal.LeftFoot, curret.lf);
            UpdateIKPosition(AvatarIKGoal.RightFoot, curret.rf);
            UpdateIKPosition(AvatarIKGoal.LeftHand, curret.lh);
            UpdateIKPosition(AvatarIKGoal.RightHand, curret.rh);

            UpdateIKWeight(AvatarIKGoal.LeftFoot, 1);
            UpdateIKWeight(AvatarIKGoal.RightFoot, 1);
            UpdateIKWeight(AvatarIKGoal.LeftHand, 1);
            UpdateIKWeight(AvatarIKGoal.RightHand, 1);
        }

        public IKSnapshot CreateSnapshot(Vector3 o)
        {
            IKSnapshot r = new IKSnapshot();
            r.lh = LocalToWorld(ikBase.lh);
            r.rh = LocalToWorld(ikBase.rh);
            r.lf = LocalToWorld(ikBase.lf);
            r.rf = LocalToWorld(ikBase.rf);
            return r;
        }

        Vector3 LocalToWorld(Vector3 p)
        {
            Vector3 r = h.position;
            r += h.right * p.x;
            r += h.forward * p.z;
            r += h.up * p.y;
            return r;
        }

        public void CopySnapShot(ref IKSnapshot to, IKSnapshot from)
        {
            to.rh = from.rh;
            to.lh = from.lh;
            to.rf = from.rf;
            to.lf = from.lf;
        }

        public void UpdateIKPosition(AvatarIKGoal goal, Vector3 pos)
        {
            switch (goal)
            {
                case AvatarIKGoal.LeftFoot:
                    lf = pos;
                    break;
                case AvatarIKGoal.RightFoot:
                    rf = pos;
                    break;
                case AvatarIKGoal.LeftHand:
                    lh = pos;
                    break;
                case AvatarIKGoal.RightHand:
                    rh = pos;
                    break;
                default:
                    break;
            }
        }
        public void UpdateIKWeight(AvatarIKGoal goal, float w)
        {
            switch (goal)
            {
                case AvatarIKGoal.LeftFoot:
                    w_lf = w;
                    break;
                case AvatarIKGoal.RightFoot:
                    w_rf = w;
                    break;
                case AvatarIKGoal.LeftHand:
                    w_lh = w;
                    break;
                case AvatarIKGoal.RightHand:
                    w_rh = w;
                    break;
                default:
                    break;
            }

            void OnAnimatorIK()
            {
                SetIKPos(AvatarIKGoal.LeftHand, lh, w_lh);
                SetIKPos(AvatarIKGoal.LeftHand, rh, w_rh);
                SetIKPos(AvatarIKGoal.LeftHand, lf, w_lf);
                SetIKPos(AvatarIKGoal.LeftHand, rf, w_rf);
            }

            void SetIKPos(AvatarIKGoal goal, Vector3 tp, float w)
            {
                anim.SetIKPositionWeight(goal, w);
                anim.SetIKPosition(goal, tp);
            }
        }
    }
}