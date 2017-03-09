using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoScriptShp.Poco
{
    public static class WPContenido
    {
        public const string Title = "PiePaginaCorporativo";
        public const string ZoneId = "Footer";
        public const int ZoneIndex = 1;

        private const string schema = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<WebPart xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.microsoft.com/WebPart/v2\">" +
                "<Title>{0}</Title>" +
                "<FrameType>None</FrameType>" +
                "<Description>Permite a los autores escribir contenido de texto enriquecido.</Description>" +
                "<IsIncluded>true</IsIncluded>" +
                "<ZoneID>wpz</ZoneID>" +
                "<PartOrder>0</PartOrder>" +
                "<FrameState>Normal</FrameState>" +
                "<Height />" +
                "<Width />" +
                "<AllowRemove>true</AllowRemove>" +
                "<AllowZoneChange>true</AllowZoneChange>" +
                "<AllowMinimize>true</AllowMinimize>" +
                "<AllowConnect>true</AllowConnect>" +
                "<AllowEdit>true</AllowEdit>" +
                "<AllowHide>true</AllowHide>" +
                "<IsVisible>true</IsVisible>" +
                "<DetailLink />" +
                "<HelpLink />" +
                "<HelpMode>Modeless</HelpMode>" +
                "<Dir>Default</Dir>" +
                "<PartImageSmall />" +
                "<MissingAssembly>No se puede importar este elemento web.</MissingAssembly>" +
                "<PartImageLarge>/_layouts/15/images/mscontl.gif</PartImageLarge>" +
                "<IsIncludedFilter />" +
                "<Assembly>Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c</Assembly>" +
                "<TypeName>Microsoft.SharePoint.WebPartPages.ContentEditorWebPart</TypeName>" +
                "<ContentLink xmlns = \"http://schemas.microsoft.com/WebPart/v2/ContentEditor\" />" +
                "<Content xmlns=\"http://schemas.microsoft.com/WebPart/v2/ContentEditor\"><![CDATA[​{1}]]></Content>" +
                "<PartStorage xmlns=\"http://schemas.microsoft.com/WebPart/v2/ContentEditor\" />" +
            "</WebPart>";

        public static string DefinicionWP(string contenido)
        {
            if (string.IsNullOrEmpty(contenido))
                contenido = "&#160;© 2017 Redcom Cibernético<br/>";

            return string.Format(schema, Title, contenido);
        }
    }
}
