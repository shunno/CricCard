using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Microsoft.AspNet.SignalR;
using  System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNet.SignalR.Hubs;
using Model.ViewModel;

namespace WebMvc.Hubs
{
    [EnableCors(origins: "http://localhost:2179/", headers: "*", methods: "*")]
    [HubName("match")]
    public class MatchHub : Hub
    {

        public static ConcurrentDictionary<int, List<string>> _mapping = new ConcurrentDictionary<int, List<string>>();

        private static ConcurrentDictionary<string,int> _userToMatchmapping = new ConcurrentDictionary<string,int>();

        public override Task OnConnected()
        {
            string value = Context.ConnectionId;
            return base.OnConnected();
        }

        public void AddToMatch(int matchId)
        {
            _mapping.TryAdd(matchId, new List<string>());
            _mapping[matchId].Add(Context.ConnectionId);
            _userToMatchmapping.TryAdd(Context.ConnectionId, matchId);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var connectionId = Context.ConnectionId;
            if (_userToMatchmapping.Keys.Contains(connectionId))
            {
                var match = _userToMatchmapping[connectionId];
                _mapping[match].Remove(connectionId);
                int removedmatch = 0;
                _userToMatchmapping.TryRemove(connectionId, out removedmatch);
            }
            
            return base.OnDisconnected(stopCalled);
        }
    }
}