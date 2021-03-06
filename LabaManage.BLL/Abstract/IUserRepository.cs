﻿using System.Collections.Generic;
using LabaManage.Models.Models;

namespace LabaManage.BLL.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<RoleModel> Roles { get; }

        IEnumerable<UserModel> Users { get; }

        IEnumerable<UserModel> GetUserList(int page, int pageSize);

        UserModel GetUserByName(string name);

        UserModel GetUserById(int id);

        UserModel UserDelete(int id);

        int GetUsersCount();

        IEnumerable<UserModel> GetUsersInRole(string roleName);

        void UserUpdate(UserModel user);

        bool AreUsersInRole(int id);

        bool UserPasswordValidate(UserModel user, string password);

        void UserPasswordSet(UserModel user, string password);

        string[] GetRolesForUser(string username);

        bool IsUserInRole(string username, string roleName);

        RoleModel GetFirstRole();

        RoleModel GetRoleById(int id);

        RoleModel GetRoleByName(string name);

        RoleModel RoleDelete(int id);

        void RoleUpdate(RoleModel role);
    }
}
