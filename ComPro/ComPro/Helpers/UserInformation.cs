using ComPro.Interfaces;
using ComPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Helpers
{
    public static class UserInformation
    {
        
        public static string UserName (this string Email)
        {
            IUtility _utility = new UtilityManager();
            ApplicationDbContext Data = new ApplicationDbContext();
            
            

            try
            {

                if (!_utility.CheckEmailAddressFormat(Email))
                {

                    var user = Data.Users.FirstOrDefault(x => x.Id == Email);
                    var user2 = Data.UserInfo.FirstOrDefault(x => x.Email == user.Email);
                    return user2.Name;
                }
                else
                {
                    var user = Data.UserInfo.FirstOrDefault(x=>x.Email==Email);
                      return user.Name;
                }
                

            }

            catch
            {
                return Email;

            }
        }

        public static string UserNameById(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                ApplicationDbContext Data = new ApplicationDbContext();


                var user = Data.Users.FirstOrDefault(x => x.Id == Id);
                var user2 = Data.UserInfo.FirstOrDefault(x => x.Email == user.Email);
                return user2.Name;
            }
            return string.Empty;
            
        }

        public static string UserEmailById(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                ApplicationDbContext Data = new ApplicationDbContext();
                             var user2 = Data.UserInfo.FirstOrDefault(x => x.UserId == Id);


                                return user2.Email;
                }
                catch
                {
                    return string.Empty;
                }
             
            }
            return string.Empty;

        }

    }
}