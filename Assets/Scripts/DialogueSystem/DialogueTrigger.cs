using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTrigger : EventTrigger
    {
        // [Tooltip("If zero, reading manual")]
        // public float dialogTime; 
        public Dialogue dialogue;

        public void DisplayDialogue()
        {
            Debug.Log("Starting dialogue");
            DialogueManager.Instance.StartDialogue(dialogue);
        }
    }
}