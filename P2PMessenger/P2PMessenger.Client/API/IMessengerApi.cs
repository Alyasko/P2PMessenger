using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;

namespace P2PMessenger.Client.API
{
    public interface IMessengerApi
    {
        [Post("/users/?value={fullIpAddress}")]
        Task RegisterAsync([Path]string fullIpAddress);

        [Get("/users/")]
        Task<IEnumerable<string>> GetConnectedUsersAsync();
    }
}