using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public static partial class Extensions
{
    #region Transform
    /// <summary>재귀적으로 이름에 맞는 트랜스폼의 자식을 반환하는 함수</summary>
    public static Transform FindChildRecursive(this Transform parent, string name)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            if (child.name.Contains(name))
                return child;

            var result = child.FindChildRecursive(name);
            if (result != null)
                return result;
        }

        return null;
    }

    static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
    {
#if UNITY_2019_2_OR_NEWER
        if (!gameObject.TryGetComponent<T>(out var component))
        {
            component = gameObject.AddComponent<T>();
        }
#else
        var component = gameObject.GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }
#endif

        return component;
    }
    #endregion

    #region Animation

    // UniTask
    //public async static UniTaskVoid AnimationTestAsync(this Animator animator, Action callback, int layerIndex = 0)
    //{
    //    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layerIndex);
    //    await UniTask.WaitUntil(() => { return info.normalizedTime >= 1.0f && !animator.IsInTransition(0); });
    //    callback.Invoke();
    //}

    #endregion

}