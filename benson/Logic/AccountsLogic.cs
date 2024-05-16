using System;
using System.Collections.Generic;

public class AccountsLogic
{
    private List<AccountModel> _accounts;

    static public AccountModel? CurrentAccount { get; private set; }
    string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));

    public AccountsLogic()
    {
        _accounts = DataAccess<AccountModel>.LoadAll(path);
    }

    public bool UpdateList(AccountModel acc)
    {
        int index = _accounts.FindIndex(s => s.EmailAddress == acc.EmailAddress);

        if (index != -1)
        {
            Console.WriteLine("An account with that email address already exists. Please try again.");
            UserRegister.Start();
            return false;
        }

        _accounts.Add(acc);
        DataAccess<AccountModel>.WriteAll(_accounts, path);
        Console.WriteLine("Account created successfully!");
        return true;
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

    public List<AccountModel> GetAllAccounts(int role = -1)
    {
        return (role < 0) ? _accounts : _accounts.FindAll(acc => Convert.ToInt32(acc.Role) == role);
    }
}
