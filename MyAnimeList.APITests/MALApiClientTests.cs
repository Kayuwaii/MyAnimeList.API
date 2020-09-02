using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyAnimeList.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyAnimeList.API.Tests
{
    [TestClass()]
    public class MALApiClientTests
    {
        [TestMethod()]
        public void MALApiClientTest()
        {
            try
            {
                var temp = new UserParams
                {
                    ClientId = "0c16e92529563bfe0a468a4bb3ff632b",
                    OAuth2State = "Kayuwaii"
                };
                MALApiClient test = new MALApiClient(temp);
                string code = test.GetAuthUrl();
                test.DoAuth(code);
                try
                {
                    Console.WriteLine(test.GetAnimeList(1).Result);
                }
                catch (Exception e)
                {
                    Assert.Fail();
                }
            }
            catch (Exception s)
            {
                var x = s;
            }
        }
    }
}