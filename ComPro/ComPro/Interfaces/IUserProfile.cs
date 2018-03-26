﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComPro.Models;

namespace ComPro.Interfaces
{
    public interface IUserProfile
    {
        void AddationalInfo(InternalRegisterViewModel model);
        void ExternalAddationalInfo(UserInfo model);
        void SetUserRole(string email);
        string GetUserRole(string email);
        bool CheckEmailvarification(string email);
        List<User_Approval_Model> NewUserforApproval();
       // void ApproveNewUser(string Email);
        UserInfo UserDetail(string id);
        //UserProfileModel GetUser(string name);
        bool ApproveNewUser(int ID);
        UserInfo CurrentUserDetail();
        string CheckExternalUser(string providerkey);
        
        

        IEnumerable<UserInfo> AllUser();

        UserInfo DetailProfile(int Id);
        UserInfo EditUserProfile();
        bool PostEditUserProfile(UserInfo info);
        string DeleteUserProfile(int id);

        string CheckLink(string email);
        string SetProfilePicture(string Gender);
    }
}
