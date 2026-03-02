using UnityEditor;

[CustomEditor(typeof(BaseEnemyLogic))]
public class BaseEnemyLogicDrawer : Editor
{
    private Editor healthEditor;
    private bool showValues;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        BaseEnemyLogic bel = (BaseEnemyLogic)target;

        if (bel.health != null)
        {
            EditorGUILayout.Space(10);
            showValues = EditorGUILayout.Foldout(showValues, "Show Values");

            if (showValues)
            {
                if (healthEditor == null)
                    healthEditor = CreateEditor(bel.health);

                healthEditor.DrawDefaultInspector();
            }
        }
    }
}