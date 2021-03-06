using Microsoft.AspNetCore.Http;
using NewEmploymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewEmploymentSystem.Utilities
{
    public static class Utility
    {
        public static string findNextPagesSequence(string currentLevel)
        {
            string NextLevel = null;
            switch (currentLevel)
            { 
                case "Two":
                    NextLevel = "Three";
                    break; 
                case "Three":
                    NextLevel = "Four";
                    break;
                case "Four":
                    NextLevel = "Five";
                    break;
                case "Five":
                    NextLevel = "Six";
                    break;
                case "Six":
                    NextLevel = "Seven";
                    break;
                case "Seven":
                    NextLevel = "Eight";
                    break;
                case "Eight":
                    NextLevel = "Nine";
                    break;
                case "Nine":
                    NextLevel = "Ten";
                    break;
                case "Ten":
                    NextLevel = "Eleven";
                    break;
                case "Eleven":
                    NextLevel = "Twelve";
                    break;
                case "Twelve":
                    NextLevel = "Thirteen";
                    break;
                case "Thirteen":
                    NextLevel = "Fourteen";
                    break;
                case "Fourteen":
                    NextLevel = "Fifteen";
                    break;
                case "Fifteen":
                    NextLevel = "Sixteen";
                    break;
                case "Sixteen":
                    NextLevel = "Seventeen";
                    break;
                default:
                    NextLevel = currentLevel;
                    break;
            }
            return NextLevel;
        }

        public static TblPageTimeLog logTime(string uid, string enterDate, string pageLevel)
        {
            TblPageTimeLog timelog = new TblPageTimeLog()
            {
                UserId = uid,
                StartTime = DateTime.Parse(enterDate),
                EndTime = DateTime.Now,
                PageLevel = pageLevel
            };

            return timelog;
        }

        public static int leveltoNumber(string level)
        {
            switch (level)
            {
                case "One":
                    return 1;
                case "Two":
                    return 2;
                case "Three":
                    return 3;
                case "Four":
                    return 4;
                case "Five":
                    return 5;
                case "Six":
                    return 6;
                case "Seven":
                    return 7;
                case "Eight":
                    return 8;
                case "Nine":
                    return 9;
                case "Ten":
                    return 10;
                case "Eleven":
                    return 11;
                case "Twelve":
                    return 12;
                case "Thirteen":
                    return 13;
                case "Fourteen":
                    return 14;
                case "Fifteen":
                    return 15;
                case "Sixteen":
                    return 16;
                case "Seventeen":
                    return 17;
                default:
                    return 0;
            }
        }

        public static string toEnglishNumber(this string input)
        {
            string EnglishNumbers = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    EnglishNumbers += char.GetNumericValue(input, i);
                }
                else
                {
                    EnglishNumbers += input[i].ToString();
                }
            }
            return EnglishNumbers;
        }

    }
}
