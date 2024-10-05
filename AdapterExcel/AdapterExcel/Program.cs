using System;
using System.Threading;
using Experimental.System.Messaging;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace AdapterExcel
{
    class Program
    {
        private static MessageQueue queue;
        private static Excel.Application oXL;
        private static Excel._Workbook oWB;
        private static Excel._Worksheet oSheet;
        private static int row = 2;

        static void Main(string[] args)
        {
            // Start Excel og opret et nyt ark
            oXL = new Excel.Application();
            oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            oSheet.Cells[1, 1] = "Flight Number";
            oSheet.Cells[1, 2] = "ETA";

            oXL.Visible = true;
            oXL.UserControl = true;
    
            string queuePath = @".\private$\FlightQueue";

            if (!MessageQueue.Exists(queuePath))
            {
                MessageQueue.Create(queuePath);
            }
            queue = new MessageQueue(queuePath);

            queue.Send( "SAS1234;12:45" );
            queue.Send( "SAS3321;14:30");

            queue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnMessageReceived);

            queue.BeginReceive();

            Console.WriteLine("Listening for messages");
            while (true) { }
        }


        private static void OnMessageReceived(object sender, ReceiveCompletedEventArgs e)
        {
            MessageQueue mq = (MessageQueue)sender;
            Message receivedMsg = mq.EndReceive(e.AsyncResult);
            try
            {
                string[] messageParts = receivedMsg.Body.ToString().Split(';');

 
                Console.WriteLine($"Message received: " + string.Join(", ", messageParts));

                bool success = false;
                while (!success)
                {
                    try
                    {
                        oSheet.Cells[row, 1] = messageParts[0];
                        oSheet.Cells[row, 2] = messageParts[1]; 
                        row++; 
                        success = true;
                    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        Console.WriteLine($"Excel is busy. Retrying in 100 ms... {ex.Message}");
                        Thread.Sleep(100);
                    }
                }

                queue.BeginReceive();
            }
            catch (MessageQueueException mqe)
            {
                Console.WriteLine($"Message Queue Exception: {mqe.Message}");
            }
            catch (InvalidOperationException ioe)
            {
            
                Console.WriteLine($"InvalidOperationException: {ioe.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace); 
            }
        }
    }
}
