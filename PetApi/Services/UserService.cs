using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetApi.Classes;
using PetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetApi.Services
{
    public class UserService
    {
        private readonly ShopContextSql _context;
        public UserService(ShopContextSql context)
        {
            _context = context;

            _context.Database.EnsureCreated();
        }
        public IQueryable<User> GetAllUsers(QueryParameters queryParameters)
        {
            IQueryable<User> users = _context.Users;   

            users = users
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);

            return users;
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);           
            _context.SaveChanges();           
        }

        public User ModifyUser(int id, User user)
        {
            _context.Entry(user).State = EntityState.Modified;            
            _context.SaveChanges();
     
            if (_context.Users.Find(id) == null)
            {
                return null;
            }

            return user;
        }

        public User DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return user;
        }

        public List<User> DeleteMultipleUser(int[] ids)
        {
            var users = new List<User>();
            foreach (var id in ids)
            {
                var user = _context.Users.Find(id);

                if (user == null)
                {
                    return null;
                }

                users.Add(user);
            }

            _context.Users.RemoveRange(users);
            _context.SaveChanges();

            return users;

        }
    }
}
