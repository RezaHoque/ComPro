﻿using ComPro.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static ComPro.Models.Enums;

namespace ComPro.Interfaces
{
    public class UserProfileManager : IUserProfile
    {
        private ApplicationDbContext _data;
        private ApplicationUserManager _userManager;
        Email_Service_Model obj = new Email_Service_Model();
        public UserProfileManager(ApplicationUserManager userManager)
        {
           
            _data = new ApplicationDbContext();
            _userManager = userManager;
        }
        public UserProfileManager()
        {
            _data = new ApplicationDbContext();
        }
        ApplicationDbContext Data = new ApplicationDbContext();
        IUtility _utility = new UtilityManager();

        public void AddationalInfo( InternalRegisterViewModel model)
        {
            try
            {
                UserInfo UserInformation = new UserInfo();
                UserInformation.Name = model.Name;

                UserInformation.Address = model.Address;
                UserInformation.PostCode = model.PostCode;
                UserInformation.City = model.City;
                UserInformation.Phone = model.Phone;
                UserInformation.Gender = model.Gender;

                UserInformation.Photo = SetProfilePicture(model.Gender);

                UserInformation.BirthDate = null;
                var user = Data.Users.FirstOrDefault(x=>x.Email== model.Email);
                UserInformation.UserId = user.Id;


                UserInformation.CurrentJobTitle = "Not Specified";
                UserInformation.CompanyName = "Not Specified";
                UserInformation.Skills = "Not Specified";

                UserInformation.Email = model.Email;
               



                // Note: _data cant access data from database
                //_data.UserInfo.Add(UserInformation);
                //_data.SaveChanges();
                Data.UserInfo.Add(UserInformation);

                Data.SaveChanges();

                

            }
            catch
            {
                throw;
            }


        }

        public string CheckExternalUser(string providerkey)
        {

            try
            {
                var userLogins = Data.UserLogins.FirstOrDefault(x => x.ProviderKey == providerkey);
                var a = userLogins.UserId;
                var user = Data.Users.FirstOrDefault(x => x.Id == a);
                return user.Email;

            }
            catch
            {

                return null;
            }
        }


        public IEnumerable<UserInfo> AllUser()
        {
            try
            {
                var _UserInfo = Data.UserInfo;
                List<UserInfo> _userProfile = new List<UserInfo>();
                foreach (var x in _UserInfo)
                {
                    if ((GetUserRole(x.Email) != UserRole.NewUser.ToString()) && ((GetUserRole(x.Email) != UserRole.Administrator.ToString())))
                    {
                        _userProfile.Add(x);
                    }

                }

                return _userProfile;
            }

            catch
            {
                throw;
            }

        }


        public UserInfo DetailProfile(int Id)
        {
            try
            {

                 var res= Data.UserInfo.FirstOrDefault(x => x.Id == Id);
                return res;
            }

            catch
            {
                throw;
            }

        }

        public UserInfo EditUserProfile()
        {
            try
            {
                string Current_User_id = HttpContext.Current.User.Identity.GetUserId();
                var user2 = _data.Users.FirstOrDefault(x => x.Id == Current_User_id);
                return Data.UserInfo.FirstOrDefault(x => x.Email == user2.Email);
            }

            catch
            {
                throw;
            }

        }



        public bool PostEditUserProfile(UserInfo info)
        {

            try
            {
                string Current_User_id = HttpContext.Current.User.Identity.GetUserId();
                var user2 = _data.Users.FirstOrDefault(x => x.Id == Current_User_id);
                var User=Data.UserInfo.FirstOrDefault(x => x.Email == user2.Email);

                User.Name = info.Name;
                User.Address = info.Address;
                User.PostCode = info.PostCode;
                User.City = info.City;
                User.Phone = info.Phone;
                if(info.Photo != null)
                { User.Photo = info.Photo;}
                
                if(info.BirthDate != null)
                {User.BirthDate = info.BirthDate;
                  }
                
                User.CurrentJobTitle = info.CurrentJobTitle;
                User.CompanyName = info.CompanyName;
                User.Skills = info.Skills;
                User.Email = info.Email;
            
                //info.Gender = User.Gender;
                //Data.Entry(info).State = EntityState.Modified;
                Data.SaveChanges();
                return true;
            }

            catch
            {
                return false;
            }
        }

        public string DeleteUserProfile(int id)
        {
            try
            {
                var user = Data.UserInfo.FirstOrDefault(x => x.Id == id);
                var user2 = Data.Users.FirstOrDefault(y => y.Email == user.Email);
                                

                Data.UserInfo.Remove(user);
                Data.Users.Remove(user2);

                Data.SaveChanges();
                return Helpers.Constants.Delete;
            }
            catch
            {
                throw;
            }
        }

        public UserInfo UserDetail(string id)
        {
            var _user = _data.Users.FirstOrDefault(x => x.Id == id);

            UserInfo User = _data.UserInfo.FirstOrDefault(y => y.Email == _user.Email);

            return User;

        }

        public UserInfo CurrentUserDetail()
        {
            var email = HttpContext.Current.User.Identity.GetUserName();
            var result=  Data.UserInfo.FirstOrDefault(x => x.Email == email);

            return result;
        }

        public void SetUserRole(string email)
        {
            // Note : 'private readonly ApplicationDbContext _data ' cant access data from database !!!!
            //var store = new UserStore<ApplicationUser>(_data);
            // var user = _data.Users.FirstOrDefault(x => x.Email == email);

            var store = new UserStore<ApplicationUser>(Data);
            var manager = new UserManager<ApplicationUser>(store);

             
           
            var user = Data.Users.FirstOrDefault(x => x.Email == email);
            manager.AddToRole(user.Id, "NewUser");


        }

        public string GetUserRole(string email)
        {

            try
            {
               
                // Note : 'private readonly ApplicationDbContext _data ' cant access data from database !!!!
                var user = Data.Users.FirstOrDefault(x => x.Email == email);

                var UserRole = Data.UserRole.FirstOrDefault(x => x.UserId == user.Id);

                var Role = Data.Roles.FirstOrDefault(x => x.Id == UserRole.RoleId);

                return Role.Name;

            }
            catch
            {
               return  null;

            }




        }

     

        public bool CheckEmailvarification(string email)
        {
            try
            {
            var user = Data.Users.FirstOrDefault(x => x.Email == email);
            return user.EmailConfirmed;
            }

            catch
            {
                return false;
            }
            
        }

        public List<User_Approval_Model> NewUserforApproval()
        {
            try
            {
                List<UserInfo> user = _data.UserInfo.ToList();
                List<ApplicationUser> user2 = _data.Users.ToList();
                List<User_Approval_Model> NewUser = new List<User_Approval_Model>();

               
                foreach (var x in user2)
                {

                    foreach (var y in user)
                    {
                        if (GetUserRole(y.Email) == UserRole.NewUser.ToString())
                            if (x.Email == y.Email)
                            {
                                NewUser.Add(new User_Approval_Model()
                                {
                                    Id = y.Id,
                                    Name = y.Name,
                                    Photo = y.Photo,

                                });
                            }
                    }

                }
                return NewUser;
            }

            catch
            {
                throw;
            }

        }


        public bool ApproveNewUser(int  Id)
        {
            try
            {

                var UserInfo = Data.UserInfo.FirstOrDefault(x => x.Id == Id);
                UserInfo.ApprovalDate = DateTime.Now;
                Data.Entry(UserInfo).State = EntityState.Modified;

                var User = _data.Users.FirstOrDefault(x => x.Email == UserInfo.Email);

                var _UserRole = Data.UserRole.FirstOrDefault(x => x.UserId == User.Id);
                var Role = Data.Roles.FirstOrDefault(x => x.Name == UserRole.User.ToString());


                string UserId = _UserRole.UserId;
                string RoleId = Role.Id;

                Data.UserRole.Remove(_UserRole);
                Data.SaveChanges();

                _UserRole.UserId = UserId;
                _UserRole.RoleId = RoleId;

                Data.UserRole.Add(_UserRole);


                Data.SaveChanges();

               /* Email verification is turned off

                obj.ToEmail = UserInfo.Email;
                obj.EmailSubject = Helpers.Constants.Emailsubject;
                
                obj.EMailBody = Helpers.Constants.Emailbody + UserInfo.Email;



                var result = _utility.SendEmail(obj); */

                return true;

            }

            catch
            {
                return false;
                //throw;

            }


        }

        public UserInfo GetUser(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var usrProfile = _data.UserInfo.FirstOrDefault(x => x.Name == name);
                if (usrProfile != null)
                {
                    return usrProfile;
                }


            }
            return new UserInfo();
        }

        public string CheckLink(string email)
        {

            try
            {
                var user = Data.Users.FirstOrDefault(x=>x.Email==email);
                user.EmailConfirmed = true;

                Data.Entry(user).State = EntityState.Modified;
                Data.SaveChanges();

                return Helpers.Constants.Verified;

            }

            catch
            {

                return Helpers.Constants.NotVerified;

            }

        }


        public string SetProfilePicture(string data)
        {


            if (data == Gender.Male.ToString())
            {
                return "/Content/images/Profile/Male.png";
            }
            else
            {
                return "/Content/images/Profile/Female.png";
            }

        }

        public void ExternalAddationalInfo(UserInfo model)
        {
            try
            {
                UserInfo uInfo = new UserInfo
                {
                    Name = model.Name,
                    Email = model.Email,
                    CurrentJobTitle = model.CurrentJobTitle,
                    Gender = model.Gender,
                    Photo = SetProfilePicture(model.Gender),
                    ApprovalDate = DateTime.Now,
                    UserId = model.UserId,
                    Address = "Not Specified",
                    PostCode =  "Not Specified",
                    City = "Not Specified",
                    BirthDate= null,
                    CompanyName = "Not Specified",
                    Skills = "Not Specified",
                };





        Data.UserInfo.Add(uInfo);
                Data.SaveChanges();

            }
            catch
            {
                throw;
            }
        }


        public void ForgotPassword(string userId,string action, string link)
        {

            try
            {
                var User = Data.Users.FirstOrDefault(x=>x.Id== userId);

                obj.ToEmail = User.Email;
                obj.EmailSubject = action;
                
                obj.EMailBody = link;

                var result = _utility.SendEmail(obj);
            }

            catch
            {

                throw;
            }
        }

        public bool Accounttype()
        {
            try
            {

                var User = HttpContext.Current.User.Identity.GetUserId();
                
                return Data.UserLogins.Any(x=>x.UserId==User);

            }

            catch
            {
                return false;
            }


        }

        public string GetCode(string email)
        {
            try
            {

                var User = Data.Users.FirstOrDefault(x=>x.Email==email);
                return User.Id;
            }

            catch
            {
                throw;
            }
        }

    }
}