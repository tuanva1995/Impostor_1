using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomUtils
{
    public static class CustomAnimator
    {
        /// <summary>
        /// Get seconds length of a animation 
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="animationName"></param>
        /// <returns></returns>
        public static float GetAnimationLength(Animator animator, string animationName)
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;    //Get Animator controller
            for (int i = 0; i < ac.animationClips.Length; i++)                 //For all animations
            {
                if (ac.animationClips[i].name == animationName)        //If it has the same name as your clip
                {
                    return ac.animationClips[i].length;
                }
            }
            Debug.LogError("Dont exist " + animationName + " in animator of " + animator.gameObject);
            return 0;
        }
    }
}
