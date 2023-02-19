using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Febucci.UI;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UnityEvent GameEnd;
    
    [SerializeField] private GameObject _startGameButton;
    [SerializeField] private TMP_Text _dialogueText;
    [SerializeField] private GameObject[] _moveJumpTutorial;
    [SerializeField] private GameObject _player;
    
    private string _firstText="A little <color=orange>fox</color> embarks on a <wave><rainb>new adventure</rainb></wave>";
    private string _secondText="The <color=orange>fox</color> encounters an old <color=red>enemy</color>, <shake>a <color=red>wolf</color></shake>, <pend>who blocks its path</pend>";
    private string _thirdText = "In order to continue its adventure, the <color=orange>fox</color> brought an <color=red>end</color> to the <color=red>wolf</color>'s interference";
    private string _forthText = "Sometimes, in order to begin something <wave><color=orange>new</color></wave>, we must first bring an <shake><color=red>end</color></shake> to something else";

    private bool isGameStarted = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void StartGameButtonClicked()
    {
        _startGameButton.SetActive(false);
        StartCoroutine(StartGame());
        //CameraManager.Instance.ChangeCameraToMain();
        //StartCoroutine(ShowMoveJumpTutorial());
    }
    
    IEnumerator StartGame()
    {
        if (!isGameStarted)
        {
            isGameStarted = true;
        
            CameraManager.Instance.ChangeCameraToMain();
        
            yield return StartCoroutine(ShowDialogue(_firstText, 1.5f, 3.0f));
            CameraManager.Instance.ChangeCameraToWolf();
        
            yield return StartCoroutine(ShowDialogue(_secondText, 1.5f, 3.0f));
            CameraManager.Instance.ChangeCameraToMain();
        
            StartCoroutine(ShowMoveJumpTutorial());
        }

        yield return null;
    }
    
    IEnumerator ShowDialogue(string text,float delayToStart,float delayToDisappear)
    {
        yield return new WaitForSeconds(delayToStart);
        _dialogueText.text = text;
        //_dialogueText.gameObject.SetActive(true);
        _dialogueText.GetComponent<TextAnimatorPlayer>().StartShowingText();
        yield return new WaitForSeconds(delayToDisappear);
        _dialogueText.GetComponent<TextAnimatorPlayer>().StartDisappearingText();
        //yield return new WaitForSeconds(1.5f);
        //_dialogueText.gameObject.SetActive(false);
    }

    IEnumerator ShowMoveJumpTutorial()
    {
        _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _player.GetComponent<PlayerMovement>().enabled = true;
            
        foreach (var go in _moveJumpTutorial)
        {
            go.SetActive(true);
        }
        
        yield return new WaitForSeconds(5.0f);
        
        foreach (var go in _moveJumpTutorial)
        {
            go.SetActive(false);
        }
    }

    public IEnumerator ShowThirdText()
    {
        CameraManager.Instance.ChangeCameraToIce();
        yield return StartCoroutine(ShowDialogue(_thirdText, 3.5f, 4.0f));
        CameraManager.Instance.ChangeCameraToMain();
    }

    public IEnumerator ShowGameEnd()
    {
        GameEnd.Invoke();
        yield return StartCoroutine(ShowDialogue(_forthText, 3.5f, 6.0f));
        yield return StartCoroutine(ShowDialogue("Thanks for Playing our game!", 3.5f, 4.0f));
        
    }
}
