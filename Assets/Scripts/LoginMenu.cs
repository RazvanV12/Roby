using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    private string usernameInputField;
    private string passwordInputField;
    private string oldPasswordInputField;
    private string newPasswordInputField;
    private string passwordConfirmInputField;

    [SerializeField] private GameObject usernameInput;
    [SerializeField] private GameObject passwordInput;
    [SerializeField] private GameObject confirmPasswordInput;
    [SerializeField] private GameObject createAccountButton;
    [SerializeField] private GameObject returnFromRegisterButton;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject registerButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject usernameInputLogin;
    [SerializeField] private GameObject passwordInputLogin;
    [SerializeField] private GameObject validateCredentialsButton;
    [SerializeField] private GameObject returnFromLoginButton;
    [SerializeField] private GameObject changePasswordMenuButton;
    [SerializeField] private GameObject usernameResetPassInput;
    [SerializeField] private GameObject oldPasswordInput;
    [SerializeField] private GameObject newPasswordInput;
    [SerializeField] private GameObject changePasswordInputButton;
    [SerializeField] private GameObject returnFromChangePasswordButton;

    public void OpenRegisterMenu()
    {
        usernameInput.SetActive(true);
        passwordInput.SetActive(true);
        confirmPasswordInput.SetActive(true);
        createAccountButton.SetActive(true);
        returnFromRegisterButton.SetActive(true);
    }

    public void CloseRegisterMenu()
    {
        usernameInput.SetActive(false);
        passwordInput.SetActive(false);
        confirmPasswordInput.SetActive(false);
        createAccountButton.SetActive(false);
        returnFromRegisterButton.SetActive(false);
    }
    public void CloseStartMenu()
    {
        loginButton.SetActive(false);
        changePasswordMenuButton.SetActive(false);
        registerButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void OpenStartMenu()
    {
        loginButton.SetActive(true);
        changePasswordMenuButton.SetActive(true);
        registerButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void OpenLoginMenu()
    {
        usernameInputLogin.SetActive(true);
        passwordInputLogin.SetActive(true);
        validateCredentialsButton.SetActive(true);
        returnFromLoginButton.SetActive(true);
    }

    public void CloseLoginMenu()
    {
        usernameInputLogin.SetActive(false);
        passwordInputLogin.SetActive(false);
        validateCredentialsButton.SetActive(false);
        returnFromLoginButton.SetActive(false);
    }
    
    public void OpenChangePasswordMenu()
    {
        usernameResetPassInput.SetActive(true);
        oldPasswordInput.SetActive(true);
        newPasswordInput.SetActive(true);
        changePasswordInputButton.SetActive(true);
        returnFromChangePasswordButton.SetActive(true);
    }
    
    public void CloseChangePasswordMenu()
    {
        usernameResetPassInput.SetActive(false);
        oldPasswordInput.SetActive(false);
        newPasswordInput.SetActive(false);
        changePasswordInputButton.SetActive(false);
        returnFromChangePasswordButton.SetActive(false);
    }
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
    public void UpdateOldPasswordInputField(string enteredOldPassword)
    {
        oldPasswordInputField = enteredOldPassword;
    }
    public void UpdateNewPasswordInputField(string enteredNewPassword)
    {
        newPasswordInputField = enteredNewPassword;
    }
    public void AttemptLogin()
    {
        if (string.IsNullOrEmpty(usernameInputField) || string.IsNullOrEmpty(passwordInputField))
        {
            Debug.Log("Username or password is empty");
            return;
        }
        if (DbRepository.AreCredentialsValid(usernameInputField, passwordInputField))
        {
            Debug.Log("Login successful");
            UserSession.ClearSession();
            DbRepository.CreateSession(usernameInputField);
            SceneManager.LoadScene("MainMenu");
            // Load main menu and update UI for logged-in user
        }
        else
        {
            Debug.Log("Invalid username or password");
        }
    }

    public void AttemptRegister()
    {
        if (string.IsNullOrEmpty(usernameInputField) || string.IsNullOrEmpty(passwordInputField))
        {
            Debug.Log("Username or password is empty");
            return;
        }
        if(DbRepository.UsernameExists(usernameInputField))
        {
            Debug.Log("Username already exists");
            return;
        }
        if(!PasswordMatch(passwordInputField, passwordConfirmInputField))
        {
            Debug.Log("Passwords do not match");
            return;
        }

        DbRepository.AddUserToDatabase(usernameInputField, DbRepository.HashPassword(passwordInputField));
        CloseRegisterMenu();
        RestoreRegisterInputFields();
        OpenLoginMenu();
    }

    public void AttemptChangePassword()
    {
        if (string.IsNullOrEmpty(usernameInputField) || string.IsNullOrEmpty(oldPasswordInputField) || 
            string.IsNullOrEmpty(newPasswordInputField))
        {
            Debug.Log("Username, old password or new password is empty");
            return;
        }
        if (DbRepository.UsernameExists(usernameInputField) && DbRepository.AreCredentialsValid(usernameInputField, oldPasswordInputField))
        {
            DbRepository.UpdatePasswordInDatabase(usernameInputField, DbRepository.HashPassword(newPasswordInputField));
            Debug.Log("Password changed successfully");
            CloseChangePasswordMenu();
            RestoreChangePasswordInputFields();
            OpenLoginMenu();
        }
        else
        {
            Debug.Log("Invalid username or password");
        }
    }

    public void RestoreRegisterInputFields()
    {
        usernameInput.GetComponent<TMP_InputField>().text = "";
        passwordInput.GetComponent<TMP_InputField>().text = "";
        confirmPasswordInput.GetComponent<TMP_InputField>().text = "";
    }

    public void RestoreLoginInputFields()
    {
        usernameInputLogin.GetComponent<TMP_InputField>().text = "";
        passwordInputLogin.GetComponent<TMP_InputField>().text = "";
    }
    
    public void RestoreChangePasswordInputFields()
    {
        usernameResetPassInput.GetComponent<TMP_InputField>().text = "";
        oldPasswordInput.GetComponent<TMP_InputField>().text = "";
        newPasswordInput.GetComponent<TMP_InputField>().text = "";
    }

    private bool PasswordMatch(string password, string confirmPassword)
    {
        return password == confirmPassword;
    }
    
    public void QuitGame()
    {
        Debug.Log("Game closed");
    }
}
