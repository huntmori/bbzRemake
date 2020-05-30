using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace API.VO
{
    public class LoginRequestVO
    {
        public string account_name;
        public string password;

        public static explicit operator UnityEngine.Object(LoginRequestVO v)
        {
            throw new NotImplementedException();
        }
    }
}

