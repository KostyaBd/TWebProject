using System;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TWProject.BusinessLogic.DB;
using TWProject.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TWProject.Domain.Enums;
using Helpers;

namespace TWProject.BusinessLogic.Core
{
    public class UserApi
    {
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

            var apiCookie = Cookie(user.Name);
            HttpContext.Current.Response.Cookies.Add(apiCookie);

            return new ULoginResp
            {
                Status = true,
                StatusMsg = "Login successful",
                Role = user.level // Make sure this property is correctly set in ULoginResp
            };
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Database.SetInitializer<CarRentalContext>(new CreateDatabaseIfNotExists<CarRentalContext>());

                var session = db.Sessions.FirstOrDefault(s => s.Username == loginCredential);

                if (session != null)
                {
                    session.CookieString = apiCookie.Value;
                    session.ExpireTime = ValidateDateTime(DateTime.Now.AddMinutes(60));
                    session.Username = loginCredential;
                    db.Entry(session).State = EntityState.Modified;
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = ValidateDateTime(DateTime.Now.AddMinutes(60))
                    });
                }

                db.SaveChanges();
            }

            return apiCookie;
        }

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

        public ULoginResp UserRegistrationLogic(URegisterData data)
        {
            using (var db = new CarRentalContext())
            {
                var user = db.User.FirstOrDefault(u => u.Email == data.Email);
                if (user != null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "User already exists" };
                }

                user = new UDBTable
                {
                    Name = data.Username,
                    Password = data.Password,
                    Email = data.Email,
                    level = URoles.User, 
                    RegistrationDate = ValidateDateTime(DateTime.Now),
                    DateReceived = ValidateDateTime(DateTime.Now),
                    DateReturned = ValidateDateTime(DateTime.Now.AddDays(7))
                };

                db.User.Add(user);
                db.SaveChanges();
            }

            return new ULoginResp { Status = true, StatusMsg = "User registered successfully." };
        }

        private DateTime ValidateDateTime(DateTime dateTime)
        {
            DateTime minSqlDateTime = new DateTime(1753, 1, 1);
            DateTime maxSqlDateTime = new DateTime(9999, 12, 31);

            if (dateTime < minSqlDateTime)
            {
                return minSqlDateTime;
            }
            else if (dateTime > maxSqlDateTime)
            {
                return maxSqlDateTime;
            }
            else
            {
                return dateTime;
            }
        }
    }
}
