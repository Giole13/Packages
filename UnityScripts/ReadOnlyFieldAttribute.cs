using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;



[System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class ReadOnlyFieldAttribute : PropertyAttribute
{

}

// 출처 : https://dev.to/jayjeckel/unity-tips-properties-and-the-inspector-1goo
// [ReadOnlyField] 형태의 에트리뷰트로 사용이 가능하다, 읽기전용 에트리뷰터로 인스펙터에서 값 수정이 불가능하게 만들어 준다.

[UsedImplicitly, CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
public class ReadOnlyFieldAttributeDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        => EditorGUI.GetPropertyHeight(property, label, true);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}