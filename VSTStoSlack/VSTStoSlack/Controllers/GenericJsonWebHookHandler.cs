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
            var usuarios = UsuariosRepository.GetUsuarios();
            var mensagem = data["detailedMessage"]["text"].ToString();
            var nomePortal = "VSTS - BOT";

            if (!string.IsNullOrEmpty(data["resource"].ToString()))
                nomePortal = string.Format("VSTS - {0}", data["resource"]["fields"]["System.TeamProject"].ToString());

            foreach (var usuarioMencionado in usuarios)
            {
                if (mensagem.IndexOf(usuarioMencionado.NomeVSO) >= 0)
                {
                    mensagem = mensagem.Replace(usuarioMencionado.NomeVSO, usuarioMencionado.NomeSlack);
                    EnviarAsync(usuarioMencionado, mensagem, nomePortal);
                }
            }
        }

        return Task.FromResult(true);
    }

    private void EnviarAsync(Usuario usuarioMencionado, string mensagem, string nomePortal)
    {
        var restClient = new RestSharp.RestClient(ConfigurationManager.AppSettings["WebHookUrlSlack"]);
        var request = new RestSharp.RestRequest(RestSharp.Method.POST);

        //Caso você queria enviar msg direto pra pessoa. Lembrando que vai como bot.
        request.AddJsonBody(new { text = mensagem, channel = String.Format("@{0}",usuarioMencionado.NomeSlack), username = nomePortal });

        //Caso você queria enviar msg no canal com o mention é só passar o nome do canal em channel.
        //request.AddJsonBody(new { text = mensagem, channel = "ds-mobile-open" });

        var result = restClient.Execute(request);
    }
}
