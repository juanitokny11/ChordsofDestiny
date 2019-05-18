using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausaButtons : MonoBehaviour
{
    public GameObject MenuButton;
    public GameObject ResumeButton;
    public GameObject OptionsButton;
    public GameObject ExitButton;
    public GameObject YesButton;
    public GameObject NoButton;
    public GameObject Yes2Button;
    public GameObject No2Button;
    public GameObject OptionsText;
    public Sprite OptionsimgEng;
    public Sprite OptionsimgEsp;
    public GameObject PauseText;
    public Sprite PauseimgEng;
    public Sprite PauseimgEsp;
    public Sprite ExitEng;
    public Sprite ExitEsp;
    public Sprite ExitPEng;
    public Sprite ExitPEsp;
    public Sprite OptionsEng;
    public Sprite OptionsEsp;
    public Sprite OptionsPEng;
    public Sprite OptionsPEsp;
    public Sprite MenuEng;
    public Sprite MenuEsp;
    public Sprite MenuPEng;
    public Sprite MenuPEsp;
    public Sprite ResumeEng;
    public Sprite ResumeEsp;
    public Sprite ResumePEng;
    public Sprite ResumePEsp;
    public Sprite YesEng;
    public Sprite YesEsp;
    public Sprite YesPEng;
    public Sprite YesPEsp;
    public Sprite No;
    public Sprite NoP;
    public SpriteState MenuState = new SpriteState();
    public SpriteState OptionsState = new SpriteState();
    public SpriteState ExitState = new SpriteState();
    public SpriteState YesState = new SpriteState();
    public SpriteState NoState = new SpriteState();
    public SpriteState Yes2State = new SpriteState();
    public SpriteState No2State = new SpriteState();
    public SpriteState ResumeState = new SpriteState();


    private void Update()
    {
       ChangeButtons();
    }
    private void ChangeButtons()
    {
        if (LanguageManager.langData.currentLanguage == LangData.Languages.English)
        {
            OptionsText.GetComponent<Image>().sprite = OptionsimgEng;
            PauseText.GetComponent<Image>().sprite = PauseimgEng;

            MenuButton.GetComponent<Image>().sprite = MenuPEng;
            MenuState.highlightedSprite = MenuEng;
            MenuState.pressedSprite = MenuEng;
            MenuButton.GetComponent<Button>().spriteState = MenuState;
            /*if (MenuButton.GetComponent<Button>().spriteState.pressedSprite || MenuButton.GetComponent<Button>().spriteState.highlightedSprite)
            {
                MenuButton.GetComponent<Image>().sprite = MenuPEng;
            }*/
            OptionsButton.GetComponent<Image>().sprite = OptionsPEng;
            OptionsState.highlightedSprite = OptionsEng;
            OptionsState.pressedSprite = OptionsEng;
            OptionsButton.GetComponent<Button>().spriteState = OptionsState;
            /*if (OptionsButton.GetComponent<Button>().spriteState.pressedSprite || OptionsButton.GetComponent<Button>().spriteState.highlightedSprite)
            {
                OptionsButton.GetComponent<Image>().sprite = OptionsPEng;
            }*/
            ExitButton.GetComponent<Image>().sprite = ExitPEng;
            ExitState.highlightedSprite = ExitEng;
            ExitState.pressedSprite = ExitEng;
            ExitButton.GetComponent<Button>().spriteState = ExitState;
            /*if (ExitButton.GetComponent<Button>().spriteState.pressedSprite || ExitButton.GetComponent<Button>().spriteState.highlightedSprite)
            {
                ExitButton.GetComponent<Image>().sprite = ExitPEng;
            }*/
            YesButton.GetComponent<Image>().sprite = YesPEng;
            YesState.highlightedSprite = YesEng;
            YesState.pressedSprite = YesEng;
            YesButton.GetComponent<Button>().spriteState = YesState;
            /*if (YesButton.GetComponent<Button>().spriteState.pressedSprite || YesButton.GetComponent<Button>().spriteState.highlightedSprite)
            {
                YesButton.GetComponent<Image>().sprite = YesPEng;
            }*/
            NoButton.GetComponent<Image>().sprite = NoP;
            NoState.highlightedSprite = No;
            NoState.pressedSprite = No;
            NoButton.GetComponent<Button>().spriteState = NoState;
            /*if (NoButton.GetComponent<Button>().spriteState.pressedSprite || NoButton.GetComponent<Button>().spriteState.highlightedSprite)
            {
                NoButton.GetComponent<Image>().sprite = NoP;
            }*/
            Yes2Button.GetComponent<Image>().sprite = YesPEng;
            Yes2State.highlightedSprite = YesEng;
            Yes2State.pressedSprite = YesEng;
            Yes2Button.GetComponent<Button>().spriteState = Yes2State;
            /*if (Yes2Button.GetComponent<Button>().spriteState.pressedSprite || Yes2Button.GetComponent<Button>().spriteState.highlightedSprite)
            {
                Yes2Button.GetComponent<Image>().sprite = YesPEng;
            }*/
            No2Button.GetComponent<Image>().sprite = NoP;
            No2State.highlightedSprite = No;
            No2State.pressedSprite = No;
            No2Button.GetComponent<Button>().spriteState = No2State;
            /*if (No2Button.GetComponent<Button>().spriteState.pressedSprite || No2Button.GetComponent<Button>().spriteState.highlightedSprite)
            {
                No2Button.GetComponent<Image>().sprite = NoP;
            }*/
            ResumeButton.GetComponent<Image>().sprite = ResumePEng;
            ResumeState.highlightedSprite = ResumeEng;
            ResumeState.pressedSprite = ResumeEng;
            ResumeButton.GetComponent<Button>().spriteState = ResumeState;
            /*if (ResumeButton.GetComponent<Button>().spriteState.pressedSprite || ResumeButton.GetComponent<Button>().spriteState.highlightedSprite)
            {
                ResumeButton.GetComponent<Image>().sprite = ResumePEng;
            }*/
        }
        else if (LanguageManager.langData.currentLanguage == LangData.Languages.Spanish)
        {
            OptionsText.GetComponent<Image>().sprite = OptionsimgEsp;
            PauseText.GetComponent<Image>().sprite = PauseimgEsp;
            MenuButton.GetComponent<Image>().sprite = MenuPEsp;
            MenuState.highlightedSprite = MenuEsp;
            MenuState.pressedSprite = MenuEsp;
            MenuButton.GetComponent<Button>().spriteState = MenuState;
            /* if (MenuButton.GetComponent<Button>().spriteState.pressedSprite || MenuButton.GetComponent<Button>().spriteState.highlightedSprite)
             {
                 MenuButton.GetComponent<Image>().sprite = MenuPEsp;
             }*/
            OptionsButton.GetComponent<Image>().sprite = OptionsPEsp;
            OptionsState.highlightedSprite = OptionsEsp;
            OptionsState.pressedSprite = OptionsEsp;
            OptionsButton.GetComponent<Button>().spriteState = OptionsState;
            /* if (OptionsButton.GetComponent<Button>().spriteState.pressedSprite || OptionsButton.GetComponent<Button>().spriteState.highlightedSprite)
             {
                 OptionsButton.GetComponent<Image>().sprite = OptionsPEsp;
             }*/
            ExitButton.GetComponent<Image>().sprite = ExitPEsp;
            ExitState.highlightedSprite = ExitEsp;
            ExitState.pressedSprite = ExitEsp;
            ExitButton.GetComponent<Button>().spriteState = ExitState;
            /*if (ExitButton.GetComponent<Button>().spriteState.pressedSprite || ExitButton.GetComponent<Button>().spriteState.highlightedSprite)
            {
                ExitButton.GetComponent<Image>().sprite = ExitPEsp;
            }*/
            YesButton.GetComponent<Image>().sprite = YesPEsp;
            YesState.highlightedSprite = YesEsp;
            YesState.pressedSprite = YesEsp;
            YesButton.GetComponent<Button>().spriteState = YesState;
            /*if (YesButton.GetComponent<Button>().spriteState.pressedSprite || YesButton.GetComponent<Button>().spriteState.highlightedSprite)
            {
                YesButton.GetComponent<Image>().sprite = YesPEsp;
            }*/
            NoButton.GetComponent<Image>().sprite = NoP;
            NoState.highlightedSprite = No;
            NoState.pressedSprite = No;
            NoButton.GetComponent<Button>().spriteState = NoState;
            /* if (NoButton.GetComponent<Button>().spriteState.pressedSprite || NoButton.GetComponent<Button>().spriteState.highlightedSprite)
             {
                 NoButton.GetComponent<Image>().sprite = NoP;
             }*/
            Yes2Button.GetComponent<Image>().sprite = YesPEsp;
            Yes2State.highlightedSprite = YesEsp;
            Yes2State.pressedSprite = YesEsp;
            Yes2Button.GetComponent<Button>().spriteState = Yes2State;
            /*if (Yes2Button.GetComponent<Button>().spriteState.pressedSprite || Yes2Button.GetComponent<Button>().spriteState.highlightedSprite)
            {
                Yes2Button.GetComponent<Image>().sprite = YesPEsp;
            }*/
            No2Button.GetComponent<Image>().sprite = NoP;
            No2State.highlightedSprite = No;
            No2State.pressedSprite = No;
            No2Button.GetComponent<Button>().spriteState = No2State;
            /*if (No2Button.GetComponent<Button>().spriteState.pressedSprite || No2Button.GetComponent<Button>().spriteState.highlightedSprite)
            {
                No2Button.GetComponent<Image>().sprite = NoP;
            }*/
            ResumeButton.GetComponent<Image>().sprite = ResumePEsp; 
            ResumeState.highlightedSprite = ResumeEsp;
            ResumeState.pressedSprite = ResumeEsp;
            ResumeButton.GetComponent<Button>().spriteState = ResumeState;
            /*if (ResumeButton.GetComponent<Button>().spriteState.pressedSprite || ResumeButton.GetComponent<Button>().spriteState.highlightedSprite)
            {
                ResumeButton.GetComponent<Image>().sprite = ResumePEsp;
            }*/
        }
    }
}
