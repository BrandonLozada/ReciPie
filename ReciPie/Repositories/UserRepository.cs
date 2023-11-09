using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Essentials;
using ReciPie.Views;

namespace ReciPie
{
    public class UserRepository
    {
        string webAPIKey = "AIzaSyDXEeOSCi6TJOjC2UWE52v9RiL97VVOaz8";
        FirebaseAuthProvider authProvider;
        //Firebase.Auth.FirebaseAuth FirebaseAuth;

        public UserRepository()
        {
           authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }

        // TODO: Retornar "UserCredentials" de Firebase Auth.
        public async Task<FirebaseAuth> Register(string email,string name,string password)
        {
            var UserCredential = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, name);

            if (!string.IsNullOrEmpty(UserCredential.FirebaseToken))
            {
                return UserCredential;
            }
            else
            {
                return null;
            }
        }

        public async Task<FirebaseAuth> SignIn(string email,string password)
        {
            var UserCredential = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

            if (!string.IsNullOrEmpty(UserCredential.FirebaseToken))
            {
                return UserCredential;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool>ResetPassword(string email)
        {
            await authProvider.SendPasswordResetEmailAsync(email);
            return true;
        }

        public async Task<string>ChangePassword(string token,string password)
        {
            var auth= await authProvider.ChangeUserPassword(token, password);
            return auth.FirebaseToken;
        }
    }
}
