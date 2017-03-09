using DemoScriptShp.Poco;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WebParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DemoScriptShp.Dal
{
    public static class SharePoint
    {
        public static void AnalizarColeccion(Config cfg)
        {
            AnalizarSitioRecursivo(cfg.Admin, cfg.Pass, cfg.Url);
        }

        private static void AnalizarSitioRecursivo(string admin, string pass, Uri url)
        {
            using (var ctx = GetContext(admin, pass, url))
            {
                Web oWebsite = ctx.Web;
                ctx.Load(oWebsite, w => w.Webs, w => w.Title, w => w.Url);
                ctx.ExecuteQuery();

                Console.WriteLine("URL: " + oWebsite.Url);

                AnalizarPaginasSitio(ctx, oWebsite);

                foreach (Web oRecWebsite in oWebsite.Webs)
                {
                    AnalizarSitioRecursivo(admin, pass, new Uri(oRecWebsite.Url));
                }
            }
        }

        private static void AnalizarPaginasSitio(ClientContext ctx, Web web)
        {
            string[] bibliotecas = { "Páginas del sitio", "SitePages" };

            foreach (string biblioteca in bibliotecas)
            {
                try
                {
                    List lista = web.Lists.GetByTitle(biblioteca);
                    ListItemCollection items = lista.GetItems(CamlQuery.CreateAllItemsQuery());
                    ctx.Load(lista);
                    ctx.Load(items);
                    ctx.ExecuteQuery();

                    Console.WriteLine("  LISTA: " + lista.Title);

                    foreach (ListItem li in items)
                    {
                        SetWP(ctx, li["FileRef"] as string);
                    }
                }
                catch (Microsoft.SharePoint.Client.ServerException ex)
                {
                    Console.WriteLine("  Ex: " + ex.Message);
                }
            }
        }

        private static void SetWP(ClientContext ctx, string pageUrl)
        {
            if (string.IsNullOrEmpty(pageUrl))
                return;

            Console.WriteLine("    PAG: " + pageUrl);

            File page = ctx.Web.GetFileByServerRelativeUrl(pageUrl);
            LimitedWebPartManager wpm = page.GetLimitedWebPartManager(PersonalizationScope.Shared);

            string zoneId = WPContenido.ZoneId;
            int zoneIdx = WPContenido.ZoneIndex;

            var queryWPs = wpm.WebParts.Where(x => x.WebPart.Title == WPContenido.Title);

            /*var queryWPs = from wp in wpm.WebParts
                           where wp.WebPart.Title == WPContenido.Title
                           select wp;*/

            IEnumerable<WebPartDefinition> definicionesWP = ctx.LoadQuery(queryWPs);
            ctx.ExecuteQuery();

            //Elimina el WP si existe para actualizarlo
            foreach (WebPartDefinition wpdef in definicionesWP)
            {
                ctx.Load(wpdef.WebPart);
                ctx.ExecuteQuery();
                zoneIdx = wpdef.WebPart.ZoneIndex;

                Console.WriteLine("      WP encontrado en zona " + zoneIdx + ". Borrando");

                wpdef.DeleteWebPart();
                ctx.ExecuteQuery();
            }

            //Inserta nuevo
            var importedWebPart = wpm.ImportWebPart(WPContenido.DefinicionWP(""));
            var webPart = wpm.AddWebPart(importedWebPart.WebPart, zoneId, zoneIdx);
            ctx.Load(webPart);
            ctx.ExecuteQuery();

            Console.WriteLine("      WP agregado.");
        }

        private static ClientContext GetContext(Config cfg)
        {
            return GetContext(cfg.Admin, cfg.Pass, cfg.Url);
        }

        private static ClientContext GetContext(string user, string password, Uri url)
        {
            var securePassword = new SecureString();
            foreach (var ch in password)
                securePassword.AppendChar(ch);

            return new ClientContext(url)
            {
                Credentials = new SharePointOnlineCredentials(user, securePassword)
            };
        }
    }
}
