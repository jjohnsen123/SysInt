using System;
using System.Messaging;

namespace MessageQueueUtilities
{
    public class MessageQueueClearer
    {
        public string _queuePath { get; set; }


        // Method to clear the queue
        public void ClearQueue(string _queuePath)
        {
            if (MessageQueue.Exists(_queuePath))
            {
                using (MessageQueue messageQueue = new MessageQueue(_queuePath))
                {
                    Console.WriteLine("Tømmer køen...");

                    try
                    {
                        while (true) // Fjern beskeder én efter én
                        {
                            try
                            {
                                Message message = messageQueue.Receive(new TimeSpan(0, 0, 1)); // Modtag og fjern besked med timeout
                                Console.WriteLine("Fjernet besked med Label: " + message.Label);
                            }
                            catch (MessageQueueException mqEx)
                            {
                                if (mqEx.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                                {
                                    Console.WriteLine("Ingen flere beskeder i køen.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"Fejl i køen: {mqEx.Message}");
                                    throw;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"En fejl opstod under behandlingen af køen: {ex.Message}");
                    }

                    Console.WriteLine("Køen er tømt.");
                }
            }
            else
            {
                Console.WriteLine("Message Queue eksisterer ikke.");
            }
        }
    }
}
