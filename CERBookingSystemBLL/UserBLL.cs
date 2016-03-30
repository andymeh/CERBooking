using CERBookingSystemDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERBookingSystemBLL
{
    public class UserBLL
    {
        public static bool UserNameExists(string emailAddress)
        {
            using (var db = new DALDataContext())
            {
                List<User> user = db.Users.Where(x => x.EmailAddress == emailAddress).ToList();

                if (user.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public static User getUser(string emailAddress)
        {
            using (var db = new DALDataContext())
            {
                User user = new User();
                if (emailAddress != null && emailAddress != "")
                {
                    user = db.Users.First(x => x.EmailAddress == emailAddress);
                }
                return user;
            }
        }
        public static void addNewUser(User newUser)
        {
            using(var db = new DALDataContext())
            {
                db.Users.InsertOnSubmit(newUser);
                db.SubmitChanges();
            }
        }
        public static bool isUserValid(String emailAddress, String password)
        {
            using (var db = new DALDataContext())
            {
                List<User> user = db.Users.Where(x => x.EmailAddress == emailAddress).ToList();

                if (user != null && user.Count != 0)
                {
                    String encodedPw = SHA1.Encode(password);
                    if (user.First().Password == encodedPw)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
    public class SHA1
    {
        public static string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}
