using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        private Queue<string> _sentences;

        public TextMeshProUGUI dialogueText;
        // public TextMeshProUGUI nameText;
        // private string _dialogueName = "";

        [Tooltip("Appearing letters by frame")]
        public float textSpeed = 1f;

        public float delayTime = 2f;

        public bool typingText = false;

        [SerializeField] private string defaultText = "Explore the ship";

        // public bool preventChanges;
        [SerializeField] private Image dialoguePanel;
        private bool preventChanges;

        protected override void Awake()
        {
            base.Awake();
            _sentences = new Queue<string>();
        }

        private void Start()
        {
            HideDialogue();
            // dialogueText = UIManager.Instance.dialogText;
            // nameText = UIManager.Instance.dialogName;
        }

        // private IEnumerator DisplayNext(float time)
        // {
        //     while (_sentences.Count > 0)
        //     {
        //         DisplayNextSentence();
        //         yield return new WaitForSeconds(time);
        //     }
        //
        //     EndDialogue();
        // }


        public void StartDialogue(Dialogue dialogue)
        {
            // nameText.text = dialogue.name;
            // _dialogueName = dialogue.name;
            // if (_dialogueName != "")
            // {
            //     _dialogueName = dialogue.name + ": ";
            // }
            ShowDialogue();
            _sentences.Clear();
            foreach (string sentence in dialogue.sentences)
            {
                _sentences.Enqueue(sentence);
            }

            _sentences.Enqueue("");
            DisplayNextSentence(true);
        }

        private void DisplayNextSentence(bool firstCall)
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = _sentences.Dequeue();
            StopAllCoroutines();
            // StopCoroutine($"TypeSentence");
            StartCoroutine(firstCall ? TypeSentence(sentence) : DelayBetweenSentences(delayTime, sentence));
        }

        IEnumerator TypeSentence(string sentence)
        {
            // typingText = true;
            dialogueText.text = "";
            // int counter = 0;
            foreach (char letter in sentence)
            {
                dialogueText.text += letter;
                // counter++;
                yield return new WaitForSeconds(1 / textSpeed);
                // if (counter >= textSpeed)
                // {
                //     counter = 0;
                // }
            }

            DisplayNextSentence(false);
            // typingText = false;
        }

        IEnumerator DelayBetweenSentences(float time, string sentence)
        {
            yield return new WaitForSeconds(time);
            StartCoroutine(TypeSentence(sentence));
        }

        private void EndDialogue()
        {
            dialogueText.text = "";
            //Player.Instance.interactable = null;
            HideDialogue();
        }

        private void ShowDialogue()
        {
            dialoguePanel.enabled = true;
        }

        private void HideDialogue()
        {
            dialoguePanel.enabled = false;
        }

        public void HandleReading(Dialogue dialogue)
        {
            if (_sentences.Count > 0)
            {
                DisplayNextSentence(true);
            }
            else
            {
                StartDialogue(dialogue);
            }
        }

        public void ClearSentences()
        {
            _sentences.Clear();
        }

        public void ShowTextSeconds(string s, float time)
        {
            StartCoroutine(SetDialogueTextWithTime(s, time));
        }

        private IEnumerator SetDialogueTextWithTime(string s, float time)
        {
            preventChanges = true;
            dialogueText.text = s;
            yield return new WaitForSeconds(time);
            preventChanges = false;
        }

        public void DisplayTextSeconds(string text, float time)
        {
            StartCoroutine(SetDialogueTextWithTime(text, time));
        }
    }
}