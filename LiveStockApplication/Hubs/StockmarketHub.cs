using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LiveStockApplication.Hubs
{
    // Denne klasse er til for at fange når clientside vil sende noget retur.
    // Skal udvides hvis clientside skal kunne sende noget til serveren via "hubben"
    public class StockmarketHub : Hub
    {
        
        //public async Task SendData(string data)
        //{
        //    //await Clients.All.SendAsync("UpdateStockmarket", data);
        //}

    }
}
