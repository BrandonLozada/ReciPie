using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ReciPie.Models;
using Firebase.Database.Query;

namespace ReciPie
{
    public class UserInfoRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://reci-pie-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("reci-pie.appspot.com");

        public async Task<bool> AddUserInformation(string id, AddUserInformation myInfo)
        {
            var data = await firebaseClient.Child("users").Child(id).PostAsync(JsonConvert.SerializeObject(myInfo));
            //mDatabase.child("2").setValue(data);

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AddUserInformation_2(string id, AddUserInformation myInfo)
        {
            var data = await firebaseClient.Child("users").PostAsync(JsonConvert.SerializeObject(myInfo));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<UserInformation> GetById(string id)
        {
            return (await firebaseClient.Child("users" + "/" + id).OnceSingleAsync<UserInformation>());
        }

        public async Task<bool> Update(string id, UserInformation myInfo)
        {
            await firebaseClient.Child("users" + "/" + id).PutAsync(JsonConvert.SerializeObject(myInfo));
            return true;
        }
    }
}
