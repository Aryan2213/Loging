using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject loging;
    public GameObject create;
    public GameObject resetPassword;
    public GameObject passwordConfig;

    public InputField usernameL;
    public InputField passwordL;


    public InputField usernameC;
    public InputField passwordC;
    public InputField emailC;

    public InputField emailR;

    public InputField code;
    public InputField userNameConfig;
    public InputField updatePsassword;

    public string resetCode;

    private void Start()
    {
        Loging();
    }

    public void Loging()
    {
        loging.SetActive(true);
        create.SetActive(false);
        resetPassword.SetActive(false);
        passwordConfig.SetActive(false);


    }
    public void Create()
    {
        loging.SetActive(false);
        create.SetActive(true);
        resetPassword.SetActive(false);
        passwordConfig.SetActive(false);

    }
    public void ResetPasswordB()
    {
        loging.SetActive(false);
        create.SetActive(false);
        resetPassword.SetActive(true);
        passwordConfig.SetActive(false);

    }
    public void passwordConfing()
    {
        loging.SetActive(false);
        create.SetActive(false);
        resetPassword.SetActive(false);
        passwordConfig.SetActive(true);
    }


    public void SignInClicked()
    {

        var PHPResultString = DBContext.Login(usernameL.text, passwordL.text);
        Debug.Log("I Loging");
    }
    public void CreateUser()
    {
        var PHPResultString = DBContext.CreateUser(usernameC.text, passwordC.text, emailC.text);
        Debug.Log("I Create");
    }
    public void ResetPassword()
    {
        var usersUsername = DBContext.GetUsernameFromEmail(emailR.text);
        Debug.Log(usersUsername);

        resetCode = WebFunctions.Generate6CharResetCode();

        WebFunctions.SendResetEmail(emailR.text, usersUsername, resetCode);

        // Tkae them to the next screen

    }
    public void UpdatePassword()
    {

        if (resetCode == code.text)
        {
            var PHPResultString = DBContext.ResetPassword(userNameConfig.text, updatePsassword.text);
        }


    }

}

