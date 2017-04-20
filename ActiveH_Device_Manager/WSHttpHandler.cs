using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Web.WebSockets;

namespace ActiveH_Device_Manager
{
    public class WSHttpHandler : IHttpHandler
    {
        WebSocketConnectionManager wscm = new WebSocketConnectionManager();

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(WebSocketRequestHandler);
            }
        }

        public async Task WebSocketRequestHandler(AspNetWebSocketContext webSocketContext)
        {
            WebSocket webSocket = webSocketContext.WebSocket;

            wscm.AddSocket(webSocket);

            const int maxMsgSize = 1024;

            ArraySegment<Byte> recdDataBuffer = new ArraySegment<Byte>(new Byte[maxMsgSize]);
            CancellationToken cancellationToken = new CancellationToken();

            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult wsRecResult = await webSocket.ReceiveAsync(recdDataBuffer, cancellationToken);

                if (wsRecResult.MessageType == WebSocketMessageType.Close)
                {
                    string socketId = wscm.GetId(webSocket);
                    await wscm.RemoveSocket(socketId);
                }
                else
                {
                    byte[] payload = recdDataBuffer.Array.Where(b => b != 0).ToArray();
                    string recString = Encoding.UTF8.GetString(payload, 0, payload.Length);
                }
            }
        }
    }
}
