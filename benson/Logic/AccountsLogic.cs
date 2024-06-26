﻿using System;
using System.Collections.Generic;
using System.Threading;
public class AccountsLogic
{
    private List<AccountsModel> _accounts;

    static public AccountsModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        
        _accounts = DataAccess<AccountsModel>.LoadAll();
        
        
    }

    public bool UpdateList(AccountsModel acc)
    {
        int index = _accounts.FindIndex(s => s.EmailAddress == acc.EmailAddress);

        if (index != -1)
        {
            Console.WriteLine("An account with that email address already exists. Please try again.");
            Thread.Sleep(2000);
            UserRegister.Start();
            return false;
        }

        _accounts.Add(acc);
        DataAccess<AccountsModel>.WriteAll(_accounts);
        Console.WriteLine("Account created successfully!");
        return true;
    }

    public AccountsModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }

    public List<AccountsModel> GetAllAccounts(int role = -1)
    {
        return (role < 0) ? _accounts : _accounts.FindAll(acc => Convert.ToInt32(acc.Role) == role);
    }
}
