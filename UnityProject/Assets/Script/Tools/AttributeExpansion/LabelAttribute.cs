using UnityEngine;
using System;

[System.AttributeUsage(AttributeTargets.Field)]
public sealed class LabelAttribute : PropertyAttribute
{
    // Attribute used to make a float or int variable in a script be restricted to a specific range.
    public LabelAttribute()
    {

    }
}

