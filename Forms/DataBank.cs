using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace Forms
{
    class DataBank
    {
        public static string Mail;
        public static NpgsqlConnection conn;
        public static NpgsqlCommand cmd;
        public static int i;
        #region Connect
        public static string Server = "localhost";
        public static string Port = "5432";
        public static string User_id = "postgres";
        public static string Password = "05052010";
        public static string DB = "LogPas";
        #endregion
        #region Mail
        public static string MyMail = "fgdhfghtyh@mail.ru"; // adiorhgjdfipg@mail.ru
        public static string MyPass = "wWBh0TimZGpSGXG0AEXn"; // Lf0gwk05upVB9N4wDKdt
        public static int MyPort = 465; // 465
        // fgdhfghtyh@mail.ru wWBh0TimZGpSGXG0AEXn (работало несколько раз)
        #endregion
    }
}
