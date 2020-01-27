using Lomztein.BruteForceAttackSequel;
using Lomztein.BruteForceAttackSequel.Modifications;
using Lomztein.BruteForceAttackSequel.Modifications.Modifiers;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (Modifier))]
public class ModifierEditor : Editor
{
    private SerializedProperty _eventInfo;
    private Type[] eventModTypes;

    private bool eventArrayFoldout;
    private int typeSelectionIndex = -1;

    public void OnEnable()
    {
        eventModTypes = typeof(EventMod).Assembly.GetTypes().Where (x => x.IsSubclassOf (typeof (EventMod)) && !x.IsAbstract).ToArray ();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Modifier modifier = target as Modifier;
        modifier.SetName(EditorGUILayout.TextField("Name", modifier.Name));
        modifier.SetDesc(EditorGUILayout.TextField("Description", modifier.Description));
        SerializedProperty statMods = serializedObject.FindProperty("_statMods");
        EditorGUILayout.PropertyField(statMods, true);
        OnEventModsGUI();
        serializedObject.ApplyModifiedProperties();
    }

    private void AddNewEventMod (SerializedProperty array, string typeName)
    {
        array.InsertArrayElementAtIndex(array.arraySize);
        SerializedProperty info = array.GetArrayElementAtIndex(array.arraySize - 1);
        info.NextVisible(true);
        info.stringValue = typeName;
        info.Reset();
        serializedObject.ApplyModifiedPropertiesWithoutUndo();
    }

    private void OnEventModsGUI()
    {
        SerializedProperty eventModArray = serializedObject.FindProperty("_eventMods");

        eventArrayFoldout = EditorGUILayout.Foldout(eventArrayFoldout, "Event Mods");
        if (eventArrayFoldout)
        {
            typeSelectionIndex = EditorGUILayout.Popup("Add Mod", typeSelectionIndex, eventModTypes.Select(x => x.Name).ToArray());
            if (typeSelectionIndex >= 0)
            {
                int selection = typeSelectionIndex;
                typeSelectionIndex = -1;
                AddNewEventMod(eventModArray, eventModTypes[selection].FullName);
            }
            for (int i = 0; i < eventModArray.arraySize; i++)
            {
                SerializedProperty eventInfo = eventModArray.GetArrayElementAtIndex(i);
                OnEventInfoGUI(eventModArray, i, eventInfo);
            }
        }
    }

    private void OnEventInfoGUI(SerializedProperty array, int index, SerializedProperty info)
    {
        info.NextVisible(true);

        Type type = eventModTypes.FirstOrDefault(x => x.FullName == info.stringValue);
        if (type != null)
        {
            EditorGUILayout.LabelField(type.Name);

            FieldInfo[] modPropertyFields = type.GetFields().Where(x => x.IsDefined(typeof(ExposedPropertyAttribute))).ToArray();

            info.NextVisible(true);
            info.arraySize = modPropertyFields.Length;

            for (int i = 0; i < modPropertyFields.Length; i++)
            {
                SerializedProperty property = info.GetArrayElementAtIndex(i);
                property.stringValue = OnGenericSerializedProperty(property, modPropertyFields[i].FieldType, modPropertyFields[i].Name);
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Invalid type: " + info.stringValue + ". Please remove.");
        }

        if (GUILayout.Button("Remove"))
        {
            array.DeleteArrayElementAtIndex(index);
        }
        info.Reset();
    }

    private string OnGenericSerializedProperty (SerializedProperty property, Type type, string label)
    {
        string value = property.stringValue;

        try
        {
            if (type.IsEquivalentTo(typeof(string)) || type.IsEquivalentTo(typeof(Resource)))
            {
                value = EditorGUILayout.TextField(label, value);
            }
            if (type.IsEnum)
            {
                value = EditorGUILayout.EnumPopup(label, StringConversion.FromString(value, type) as Enum).ToString();
            }
            if (type.IsEquivalentTo(typeof(int)))
            {
                value = EditorGUILayout.IntField(label, int.Parse(value)).ToString();
            }
            if (type.IsEquivalentTo(typeof(float)) || type.IsEquivalentTo (typeof (ModProperty)))
            {
                value = EditorGUILayout.FloatField(label, float.Parse(value)).ToString();
            }
            if (type.IsEquivalentTo (typeof (bool)))
            {
                value = EditorGUILayout.Toggle(label, bool.Parse(value)).ToString();
            }
        } catch (Exception)
        {
            value = Activator.CreateInstance(type).ToString();
        }

        return value;
    }
}
