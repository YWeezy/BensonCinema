using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class AccountsLogic
{
    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        //rewrite this checks to use email instead of id

        int index = _accounts.FindIndex(s => s.EmailAddress == acc.EmailAddress);

        if (index != -1)
        {
            Console.WriteLine("An account with that email address already exists. Please try again.");
            UserRegister.Start();
            return;
        }


        if (index != -1)
        {
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
            Console.WriteLine("Account created successfully!");
            Utils.userIsLoggedIn = true;


        }
        AccountsAccess.WriteAll(_accounts);

    }

    public AccountModel GetById(string id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }
}




