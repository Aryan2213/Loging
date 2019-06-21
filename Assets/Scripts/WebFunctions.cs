using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;


    public static class WebFunctions
    {

        public const string ResetEmailSenderName = "DJ3D";
        private const string ResetEmailAddress = "arui.13.22@gmail.com";
        private const string ResetEmailPassword = "ohmygodrick!";

        // ------------------------------------------------- //

        private static System.Random random = new System.Random();

        public static string Generate6CharResetCode()
        {
            const string chars = "AzByCxDwEvFuGtHsIrJqKpLoMnNmOlPkQjRiShTgUfVeWdXcYbZa1234567890";

            MatchEvaluator RandomChar = delegate (Match m) {
                return chars[random.Next(chars.Length)].ToString();
            };

            return Regex.Replace("XXXXXX", "X", RandomChar);
        }

        // ------------------------------------------------- //

        private static SmtpClient smtpClient;

        public static void SendResetEmail(string recipientAddress, string recipientUsername, string resetCode)
        {
            // Some vars to start
            MailMessage mail = new MailMessage();

            // Hard coding this for the assignment.
            MailAddress ourEmail = new MailAddress(ResetEmailAddress, ResetEmailSenderName);

            // Write the email
            mail.From = ourEmail;
            mail.To.Add(recipientAddress);
            mail.Subject = "Password Reset for " + recipientUsername;
            mail.Body = string.Format("Forgot your password? No problem!.\nCopy the following code and paste it into the Password Recovery screen in the game.\n\n{0}", resetCode);

            // Setup an email client
            if (smtpClient == null)
            {
                smtpClient = new SmtpClient("smtp.gmail.com") {
                    Port = 25,
                    Credentials = new NetworkCredential(ResetEmailAddress, ResetEmailPassword) as ICredentialsByHost,
                    EnableSsl = true,
                };

                // Setup certificate validation callback
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors) { return true; };

            }


            //// Setup send completed callback
            //smtpClient.SendCompleted += callback;

            // Send
            smtpClient.Send(mail);


            Debug.Log(string.Format("Successfully sent recovery email to {0}", recipientAddress));

        }

    }

