﻿using Lab.Class.Bank;
using Lab.Bank.Db;
using Lab.Bank.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Bank.Screens
{
    internal class UpdateClient : Screen
    {
        public static void Update()
        {
            string accountNumber = "";

            drawScreenHeader("Update Client");
            accountNumber = readOneInfo("Enter Client Account Number to Update: ").ToString();

            while (!IsClientExist(accountNumber))
                accountNumber = readOneInfo("Account Number " + accountNumber + " is Not found, Choose another one: ").ToString();

            BankClient client = findClient(accountNumber);
            PrintClient(client);


            if (confirmationMessage("Are you sure to Update Acc. " + accountNumber + " Y /N ?") == 'y')
            {
                List<object> clientsList = FileDbContext.convertFileDataToList(FileDbContext.ClientsDbConnectionString, FileDbContext.FileRowSeparator, FileDbContext.enConvertLineToObjetTypes.Client);

                foreach (BankClient bankClient in clientsList)
                {
                    if (bankClient.AccountNumber == client.AccountNumber)
                    {
                        client = readClientInfo(client.AccountNumber);

                        bankClient.PinCode = client.PinCode;
                        bankClient.FirstName = client.FirstName;
                        bankClient.LastName = client.LastName;
                        bankClient.Email = client.Email;
                        bankClient.Phone = client.Phone;
                        bankClient.AccountBalance = client.AccountBalance;

                        break;
                    }
                }

                if (FileDbContext.saveListToFile(clientsList, FileDbContext.ClientsDbConnectionString, false))
                {
                    Console.WriteLine();
                    Console.WriteLine("Client ({0}) Updated Successfully", accountNumber);
                    PrintClient(client);
                }
            }
        }
    }
}
