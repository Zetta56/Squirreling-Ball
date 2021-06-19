using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilLib.Scripts
{
    public static class Utils
    {
        public static string StringifyTime(float time) {
            int minutes = (int)time / 60;
            int seconds = (int)time % 60;
            int subseconds = (int)(time * 100 % 100);

            return $"{minutes.ToString("00")}:{seconds.ToString("00")}:{subseconds.ToString("00")}";
        }
    }
}
