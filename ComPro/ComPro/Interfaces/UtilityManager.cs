using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComPro.Models;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Data.Entity;
using static ComPro.Models.Enums;
using System.Text.RegularExpressions;
using System.Drawing;

namespace ComPro.Interfaces
{
    public class UtilityManager :IUtility
    {
        ApplicationDbContext _data = new ApplicationDbContext();


        public bool CheckEmailAddressFormat(string Email)
        {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(Email);
                    return addr.Address == Email;
                }
                catch
                {
                    return false;
                }
            
        }

        public void ConfirmEmai(string email)
        {
           var user = _data.Users.FirstOrDefault(x=>x.Email==email);
            user.EmailConfirmed = true;

            _data.Entry(user).State = EntityState.Modified;
            _data.SaveChanges();



        }

        public string SendEmail(Email_Service_Model obj)
        {
            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  

                //WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpServer = Helpers.Constants.SmtpServer; 

                //gmail port to send emails 

                //WebMail.SmtpPort = 587;
                WebMail.SmtpPort = Int32.Parse(Helpers.Constants.SmtpPort);

                
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application 

                //WebMail.UserName = "pori468@gmail.com";
                WebMail.UserName = System.Configuration.ConfigurationManager.AppSettings["UserName"];


                //WebMail.Password = "asmani6633";
                WebMail.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                //Sender email address.  
                //WebMail.From = "pori468@gmail.com";
                WebMail.From = System.Configuration.ConfigurationManager.AppSettings["From"];

                //Send email  
                WebMail.Send(to: obj.ToEmail, subject: obj.EmailSubject, body: obj.EMailBody, cc: obj.EmailCC, bcc: obj.EmailBCC, isBodyHtml: true);
                //ViewBag.Status = "Email Sent Successfully.";

                //return "Email Sent Successfully.";
                return Helpers.Constants.Send;
            }
            catch
            {
                return Helpers.Constants.Fail;
            }



        }


    }
}
