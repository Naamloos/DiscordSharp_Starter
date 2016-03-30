using DiscordSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordSharp_Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            // First of all, a DiscordClient will be created, and the email and password will be defined.
            DiscordClient client = new DiscordClient();
            client.ClientPrivateInformation.Email = "Your Email";
            client.ClientPrivateInformation.Password = "Your password";

            // Then, we are going to set up our events before connecting to discord, to make sure nothing goes wrong.

            client.Connected += (sender, e) => // Client is connected to Discord
            {
                Console.WriteLine("Connected! User: " + e.user.Username);
                // If the bot is connected, this message will show.
                // Changes to client, like playing game should be called when the client is connected,
                // just to make sure nothing goes wrong.
                client.UpdateCurrentGame("Bot online!\nPress any key to close this window."); // This will display at "Playing: "
            };


            client.PrivateMessageReceived += (sender, e) => // Private message has been received
            {
                if (e.message == "help")
                {
                    e.author.SendMessage("This is a private message!");
                    // Because this is a private message, the bot should send a private message back
                    // A private message does NOT have a channel
                }
                if (e.message.StartsWith("join "))
                {
                    string inviteID = e.message.Substring(e.message.LastIndexOf('/') + 1);
                    // Thanks to LuigiFan (Developer of DiscordSharp) for this line of code!
                    client.AcceptInvite(inviteID);
                    e.author.SendMessage("Joined your discord server!");
                    Console.WriteLine("Got join request from " + inviteID);
                }
            };


            client.MessageReceived += (sender, e) => // Channel message has been received
            {
                if (e.message_text == "help")
                {
                    e.Channel.SendMessage("This is a public message!");
                    // Because this is a public message, 
                    // the bot should send a message to the channel the message was received.
                }
            };


            // Now, try to connect to Discord.
            try{ 
                // Make sure that IF something goes wrong, the user will be notified.
                // The SendLoginRequest should be called after the events are defined, to prevent issues.
                client.SendLoginRequest();
                client.Connect(); // Login request, and then connect using the discordclient i just made.
            }catch(Exception e){
                Console.WriteLine("Something went wrong!\n" + e.Message + "\nPress any key to close this window.");
            }

            // Done! your very own Discord bot is online!


            // Now to make sure the console doesnt close:
            Console.ReadKey(); // If the user presses a key, the bot will shut down.
        }

        public int stringnumber(string name, int min, int max)
        {
            // Bonus code: returns number based of bytes of string.
            // If something goes wrong, (eg: too long int) it return the min value.
            // This is fun for commands to "rate" a user or anything else, and make sure the same string returns the same number
            try
            {
                byte[] namebt = Encoding.UTF8.GetBytes(name);
                string namebtstring = namebt.ToString();
                int namebtint = Int32.Parse(namebtstring);
                Random rnd = new Random(namebtint);
                int returnint = rnd.Next(min, max);
                return returnint;
            }
            catch (Exception)
            {
                return min;
            }
        }
    }
}
