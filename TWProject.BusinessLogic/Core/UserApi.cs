using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TWProject.BusinessLogic.DB;
using TWProject.Domain.Entities.User;
using Helpers;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TWProject.Domain.Enums;
using System.Data.Entity;

namespace TWProject.BusinessLogic.Core
{
    public class UserApi
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserApi(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        internal ULoginResp UserLoginLogic(ULoginData data)
        {
            UDBTable user;
            using (var context = new CarRentalContext())
            {
                user = context.User.FirstOrDefault(u => u.Name == data.Credential && u.Password == data.Password);
                if (user == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "Wrong email or password" };
                }
            }

            var apiCookie = CreateCookie(user.Name);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("X-KEY",apiCookie.Value, apiCookie.Options);

            return new ULoginResp { Status = true };
        }

        internal (string Value, CookieOptions Options) CreateCookie(string loginCredential)
        {
            var cookieValue = CookieGenerator.Create(loginCredential);
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(60),
                HttpOnly = true
            };

            using (var db = new SessionContext())
            {
                var session = db.Sessions.FirstOrDefault(s => s.Username == loginCredential);

                if (session != null)
                {
                    session.CookieString = cookieValue;
                    session.ExpireTime = DateTime.Now.AddMinutes(60);
                    session.Username = loginCredential;
                    db.Entry(session).State = EntityState.Modified;
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = cookieValue,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                }

                db.SaveChanges();
            }

            return (cookieValue, options);
        }

        /*internal (string Value, CookieOptions Options) CreateApiCookie(string loginCredential)
        {
            var apiCookie = Cookie(loginCredential);
            return (apiCookie.Value, new CookieOptions { Expires = DateTime.Now.AddMinutes(60), HttpOnly = true });
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                var session = db.Sessions.FirstOrDefault(s => s.Username == loginCredential);

                if (session != null)
                {
                    session.CookieString = apiCookie.Value;
                    session.ExpireTime = DateTime.Now.AddMinutes(60);
                    session.Username = loginCredential;
                    db.Entry(session).State = EntityState.Modified;
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                }

                db.SaveChanges();
            }

            return apiCookie;
        }*/

        internal UserMini UserCookie(string cookie)
        {
            Session session;
            UDBTable currentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new CarRentalContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    currentUser = db.User.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    currentUser = db.User.FirstOrDefault(u => u.Name == session.Username);
                }
            }

            if (currentUser == null) return null;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UDBTable, UserMini>());
            var mapper = config.CreateMapper();
            var userMini = mapper.Map<UserMini>(currentUser);
            return userMini;
        }

        internal URegisterResp UserRegistrationLogic(URegisterData data)
        {
            using (var context = new CarRentalContext())
            {
                var result = context.User.FirstOrDefault(u => u.Name == data.Username);
                if (result != null)
                {
                    return new URegisterResp { Status = false, StatusMsg = "User with this name already exists" };
                }

                var newUser = new UDBTable
                {
                    Name = data.Username,
                    Password = data.Password,
                    Email = data.Email
                };
                context.User.Add(newUser);
                context.SaveChanges();
                return new URegisterResp { Status = true };
            }
        }
    }
}
