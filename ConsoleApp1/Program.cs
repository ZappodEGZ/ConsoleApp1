using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static Dictionary<string, List<KeyValuePair<string, int>>> messageData = new();

    static void Main()
    {
        LoadMessageData("Input.txt");

        Console.WriteLine("Introdu numele expeditorului si destinatarului pentru a afla cate mesaje au fost trimise.");

        while (true)
        {
            Console.Write("\nExpeditor: ");
            string sender = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(sender))
                break;

            Console.Write("Destinatar: ");
            string receiver = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(receiver))
                break;

            QueryMessages(sender, receiver);
        }
    }

    static void LoadMessageData(string filePath)
    {
        foreach (var line in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(':', 2);
            var sender = parts[0].Trim();
            var receiverEntries = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);

            var receiverList = new List<KeyValuePair<string, int>>();

            foreach (var entry in receiverEntries)
            {
                var pair = entry.Trim().Split('-');
                var receiver = pair[0].Trim();
                var count = int.Parse(pair[1]);

                receiverList.Add(new KeyValuePair<string, int>(receiver, count));
            }

            messageData[sender] = receiverList;
        }
    }

    static void QueryMessages(string sender, string receiver)
    {
        if (!messageData.ContainsKey(sender))
        {
            Console.WriteLine($"{sender} nu a fost gasit in date.");
            return;
        }

        var receiverList = messageData[sender];
        int count = 0;

        foreach (var pair in receiverList)
        {
            if (pair.Key == receiver)
            {
                count = pair.Value;
                break;
            }
        }

        if (count > 0)
        {
            string form = count == 1 ? "mesaj" : "mesaje";
            Console.WriteLine($"{sender} a trimis {count} {form} către {receiver}.");
        }
        else
        {
            Console.WriteLine($"{sender} nu a trimis niciun mesaj către {receiver}.");
        }
    }
}
