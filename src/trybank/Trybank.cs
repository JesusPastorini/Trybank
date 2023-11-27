namespace Trybank.Lib;

public class TrybankLib
{
    public bool Logged;
    public int loggedUser;

    //0 -> Número da conta
    //1 -> Agência
    //2 -> Senha
    //3 -> Saldo
    public int[,] Bank;
    public int registeredAccounts;
    private int maxAccounts = 50;

    public TrybankLib()
    {
        loggedUser = -99;
        registeredAccounts = 0;
        Logged = false;
        Bank = new int[maxAccounts, 4];
    }

    // 1. Construa a funcionalidade de cadastrar novas contas
    // Função auxiliar para verificar se a conta já existe
    private bool AccountExists(int number, int agency)
    {
        for (int i = 0; i < registeredAccounts; i++)
        {
            if (Bank[i, 0] == number && Bank[i, 1] == agency)
            {
                return true;
            }
        }
        return false;
    }

    public void RegisterAccount(int number, int agency, int pass)
    {
        if (AccountExists(number, agency))
        {
            throw new ArgumentException("A conta já está sendo usada!");
        }

        // Verificar se há espaço disponível no array Bank
        if (registeredAccounts < maxAccounts)
        {
            // Armazenar os dados no array Bank na próxima posição disponível com saldo 0
            Bank[registeredAccounts, 0] = number;
            Bank[registeredAccounts, 1] = agency;
            Bank[registeredAccounts, 2] = pass;
            Bank[registeredAccounts, 3] = 0;
            registeredAccounts++;

            return;
        }
    }
    // Função auxiliar para encontrar a posição da pessoa usuária
    private int FindUserIndex(int number, int agency)
    {
        for (int i = 0; i < registeredAccounts; i++)
        {
            if (Bank[i, 0] == number && Bank[i, 1] == agency)
            {
                return i;
            }
        }
        return -1;
    }
    // 2. Construa a funcionalidade de fazer Login
    public void Login(int number, int agency, int pass)
    {
        if (Logged)
        {
            throw new AccessViolationException("Usuário já está logado");
        }

        int userIndex = FindUserIndex(number, agency);

        // Se encontrado e a senha for correta
        if (userIndex != -1 && Bank[userIndex, 2] == pass)
        {
            Logged = true;
            loggedUser = userIndex;
        }
        // Se encontrado, mas a senha for incorreta
        else if (userIndex != -1)
        {
            throw new ArgumentException("Senha incorreta");
        }
        // Se não encontrado
        else
        {
            throw new ArgumentException("Agência + Conta não encontrada");
        }
    }

    // 3. Construa a funcionalidade de fazer Logout
    public void Logout()
    {
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        Logged = false;
        loggedUser = -99;
    }

    // 4. Construa a funcionalidade de checar o saldo
    public int CheckBalance()
    {
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        return Bank[loggedUser, 3];
    }

    // 5. Construa a funcionalidade de depositar dinheiro
    public void Deposit(int value)
    {
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        Bank[loggedUser, 3] += value;
    }

    // 6. Construa a funcionalidade de sacar dinheiro
    public void Withdraw(int value)
    {
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado");
        }
        if (Bank[loggedUser, 3] >= value)
        {
            // Retirar o valor passado por parâmetro do saldo da pessoa usuária logada
            Bank[loggedUser, 3] -= value;
        }
        else
        {
            throw new InvalidOperationException("Saldo insuficiente");
        }
    }

    // 7. Construa a funcionalidade de transferir dinheiro entre contas
    public void Transfer(int destinationNumber, int destinationAgency, int value)
    {
        throw new NotImplementedException();
    }


}
