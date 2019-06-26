using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BeatPos))]
public class BeatPosDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            BeatPos bp = attribute as BeatPos;
            for (int i = 0; i < bp.patternLength; i++)
            {
                Rect labelRect = new Rect(position.x + 21 + (11 * i), position.y, 10, 20);
                GUI.color = Color.green;
                if (i == property.intValue)
                {
                    GUI.Label(labelRect, "x");
                }
                else
                {
                    GUI.Label(labelRect, "-");
                }
            }
            GUI.color = Color.white;
        }
        else
        {
          
        }
    }
}
