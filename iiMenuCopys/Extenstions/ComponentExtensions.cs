using UnityEngine;

public static class ComponentExtensions
{
    /// <summary>
    /// Allows AddComponent from any Component (including Text).
    /// </summary>
    public static T AddComponent<T>(this Component component) where T : Component
    {
        return component.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// Allows GetComponent from any Component (explicit wrapper).
    /// </summary>
    public static T GetComponentEx<T>(this Component component) where T : Component
    {
        return component.gameObject.GetComponent<T>();
    }

    /// <summary>
    /// Gets component or adds it if missing.
    /// </summary>
    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        T existing = component.gameObject.GetComponent<T>();
        if (existing == null)
            existing = component.gameObject.AddComponent<T>();

        return existing;
    }
}