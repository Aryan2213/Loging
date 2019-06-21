using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public static class DBContext
{
    private const string baseURL = "http://localhost/SQLMy/";

    // ------------------------------------------------- //

    public static string CreateUser(string username, string password, string email)
    {
        // Link to PHP
        string url = baseURL + "InsertUser.php";

        //Info to send to the POST variables in PHP
        WWWForm insertUserForm = new WWWForm();
        insertUserForm.AddField("usernamePost", username);
        insertUserForm.AddField("passwordPost", password);
        insertUserForm.AddField("emailPost", email);

        // Execute
        return DoPHP(url, insertUserForm);
    }

    // ------------------------------------------------- //

    public static string Login(string username, string password)
    {
        // Link to PHP
        string url = baseURL + "login.php";

        //Info to send to the POST variables in PHP
        WWWForm loginForm = new WWWForm();
        loginForm.AddField("usernamePost", username);
        loginForm.AddField("passwordPost", password);

        // Execute
        return DoPHP(url, loginForm);
    }

    // ------------------------------------------------- //

    public static string GetUsernameFromEmail(string email)
    {
        // Link to PHP
        string url = baseURL + "CheckUser.php";

        //Info to send to the POST variables in PHP
        WWWForm form = new WWWForm();
        form.AddField("emailPost", email);

        // Execute
        // Should be the username associated with the given email address.
        return DoPHP(url, form).Trim();

    }

    // ------------------------------------------------- //

    public static string ResetPassword(string username, string newPassword)
    {
        // Link to PHP
        string url = baseURL + "UpdatePassword.php";

        //Info to send to the POST variables in PHP
        WWWForm passwordResetForm = new WWWForm();
        passwordResetForm.AddField("usernamePost", username);
        passwordResetForm.AddField("passwordPost", newPassword);

        // Execute
        return DoPHP(url, passwordResetForm);

    }

    // ------------------------------------------------- //

    private static string DoPHP(string url, WWWForm postForm)
    {

        // Send info to PHP
        WWW www = new WWW(url, postForm);

        // HORRIBLE. In production I would do this on a seperate thread
        // cause it blocks the game loop, and set up a callback. But
        // this works for testing so I'll let it slide.
        while (!www.isDone)
            Thread.Sleep(1);

        if (www.error != null)
            Debug.Log("PHP Error: \"" + www.error + "\"");


        Debug.Log("PHP Response: \"" + www.text + "\"");
        return www.text;

    }

}

