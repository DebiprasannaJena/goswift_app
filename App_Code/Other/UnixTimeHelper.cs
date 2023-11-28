using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UnixTimeHelper
/// </summary>
public static class UnixTimeHelper
{   
    public static long ToUnixTime(this DateTime time)
    {
        var totalSeconds = (long)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        return totalSeconds;
    }
}