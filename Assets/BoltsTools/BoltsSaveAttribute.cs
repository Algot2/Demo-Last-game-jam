using UnityEngine;

public enum SavedVariableType {Any, Float, Int, String, Bool, Class}

public class BoltsSaveAttribute : PropertyAttribute
{
    public SavedVariableType filterType;

    public BoltsSaveAttribute(SavedVariableType filterType = SavedVariableType.Any)
    {
        this.filterType = filterType;
    }
}
