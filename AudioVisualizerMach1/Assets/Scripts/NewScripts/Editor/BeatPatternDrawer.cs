using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Text;

[CustomPropertyDrawer(typeof(BeatPattern))]
public class BeatPatternDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
     if (property.propertyType == SerializedPropertyType.String)
        {
            string number = Regex.Replace(label.text, @"\D", "");
            GUI.color = Color.green;
            Rect labelRect = new Rect(position.x, position.y, 300, 20);
            GUI.Label(labelRect, number);

            //setting the length of the string
            BeatPattern bp = attribute as BeatPattern;
            if (property.stringValue.Length != bp.patternLength)
            {
                StringBuilder startString = new StringBuilder();
                for (int i = 0; i < bp.patternLength; i++)
                {
                    startString.Append('0');
                }
                property.stringValue = startString.ToString();
            }
            StringBuilder sb = new StringBuilder(property.stringValue);
            for (int i = 0; i < property.stringValue.Length; i++)
            {
                Rect buttonRect = new Rect(position.x + 21 + (11 * i), position.y, 10, 20);
                //coloring
                if(sb[i] == '1')
                {
                    GUI.color = Color.green;
                }
                else
                {
                    if(i % bp.beatInterval == 0)
                    {
                        GUI.color = Color.red;
                    }
                    else
                    {
                        GUI.color = Color.black;
                    }
                }
                if (GUI.Button(buttonRect, GUIContent.none))
                {
                    if (sb[i] == '0')
                    {
                        sb[i] = '1';
                        property.stringValue = sb.ToString();
                    }
                    else
                    {
                        sb[i] = '0';
                        property.stringValue = sb.ToString();
                    }
                }
            }
        }
        else
        {
            GUI.color = Color.green;
            Rect labelRect = new Rect(position.x, position.y, 20, 20);
            GUI.Label(labelRect, "This attribute only works with strings");
        }
        GUI.color = Color.white;
    }
}
