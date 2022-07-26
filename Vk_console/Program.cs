using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Vk_console
{
    class Program
    {
        static void Main(string[] args)
        {
            
                var api = new VkApi();

                api.Authorize(new ApiAuthParams
                {
                    AccessToken = "e294a177417e6c6186392a0c22232a573533bf30932080baebc35da6a656187d0f6531dba38794cf13ccf"
                });
                Console.WriteLine(api.Token);
                var res = api.Groups.Get(new GroupsGetParams());

            
                Console.WriteLine(res.TotalCount);

                Console.ReadLine();
            
        }
    }
}
