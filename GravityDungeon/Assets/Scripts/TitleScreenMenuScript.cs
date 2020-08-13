using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenMenuScript : MonoBehaviour
{
    public GameObject titleScreenStart, levelSelect, instructions, credits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("PetraPuzzleTest1");
    }

    public void LevelSelectButton()
    {

    }

    public void InstructionsButton()
    {
        titleScreenStart.SetActive(false);
        instructions.SetActive(true);
    }

    public void CreditsButton()
    {

    }

    public void BackButton()
    {
        titleScreenStart.SetActive(true);
        //levelSelect.SetActive(false);
        instructions.SetActive(false);
        //credits.SetActive(false);
    }
}
