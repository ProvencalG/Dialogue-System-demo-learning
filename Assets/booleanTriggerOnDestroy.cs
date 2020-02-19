using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class booleanTriggerOnDestroy : MonoBehaviour
{
    /// The variable to increment.
    /// </summary>
    [Tooltip("Change stats of this Dialogue System boolean.")]
    [VariablePopup]
    public string variable = string.Empty;

    enum States {True, False, Contrary};

    [SerializeField] States setTo;

    protected string actualVariableName
    {
        get { return string.IsNullOrEmpty(variable) ? DialogueActor.GetPersistentDataName(transform) : variable; }
    }


    // Update is called once per frame
    void OnDestroy()
    {
        if (!Application.isPlaying) return;
        if (DialogueManager.Instance == null || DialogueManager.DatabaseManager == null || DialogueManager.MasterDatabase == null) return;
        bool oldValue = DialogueLua.GetVariable(actualVariableName).asBool;

        bool newValue;

        switch (setTo)
        {
            case States.True:
                newValue = true;
                break;
            case States.False:
                newValue = false;
                break;
            case States.Contrary:
                newValue = !oldValue;
                break;
            default:
                newValue = oldValue;
                break;
        }

        DialogueLua.SetVariable(actualVariableName, newValue);
        DialogueManager.SendUpdateTracker();
    }
}
