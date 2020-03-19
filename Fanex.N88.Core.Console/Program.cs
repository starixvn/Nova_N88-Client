namespace Fanex.N88.Core.Console
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Fanex.N88.Core.Models.ValueObject;
    using Fanex.N88.Core.Service.Impl;

    public static class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<UserPolicy> userPolicies = new UserService().GetUserPolicies("ManageUser", 1);
            Console.ReadLine();
        }

        public static Task<string> DoAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                return "Hello world!";
            });
        }
    }
}