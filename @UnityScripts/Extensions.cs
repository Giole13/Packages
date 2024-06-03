using UnityEngine;

public static partial class Extensions
{
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





}