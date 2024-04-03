using System;
using System.Collections.Generic;

class AccountsLogic
{
    private List<AccountModel> _accounts;

    static public AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }

    public void UpdateList(AccountModel acc)
    {
        int index = _accounts.FindIndex(s => s.EmailAddress == acc.EmailAddress);

        if (index != -1)
        {
            Console.WriteLine("An account with that email address already exists. Please try again.");
            UserRegister.Start();
            return;
        }

        _accounts.Add(acc);
        AccountsAccess.WriteAll(_accounts);
        Console.WriteLine("Account created successfully!");
    }

    public AccountModel CheckLogin(string email, string password)
    {
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }
}
