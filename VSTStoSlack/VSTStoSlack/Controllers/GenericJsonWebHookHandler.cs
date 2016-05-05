using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using VSTStoSlack.Models;
using System;

public class GenericJsonWebHookHandler : WebHookHandler
{
    public GenericJsonWebHookHandler()
    {
        this.Receiver = "genericjson";
    }

    public override Task ExecuteAsync(string generator, WebHookHandlerContext context)
    {
        JObject data = context.GetDataOrDefault<JObject>();

        if (context.Id == "v" && !string.IsNullOrEmpty(data["message"].ToString()))
        {
            var users = UsersRepository.GetUsers();
            var message = data["detailedMessage"]["text"].ToString();
            var boardName = "VSTS - BOT";

            if (!string.IsNullOrEmpty(data["resource"].ToString()))
                boardName = string.Format("VSTS - {0}", data["resource"]["fields"]["System.TeamProject"].ToString());

            foreach (var userMentions in users)
            {
                if (message.IndexOf(userMentions.VstsName) >= 0)
                {
                    message = message.Replace(userMentions.VstsName, userMentions.SlackName);
                    SendAsync(userMentions, message, boardName);
                }
            }
        }

        return Task.FromResult(true);
    }

    private void SendAsync(User userMentions, string message, string boardName)
    {
        var restClient = new RestSharp.RestClient(ConfigurationManager.AppSettings["WebHookUrlSlack"]);
        var request = new RestSharp.RestRequest(RestSharp.Method.POST);

        //Caso você queria enviar msg direto pra pessoa. Lembrando que vai como bot.
        request.AddJsonBody(new { text = message, channel = String.Format("@{0}",userMentions.SlackName), username = boardName });

        //Caso você queria enviar msg no canal com o mention é só passar o nome do canal em channel.
        //request.AddJsonBody(new { text = mensagem, channel = "ds-mobile-open" });

        var result = restClient.Execute(request);
    }
}
