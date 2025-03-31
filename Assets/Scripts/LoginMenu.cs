using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    string usernameInputField;
    string passwordInputField;
    string passwordConfirmInputField;

    public void UpdateUsernameInputField(string enteredUsername)
    {
        usernameInputField = enteredUsername;
    }

    public void UpdatePasswordInputField(string enteredPassword)
    {
        passwordInputField = enteredPassword;
    }

    public void UpdatePasswordConfirmInputField(string enteredPasswordConfirm)
    {
        passwordConfirmInputField = enteredPasswordConfirm;
    }
    public void AttemptLogin()
    {
        Debug.Log(usernameInputField);
        Debug.Log(passwordInputField);
    }
}
