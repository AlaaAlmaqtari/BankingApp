﻿namespace App1.REST
{
    //this class shall be used to get and set users inicial information from the API REST OpenBank
    internal class Users
    {
        public string User { get; set; }

        public string Password { get; set; }

        public string Bank { get; set; }
    }
}