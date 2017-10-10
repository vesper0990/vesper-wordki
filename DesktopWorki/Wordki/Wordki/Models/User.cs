﻿using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Repository.Models.Enums;
using Wordki.Helpers;
using Wordki.Helpers.JsonConverters;

namespace Wordki.Models
{
    [Serializable]
    public class User
    {

        #region Properties
        public virtual string Name { get; set; }

        public virtual string Password { get; set; }

        [JsonProperty("LocalId")]
        public virtual long UserId { get; set; }

        public virtual bool IsLogin { get; set; }

        public virtual bool IsRegister { get; set; }

        [JsonProperty("lastLoginDate", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime LastLoginTime { get; set; }

        public virtual DateTime DownloadTime { get; set; }

        [DefaultValue(TranslationDirection.FromFirst)]
        [JsonProperty("translationDirection", NullValueHandling = NullValueHandling.Ignore)]
        public virtual TranslationDirection TranslationDirection { get; set; }

        [JsonConverter(typeof(StringToBoolConverter))]
        [DefaultValue(true)]
        public virtual bool AllWords { get; set; }

        [DefaultValue(0)]
        [JsonProperty("timeOut", NullValueHandling = NullValueHandling.Ignore)]
        public virtual int Timeout { get; set; }

        public virtual string ApiKey { get; set; }

        #endregion

        /// <summary>
        /// Prywatny konstruktor
        /// </summary>
        public User()
        {
            UserId = 0;
            Name = "";
            Password = "";
            IsLogin = false;
            IsRegister = false;
            LastLoginTime = new DateTime();
            DownloadTime = new DateTime(1990, 9, 24);
            TranslationDirection = TranslationDirection.FromSecond;
            AllWords = false;
            Timeout = 0;
            ApiKey = "";
        }

        /// <summary>
        /// Zwraca sciezke do katalogu uzytkownika
        /// </summary>
        /// <returns>Sciezka do katalogu uzytkownika</returns>
        public virtual string GetDirectory()
        {
            if (Name == null)
                return null;
            string lPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.DataDictionary, Name);
            if (!Directory.Exists(lPath))
                Directory.CreateDirectory(lPath);
            return Path.Combine(Directory.GetCurrentDirectory(), Constants.DataDictionary, Name);
        }

        public virtual string GetStringFromObject()
        {
            StringBuilder lBuilder = new StringBuilder();
            lBuilder
              .Append("UserId: ").Append(UserId).Append(";")
              .Append("Name: ").Append(Name).Append(";")
              .Append("Password: ").Append(Password).Append(";")
              .Append("IsRegister: ").Append(IsRegister);
            return lBuilder.ToString();
        }
    }
}
