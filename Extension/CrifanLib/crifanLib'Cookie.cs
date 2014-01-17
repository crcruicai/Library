/*********************************************************
 * 开发人员：TopC
 * 创建时间：2014/1/11 11:37:13
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Net;
using System.Text.RegularExpressions;

public partial class CrifanLib
{
    #region Cookie 处理

    /// <summary>
    /// 从Url中提取主机Host
    /// <para>例如:https://skydrive.live.com/ 提取为:skydrive.live.com</para>
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string ExtractHost(string url)
    {
        string domain = "";
        if ((url != "") && (url.Contains("/")))
        {
            string[] splited = url.Split('/');
            domain = splited[2];
        }
        return domain;
    }


    /// <summary>
    /// 从Url中提取域Domain
    /// <para>例如:https://skydrive.live.com/ 提取为:".live.com"</para>
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string ExtractDomain(string url)
    {
        string host = "";
        string domain = "";
        host = ExtractHost(url);
        if (host.Contains("."))
        {
            if (Regex.IsMatch(host, @"\w\.\w"))
            {
                //like: "fiverr.com"
                domain = host;
            }
            else
            {
                domain = host.Substring(host.IndexOf('.'));
            }
        }
        return domain;
    }


    /// <summary>
    /// 从Url中提取域Domain的URL
    /// <para>例如:http://answers.yahoo.com/question/index?qid=20130323071141AA8PffP</para>
    /// <para>提取为:http://answers.yahoo.com</para>
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public string GetDomainUrl(string url)
    {
        string domainUrl = "";

        Regex urlRx = new Regex(@"((https)|(http)|(ftp))://[\w\-\.]+");
        Match foundUrl = urlRx.Match(url);
        if (foundUrl.Success)
        {
            //int slashIndex = foundUrl.Index + foundUrl.Length;
            domainUrl = url.Substring(0, foundUrl.Length);
        }
        else
        {
            domainUrl = "";
        }

        return domainUrl;
    }

    //add recognized cookie field: expires/domain/path/secure/httponly/version, into cookie
    /// <summary>
    /// 添加字段到Cookie.
    /// </summary>
    /// <param name="ck">Cookie对象</param>
    /// <param name="pairInfo"></param>
    /// <returns></returns>
    public static bool AddFieldToCookie(ref Cookie ck, PairItem pairInfo)
    {
        bool added = false;
        if (pairInfo.Key != "")
        {
            string lowerKey = pairInfo.Key.ToLower();
            switch (lowerKey)
            {
                case "expires":
                    DateTime expireDatetime;
                    bool parseDatetimeOk = false;
                    parseDatetimeOk = DateTime.TryParse(pairInfo.Value, out expireDatetime);
                    if (parseDatetimeOk)
                    {
                        // note: here coverted to local time: GMT +8
                        ck.Expires = expireDatetime;

                        //update expired filed
                        if (DateTime.Now.Ticks > ck.Expires.Ticks)
                        {
                            ck.Expired = true;
                        }

                        added = true;
                    }
                    break;
                case "domain":
                    ck.Domain = pairInfo.Value;
                    added = true;
                    break;
                case "secure":
                    ck.Secure = true;
                    added = true;
                    break;
                case "path":
                    ck.Path = pairInfo.Value;
                    added = true;
                    break;
                case "httponly":
                    ck.HttpOnly = true;
                    added = true;
                    break;
                case "version":
                    int versionValue;
                    if (int.TryParse(pairInfo.Value, out versionValue))
                    {
                        ck.Version = versionValue;
                        added = true;
                    }
                    break;
                default:
                    break;
            }
        }

        return added;
    } //addFieldToCookie

    /// <summary>
    /// 判断字符串是否是有效的cookie的某一项
    /// </summary>
    /// <param name="cookieKey"></param>
    /// <returns></returns>
    public bool IsValidCookieField(string cookieKey)
    {
        return _GCookieFieldList.Contains(cookieKey.ToLower());
    }

    //cookie field example:
    //WLSRDAuth=FAAaARQL3KgEDBNbW84gMYrDN0fBab7xkQNmAAAEgAAACN7OQIVEO14E2ADnX8vEiz8fTuV7bRXem4Yeg/DI6wTk5vXZbi2SEOHjt%2BbfDJMZGybHQm4NADcA9Qj/tBZOJ/ASo5d9w3c1bTlU1jKzcm2wecJ5JMJvdmTCj4J0oy1oyxbMPzTc0iVhmDoyClU1dgaaVQ15oF6LTQZBrA0EXdBxq6Mu%2BUgYYB9DJDkSM/yFBXb2bXRTRgNJ1lruDtyWe%2Bm21bzKWS/zFtTQEE56bIvn5ITesFu4U8XaFkCP/FYLiHj6gpHW2j0t%2BvvxWUKt3jAnWY1Tt6sXhuSx6CFVDH4EYEEUALuqyxbQo2ugNwDkP9V5O%2B5FAyCf; path=/; domain=.livefilestore.com;  HttpOnly;,
    //WLSRDSecAuth=FAAaARQL3KgEDBNbW84gMYrDN0fBab7xkQNmAAAEgAAACJFcaqD2IuX42ACdjP23wgEz1qyyxDz0kC15HBQRXH6KrXszRGFjDyUmrC91Zz%2BgXPFhyTzOCgQNBVfvpfCPtSccxJHDIxy47Hq8Cr6RGUeXSpipLSIFHumjX5%2BvcJWkqxDEczrmBsdGnUcbz4zZ8kP2ELwAKSvUteey9iHytzZ5Ko12G72%2Bbk3BXYdnNJi8Nccr0we97N78V0bfehKnUoDI%2BK310KIZq9J35DgfNdkl12oYX5LMIBzdiTLwN1%2Bx9DgsYmmgxPbcuZPe/7y7dlb00jNNd8p/rKtG4KLLT4w3EZkUAOcUwGF746qfzngDlOvXWVvZjGzA; path=/; domain=.livefilestore.com;  HttpOnly; secure;,
    //RPSShare=1; path=/;,
    //ANON=A=DE389D4D076BF47BCAE4DC05FFFFFFFF&E=c44&W=1; path=/; domain=.livefilestore.com;,
    //NAP=V=1.9&E=bea&C=VTwb1vAsVjCeLWrDuow-jCNgP5eS75JWWvYVe3tRppviqKixCvjqgw&W=1; path=/; domain=.livefilestore.com;,
    //RPSMaybe=; path=/; domain=.livefilestore.com; expires=Thu, 30-Oct-1980 16:00:00 GMT;

    //check whether the cookie name is valid or not

    /// <summary>
    /// 校验Cookie的名字是否有效/合法
    /// </summary>
    /// <param name="ckName"></param>
    /// <returns></returns>
    public static bool IsValidCookieName(string ckName)
    {
        bool isValid = true;
        if (ckName == null)
        {
            isValid = false;
        }
        else
        {
            string invalidP = @"\W+";
            Regex rx = new Regex(invalidP);
            Match foundInvalid = rx.Match(ckName);
            if (foundInvalid.Success)
            {
                isValid = false;
            }
        }

        return isValid;
    }

    // parse the cookie name and value
    /// <summary>
    /// 解析Cookie的名字和值
    /// </summary>
    /// <param name="ckNameValueExpr"></param>
    /// <param name="pair"></param>
    /// <returns></returns>
    public static bool ParseCookieNameValue(string ckNameValueExpr, out PairItem pair)
    {
        bool parsedOk = false;
        if (ckNameValueExpr == "")
        {
            pair.Key = "";
            pair.Value = "";
            parsedOk = false;
        }
        else
        {
            ckNameValueExpr = ckNameValueExpr.Trim();

            int equalPos = ckNameValueExpr.IndexOf('=');
            if (equalPos > 0) // is valid expression
            {
                pair.Key = ckNameValueExpr.Substring(0, equalPos);
                pair.Key = pair.Key.Trim();
                if (IsValidCookieName(pair.Key))
                {
                    // only process while is valid cookie field
                    pair.Value = ckNameValueExpr.Substring(equalPos + 1);
                    pair.Value = pair.Value.Trim();
                    parsedOk = true;
                }
                else
                {
                    pair.Key = "";
                    pair.Value = "";
                    parsedOk = false;
                }
            }
            else
            {
                pair.Key = "";
                pair.Value = "";
                parsedOk = false;
            }
        }
        return parsedOk;
    }

    // parse cookie field expression

    /// <summary>
    /// 解析Cookie的项和域值
    /// </summary>
    /// <param name="ckFieldExpr"></param>
    /// <param name="pair"></param>
    /// <returns></returns>
    public bool ParseCookieField(string ckFieldExpr, out PairItem pair)
    {
        bool parsedOk = false;

        if (ckFieldExpr == "")
        {
            pair.Key = "";
            pair.Value = "";
            parsedOk = false;
        }
        else
        {
            ckFieldExpr = ckFieldExpr.Trim();

            //some specials: secure/httponly
            if (ckFieldExpr.ToLower() == "httponly")
            {
                pair.Key = "httponly";
                //pair.value = "";
                pair.Value = "true";
                parsedOk = true;
            }
            else if (ckFieldExpr.ToLower() == "secure")
            {
                pair.Key = "secure";
                //pair.value = "";
                pair.Value = "true";
                parsedOk = true;
            }
            else // normal cookie field
            {
                int equalPos = ckFieldExpr.IndexOf('=');
                if (equalPos > 0) // is valid expression
                {
                    pair.Key = ckFieldExpr.Substring(0, equalPos);
                    pair.Key = pair.Key.Trim();
                    if (IsValidCookieField(pair.Key))
                    {
                        // only process while is valid cookie field
                        pair.Value = ckFieldExpr.Substring(equalPos + 1);
                        pair.Value = pair.Value.Trim();
                        parsedOk = true;
                    }
                    else
                    {
                        pair.Key = "";
                        pair.Value = "";
                        parsedOk = false;
                    }
                }
                else
                {
                    pair.Key = "";
                    pair.Value = "";
                    parsedOk = false;
                }
            }
        }

        return parsedOk;
    } //parseCookieField

    //parse single cookie string to a cookie
    //example: 
    //MSPShared=1; expires=Wed, 30-Dec-2037 16:00:00 GMT;domain=login.live.com;path=/;HTTPOnly= ;version=1
    //PPAuth=CkLXJYvPpNs3w!fIwMOFcraoSIAVYX3K!CdvZwQNwg3Y7gv74iqm9MqReX8XkJqtCFeMA6GYCWMb9m7CoIw!ID5gx3pOt8sOx1U5qQPv6ceuyiJYwmS86IW*l3BEaiyVCqFvju9BMll7!FHQeQholDsi0xqzCHuW!Qm2mrEtQPCv!qF3Sh9tZDjKcDZDI9iMByXc6R*J!JG4eCEUHIvEaxTQtftb4oc5uGpM!YyWT!r5jXIRyxqzsCULtWz4lsWHKzwrNlBRbF!A7ZXqXygCT8ek6luk7rarwLLJ!qaq2BvS; domain=login.live.com;secure= ;path=/;HTTPOnly= ;version=1
    /// <summary>
    /// 解析（SetCookie的）字符串为单个Cookie值
    /// </summary>
    /// <param name="cookieStr"></param>
    /// <param name="ck"></param>
    /// <returns></returns>
    public bool ParseSingleCookie(string cookieStr, ref Cookie ck)
    {
        bool parsedOk = true;
        //Cookie ck = new Cookie();
        //string[] expressions = cookieStr.Split(";".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
        //refer: http://msdn.microsoft.com/en-us/library/b873y76a.aspx
        string[] expressions = cookieStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        //get cookie name and value
        PairItem pair;
        if (ParseCookieNameValue(expressions[0], out pair))
        {
            ck.Name = pair.Key;
            ck.Value = pair.Value;

            string[] fieldExpressions = GetSubStrArr(expressions, 1, expressions.Length - 1);
            bool noDeisignateExpires = true;
            foreach (string eachExpression in fieldExpressions)
            {
                //parse key and value
                if (ParseCookieField(eachExpression, out pair))
                {
                    // add to cookie field if possible
                    bool addedOk = false;
                    addedOk = AddFieldToCookie(ref ck, pair);
                    if (addedOk && string.Equals(pair.Key, ConstStrExpires))
                    {
                        noDeisignateExpires = false;
                    }
                }
                else
                {
                    // if any field fail, consider it is a abnormal cookie string, so quit with false
                    parsedOk = false;
                    break;
                }
            }
            if (noDeisignateExpires)
            {
                ck.Expires = DateTime.MaxValue;
            }
        }
        else
        {
            parsedOk = false;
        }

        return parsedOk;
    } //parseSingleCookie


    // parse the Set-Cookie string (in http response header) to cookies
    // Note: auto omit to parse the abnormal cookie string
    // normal example for 'setCookieStr':
    // MSPOK= ; expires=Thu, 30-Oct-1980 16:00:00 GMT;domain=login.live.com;path=/;HTTPOnly= ;version=1,PPAuth=Cuyf3Vp2wolkjba!TOr*0v22UMYz36ReuiwxZZBc8umHJYPlRe4qupywVFFcIpbJyvYZ5ZDLBwV4zRM1UCjXC4tUwNuKvh21iz6gQb0Tu5K7Z62!TYGfowB9VQpGA8esZ7iCRucC7d5LiP3ZAv*j4Z3MOecaJwmPHx7!wDFdAMuQUZURhHuZWJiLzHP1j8ppchB2LExnlHO6IGAdZo1f0qzSWsZ2hq*yYP6sdy*FdTTKo336Q1B0i5q8jUg1Yv6c2FoBiNxhZSzxpuU0WrNHqSytutP2k4!wNc6eSnFDeouX; domain=login.live.com;secure= ;path=/;HTTPOnly= ;version=1,PPLState=1; domain=.live.com;path=/;version=1,MSPShared=1; expires=Wed, 30-Dec-2037 16:00:00 GMT;domain=login.live.com;path=/;HTTPOnly= ;version=1,MSPPre= ;domain=login.live.com;path=/;Expires=Thu, 30-Oct-1980 16:00:00 GMT,MSPCID= ; HTTPOnly= ; domain=login.live.com;path=/;Expires=Thu, 30-Oct-1980 16:00:00 GMT,RPSTAuth=EwDoARAnAAAUWkziSC7RbDJKS1VkhugDegv7L0eAAOfCAY2+pKwbV5zUlu3XmBbgrQ8EdakmdSqK9OIKfMzAbnU8fuwwEi+FKtdGSuz/FpCYutqiHWdftd0YF21US7+1bPxuLJ0MO+wVXB8GtjLKZaA0xCXlU5u01r+DOsxSVM777DmplaUc0Q4O1+Pi9gX9cyzQLAgRKmC/QtlbVNKDA2YAAAhIwqiXOVR/DDgBocoO/n0u48RFGh79X2Q+gO4Fl5GMc9Vtpa7SUJjZCCfoaitOmcxhEjlVmR/2ppdfJx3Ykek9OFzFd+ijtn7K629yrVFt3O9q5L0lWoxfDh5/daLK7lqJGKxn1KvOew0SHlOqxuuhYRW57ezFyicxkxSI3aLxYFiqHSu9pq+TlITqiflyfcAcw4MWpvHxm9on8Y1dM2R4X3sxuwrLQBpvNsG4oIaldTYIhMEnKhmxrP6ZswxzteNqIRvMEKsxiksBzQDDK/Cnm6QYBZNsPawc6aAedZioeYwaV3Z/i3tNrAUwYTqLXve8oG6ZNXL6WLT/irKq1EMilK6Cw8lT3G13WYdk/U9a6YZPJC8LdqR0vAHYpsu/xRF39/On+xDNPE4keIThJBptweOeWQfsMDwvgrYnMBKAMjpLZwE=; domain=.live.com;path=/;HTTPOnly= ;version=1,RPSTAuthTime=1328679636; domain=login.live.com;path=/;HTTPOnly= ;version=1,MSPAuth=2OlAAMHXtDIFOtpaK1afG2n*AAxdfCnCBlJFn*gCF8gLnCa1YgXEfyVh2m9nZuF*M7npEwb4a7Erpb*!nH5G285k7AswJOrsr*gY29AVAbsiz2UscjIGHkXiKrTvIzkV2M; domain=.live.com;path=/;HTTPOnly= ;version=1,MSPProf=23ci9sti6DZRrkDXfTt1b3lHhMdheWIcTZU2zdJS9!zCloHzMKwX30MfEAcCyOjVt*5WeFSK3l2ZahtEaK7HPFMm3INMs3r!JxI8odP9PYRHivop5ryohtMYzWZzj3gVVurcEr5Bg6eJJws7rXOggo3cR4FuKLtXwz*FVX0VWuB5*aJhRkCT1GZn*L5Pxzsm9X; domain=.live.com;path=/;HTTPOnly= ;version=1,MSNPPAuth=CiGSMoUOx4gej8yQkdFBvN!gvffvAhCPeWydcrAbcg!O2lrhVb4gruWSX5NZCBPsyrtZKmHLhRLTUUIxxPA7LIhqW5TCV*YcInlG2f5hBzwzHt!PORYbg79nCkvw65LKG399gRGtJ4wvXdNlhHNldkBK1jVXD4PoqO1Xzdcpv4sj68U6!oGrNK5KgRSMXXpLJmCeehUcsRW1NmInqQXpyanjykpYOcZy0vq!6PIxkj3gMaAvm!1vO58gXM9HX9dA0GloNmCDnRv4qWDV2XKqEKp!A7jiIMWTmHup1DZ!*YCtDX3nUVQ1zAYSMjHmmbMDxRJECz!1XEwm070w16Y40TzuKAJVugo!pyF!V2OaCsLjZ9tdGxGwEQRyi0oWc*Z7M0FBn8Fz0Dh4DhCzl1NnGun9kOYjK5itrF1Wh17sT!62ipv1vI8omeu0cVRww2Kv!qM*LFgwGlPOnNHj3*VulQOuaoliN4MUUxTA4owDubYZoKAwF*yp7Mg3zq5Ds2!l9Q$$; domain=.live.com;path=/;HTTPOnly= ;version=1,MH=MSFT; domain=.live.com;path=/;version=1,MHW=; expires=Thu, 30-Oct-1980 16:00:00 GMT;domain=.live.com;path=/;version=1,MHList=; expires=Thu, 30-Oct-1980 16:00:00 GMT;domain=.live.com;path=/;version=1,NAP=V=1.9&E=bea&C=zfjCKKBD0TqjZlWGgRTp__NiK08Lme_0XFaiKPaWJ0HDuMi2uCXafQ&W=1;domain=.live.com;path=/,ANON=A=DE389D4D076BF47BCAE4DC05FFFFFFFF&E=c44&W=1;domain=.live.com;path=/,MSPVis=$9;domain=login.live.com;path=/,pres=; expires=Thu, 30-Oct-1980 16:00:00 GMT;domain=.live.com;path=/;version=1,LOpt=0; domain=login.live.com;path=/;version=1,WLSSC=EgBnAQMAAAAEgAAACoAASfCD+8dUptvK4kvFO0gS3mVG28SPT3Jo9Pz2k65r9c9KrN4ISvidiEhxXaPLCSpkfa6fxH3FbdP9UmWAa9KnzKFJu/lQNkZC3rzzMcVUMjbLUpSVVyscJHcfSXmpGGgZK4ZCxPqXaIl9EZ0xWackE4k5zWugX7GR5m/RzakyVIzWAFwA1gD9vwYA7Vazl9QKMk/UCjJPECcAAAoQoAAAFwBjcmlmYW4yMDAzQGhvdG1haWwuY29tAE8AABZjcmlmYW4yMDAzQGhvdG1haWwuY29tAAAACUNOAAYyMTM1OTIAAAZlCAQCAAB3F21AAARDAAR0aWFuAAR3YW5nBMgAAUkAAAAAAAAAAAAAAaOKNpqLi/UAANQKMk/Uf0RPAAAAAAAAAAAAAAAADgA1OC4yNDAuMjM2LjE5AAUAAAAAAAAAAAAAAAABBAABAAABAAABAAAAAAAAAAA=; domain=.live.com;secure= ;path=/;HTTPOnly= ;version=1,MSPSoftVis=@72198325083833620@:@; domain=login.live.com;path=/;version=1
    // here now support parse the un-correct Set-Cookie:
    // MSPRequ=/;Version=1;version&lt=1328770452&id=250915&co=1; path=/;version=1,MSPVis=$9; Version=1;version=1$250915;domain=login.live.com;path=/,MSPSoftVis=@72198325083833620@:@; domain=login.live.com;path=/;version=1,MSPBack=1328770312; domain=login.live.com;path=/;version=1


    /// <summary>
    /// 解析（Http访问所返回的）Set-Cookie的字符串为Cookie数组:parseSetCookie
    /// </summary>
    /// <param name="setCookieStr"></param>
    /// <param name="curDomain"></param>
    /// <returns></returns>
    public CookieCollection ParseSetCookie(string setCookieStr, string curDomain)
    {
        CookieCollection parsedCookies = new CookieCollection();

        if (!string.IsNullOrEmpty(setCookieStr))
        {
            // process for expires and Expires field, for it contains ','
            //refer: http://www.yaosansi.com/post/682.html
            // may contains expires or Expires, so following use xpires
            string commaReplaced = Regex.Replace(setCookieStr, @"xpires=\w{3},\s\d{2}-\w{3}-\d{2,4}", new MatchEvaluator(ProcessExpireField));
            string[] cookieStrArr = commaReplaced.Split(',');
            foreach (string cookieStr in cookieStrArr)
            {
                Cookie ck = new Cookie();
                // recover it back
                string recoveredCookieStr = Regex.Replace(cookieStr, @"xpires=\w{3}" + ConstReplacedChar + @"\s\d{2}-\w{3}-\d{2,4}", new MatchEvaluator(RecoverExpireField));
                if (ParseSingleCookie(recoveredCookieStr, ref ck))
                {
                    if (NeedAddThisCookie(ck, curDomain))
                    {
                        parsedCookies.Add(ck);
                    }
                }
            }
        }

        return parsedCookies;
    } //parseSetCookie

    // parse Set-Cookie string part into cookies
    // leave current domain to empty, means omit the parsed cookie, which is not set its domain value
    /// <summary>
    /// 解析Javascript中的setCookie为Cookie变量:parseJsSetCookie
    /// </summary>
    /// <param name="setCookieStr"></param>
    /// <returns></returns>
    public CookieCollection ParseSetCookie(string setCookieStr)
    {
        return ParseSetCookie(setCookieStr, "");
    }

    //parse Javascript string "$Cookie.setCookie(XXX);" to a cookie
    // input example:
    //$Cookie.setCookie('wla42','cHJveHktYmF5LnB2dC1jb250YWN0cy5tc24uY29tfGJ5MioxLDlBOEI4QkY1MDFBMzhBMzYsMSwwLDA=','live.com','/',new Date(1328842189083.44),1);
    //$Cookie.setCookie('wla42','YnkyKjEsOUE4QjhCRjUwMUEzOEEzNiwwLCww','live.com','/',new Date(1329198041411.84),1);
    //$Cookie.setCookie('wla42', 'YnkyKjEsOUE4QjhCRjUwMUEzOEEzNiwwLCww', 'live.com', '/', new Date(1329440307389.9), 1);
    //$Cookie.setCookie('wla42', 'cHJveHktYmF5LnB2dC1jb250YWN0cy5tc24uY29tfGJ5MioxLDlBOEI4QkY1MDFBMzhBMzYsMSwwLDA=', 'live.com', '/', new Date(1329440307483.5), 1);
    //$Cookie.setCookie('wls', 'A|eyJV-t:a*nS', '.live.com', '/', null, 1);
    //$Cookie.setCookie('MSNPPAuth','','.live.com','/',new Date(1327971507311.9),1);
    public bool ParseJsSetCookie(string singleSetCookieStr, out Cookie parsedCk)
    {
        bool parseOk = false;
        parsedCk = new Cookie();

        string name = "";
        string value = "";
        string domain = "";
        string path = "";
        string expire = "";
        string secure = "";

        //                                     1=name      2=value     3=domain     4=path   5=expire  6=secure
        string setckP = @"\$Cookie\.setCookie\('(\w+)',\s*'(.*?)',\s*'([\w\.]+)',\s*'(.+?)',\s*(.+?),\s*(\d?)\);";
        Regex setckRx = new Regex(setckP);
        Match foundSetck = setckRx.Match(singleSetCookieStr);
        if (foundSetck.Success)
        {
            name = foundSetck.Groups[1].ToString();
            value = foundSetck.Groups[2].ToString();
            domain = foundSetck.Groups[3].ToString();
            path = foundSetck.Groups[4].ToString();
            expire = foundSetck.Groups[5].ToString();
            secure = foundSetck.Groups[6].ToString();

            // must: name valid and domain is not null
            if (IsValidCookieName(name) && (domain != ""))
            {
                parseOk = true;

                parsedCk.Name = name;
                parsedCk.Value = value;
                parsedCk.Domain = domain;
                parsedCk.Path = path;

                // note, here even parse expire field fail
                //do not consider it must fail to parse the whole cookie
                if (expire.Trim() == "null")
                {
                    // do nothing
                }
                else
                {
                    DateTime expireTime;
                    if (ParseJsNewDate(expire, out expireTime))
                    {
                        parsedCk.Expires = expireTime;
                    }
                }

                if (secure == "1")
                {
                    parsedCk.Secure = true;
                }
                else
                {
                    parsedCk.Secure = false;
                }
            } //if (isValidCookieName(name) && (domain != ""))
        } //foundSetck.Success

        return parseOk;
    }

    //check whether a cookie is expired
    //if expired property is set, then just return it value
    //if not set, check whether is a session cookie, if is, then not expired
    //if expires is set, check its real time is expired or not

    //add a single cookie to cookies, if already exist, update its value
    /// <summary>
    /// 判断Cookie是否已经过期/失效/无效
    /// </summary>
    /// <param name="toAdd"></param>
    /// <param name="cookies"></param>
    /// <param name="overwriteDomain"></param>
    public static void AddCookieToCookies(Cookie toAdd, ref CookieCollection cookies, bool overwriteDomain)
    {
        bool found = false;

        if (cookies.Count > 0)
        {
            foreach (Cookie originalCookie in cookies)
            {
                if (originalCookie.Name == toAdd.Name)
                {
                    // !!! for different domain, cookie is not same,
                    // so should not set the cookie value here while their domains is not same
                    // only if it explictly need overwrite domain
                    if ((originalCookie.Domain == toAdd.Domain) || ((originalCookie.Domain != toAdd.Domain) && overwriteDomain))
                    {
                        //here can not force convert CookieCollection to HttpCookieCollection,
                        //then use .remove to remove this cookie then add
                        // so no good way to copy all field value
                        originalCookie.Value = toAdd.Value;

                        originalCookie.Domain = toAdd.Domain;

                        originalCookie.Expires = toAdd.Expires;
                        originalCookie.Version = toAdd.Version;
                        originalCookie.Path = toAdd.Path;

                        //following fields seems should not change
                        //originalCookie.HttpOnly = toAdd.HttpOnly;
                        //originalCookie.Secure = toAdd.Secure;

                        found = true;
                        break;
                    }
                }
            }
        }

        if (!found)
        {
            if (toAdd.Domain != "")
            {
                // if add the null domain, will lead to follow req.CookieContainer.Add(cookies) failed !!!
                cookies.Add(toAdd);
            }
        }

    } //addCookieToCookies

    //add singel cookie to cookies, default no overwrite domain
    /// <summary>
    /// 将单个Cookie添加到Cookie数组变量中:addCookieToCookies
    /// </summary>
    /// <param name="toAdd"></param>
    /// <param name="cookies"></param>
    public static void AddCookieToCookies(Cookie toAdd, ref CookieCollection cookies)
    {
        AddCookieToCookies(toAdd, ref cookies, false);
    }

    //check whether the cookies contains the ckToCheck cookie
    //support:
    //ckTocheck is Cookie/string
    //cookies is Cookie/string/CookieCollection/string[]
    public static bool IsContainCookie(object ckToCheck, object cookies)
    {
        bool isContain = false;

        if ((ckToCheck != null) && (cookies != null))
        {
            string ckName = "";
            Type type = ckToCheck.GetType();

            //string typeStr = ckType.ToString();

            //if (ckType.FullName == "System.string")
            if (type.Name.ToLower() == "string")
            {
                ckName = (string)ckToCheck;
            }
            else if (type.Name == "Cookie")
            {
                ckName = ((Cookie)ckToCheck).Name;
            }

            if (ckName != "")
            {
                type = cookies.GetType();

                // is single Cookie
                if (type.Name == "Cookie")
                {
                    if (ckName == ((Cookie)cookies).Name)
                    {
                        isContain = true;
                    }
                }
                // is CookieCollection
                else if (type.Name == "CookieCollection")
                {
                    foreach (Cookie ck in (CookieCollection)cookies)
                    {
                        if (ckName == ck.Name)
                        {
                            isContain = true;
                            break;
                        }
                    }
                }
                // is single cookie name string
                else if (type.Name.ToLower() == "string")
                {
                    if (ckName == (string)cookies)
                    {
                        isContain = true;
                    }
                }
                // is cookie name string[]
                else if (type.Name.ToLower() == "string[]")
                {
                    foreach (string name in ((string[])cookies))
                    {
                        if (ckName == name)
                        {
                            isContain = true;
                            break;
                        }
                    }
                }
            }
        }

        return isContain;
    } //isContainCookie

    // 
    // if omitUpdateCookies designated, then omit cookies of omitUpdateCookies in cookiesToUpdate
    /// <summary>
    /// 判断Cookies中是否包含某个Cookie:isContainCookie
    /// <para>update cookiesToUpdate to localCookies</para>
    /// </summary>
    /// <param name="cookiesToUpdate"></param>
    /// <param name="localCookies"></param>
    /// <param name="omitUpdateCookies"></param>
    public void UpdateLocalCookies(CookieCollection cookiesToUpdate, ref CookieCollection localCookies, object omitUpdateCookies)
    {
        if (cookiesToUpdate.Count > 0)
        {
            if (localCookies == null)
            {
                localCookies = cookiesToUpdate;
            }
            else
            {
                foreach (Cookie newCookie in cookiesToUpdate)
                {
                    if (IsContainCookie(newCookie, omitUpdateCookies))
                    {
                        // need omit process this
                    }
                    else
                    {
                        AddCookieToCookies(newCookie, ref localCookies);
                    }
                }
            }
        }
    } //updateLocalCookies

    //
    /// <summary>
    /// 更新本地Cookie:updateLocalCookies
    /// <para>update cookiesToUpdate to localCookies</para>
    /// </summary>
    /// <param name="cookiesToUpdate"></param>
    /// <param name="localCookies"></param>
    public void UpdateLocalCookies(CookieCollection cookiesToUpdate, ref CookieCollection localCookies)
    {
        UpdateLocalCookies(cookiesToUpdate, ref localCookies, null);
    }

    // 
    /// <summary>
    /// 从一个CookieCollection获得一个Cookie的值:getCookieVal
    /// <para>given a cookie name ckName, get its value from CookieCollection cookies</para>
    /// </summary>
    /// <param name="ckName"></param>
    /// <param name="cookies"></param>
    /// <param name="ckVal"></param>
    /// <returns></returns>
    public bool GetCookieVal(string ckName, ref CookieCollection cookies, out string ckVal)
    {
        //string ckVal = "";
        ckVal = "";
        bool gotValue = false;

        foreach (Cookie ck in cookies)
        {
            if (ck.Name == ckName)
            {
                gotValue = true;
                ckVal = ck.Value;
                break;
            }
        }

        return gotValue;
    }

    #endregion
}