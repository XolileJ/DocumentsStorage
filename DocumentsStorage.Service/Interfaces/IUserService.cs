using DocumentsStorage.Service.ViewModels;
using System;
using System.Collections.Generic;

namespace DocumentsStorage.Service.Interfaces
{
    public interface IUserService : IDisposable
    {
        string Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
